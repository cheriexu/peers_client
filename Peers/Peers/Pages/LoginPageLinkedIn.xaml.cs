﻿using Newtonsoft.Json;
using Peers.Helpers;
using Peers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Peers.Pages
{
    public partial class LoginPageLinkedIn : ContentPage
    {
        // to update go to: https://www.linkedin.com/developer/apps/5076696/auth
        private static string successRedirectUrl = "https://www.linkedin.com/help/linkedin"; // this should be an HTTPS success page. note this fails on iOS if not HTTPS link
        private static LinkedInProfile user;
        private string reqState;
        private static WebView webView;

        public LoginPageLinkedIn()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            reqState = Guid.NewGuid().ToString().Replace("-", "");

            String apiRequest = "https://www.linkedin.com/oauth/v2/authorization?client_id="
                + Config.LinkedInAppId
                + "&scope=r_basicprofile%20r_emailaddress&state=" + reqState + "&response_type=code&redirect_uri=" + successRedirectUrl;

            webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;
            webView.Navigating += WebViewOnNavigating;
            Content = webView;
        }

        public async void WebViewOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith(successRedirectUrl))
                webView.IsVisible = false;
            else
                webView.IsVisible = true;
        }

        public async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            if (e.Url.StartsWith(successRedirectUrl))
            {

                var code = OAuthHelper.GetParmFromUrl(e.Url,"code");
                var state = OAuthHelper.GetParmFromUrl(e.Url, "state");

                if ((code != "") && (state == reqState))
                {
                    var requestUrl = "https://www.linkedin.com/oauth/v2/accessToken";
                    String postData = "grant_type=authorization_code&code=" + code + "&redirect_uri=" + successRedirectUrl + "&client_id=" + Config.LinkedInAppId + "&client_secret=" + Config.LinkedInClientSecret;
                    using (WebClient webClient = new WebClient())
                    {
                        try
                        {

                            webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            var result = await webClient.UploadStringTaskAsync(requestUrl, "POST", postData);
                            if (result.Contains("access_token"))
                            {
                                var oauthResponse = JsonConvert.DeserializeObject<LinkedInOAuthResponse>(result);
                                var accessToken = oauthResponse.AccessToken;

                                webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;
                                var userDataString = webClient.DownloadString(new Uri("https://api.linkedin.com/v1/people/~:(first-name,last-name,email-address,picture-url)?format=json"));
                                var profile = JsonConvert.DeserializeObject<LinkedInProfile>(userDataString);

                                App.IsUserLoggedIn = true;
                                user = profile;
                                App.user = new UserInfo()
                                {
                                    Email = user.emailAddress,
                                    FirstName = user.firstName,
                                    LastName = user.lastName,
                                    PictureUrl = user.pictureUrl
                                };
                                var userResult = await LoginHelper.UserLoginProcess(App.user.Email, App.user.FirstName, App.user.LastName);

                                Navigation.InsertPageBefore(new MainMenu(), this);
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                // error
                                String errorMsg = OAuthHelper.GetParmFromUrl(e.Url, "error");
                                await App.mainPage.Navigation.PopToRootAsync();
                                var page = ((NavigationPage)(App.mainPage));
                                ((LoginSplash)page.CurrentPage).SetMessage("error: " + errorMsg);
                            }
                        } catch(Exception ex)
                        {
                            await App.mainPage.Navigation.PopToRootAsync();
                            var page = ((NavigationPage)(App.mainPage));
                            ((LoginSplash)page.CurrentPage).SetMessage("error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    String errorMsg = OAuthHelper.GetParmFromUrl(e.Url, "error");
                    await App.mainPage.Navigation.PopToRootAsync();
                    var page = ((NavigationPage)(App.mainPage));
                    ((LoginSplash)page.CurrentPage).SetMessage("error: " + errorMsg);
                }


            }


        }

    }
}
