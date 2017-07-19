﻿using Newtonsoft.Json;
using Peers.Helpers;
using Peers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Peers.Pages
{

    public partial class LoginPageGoogle : ContentPage
    {

        private static string successRedirectUrl = "http://apporilla.com/mobile-login-success.html";
        private static GoogleInfo user;
        private static WebView webView;

        public LoginPageGoogle()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            String apiRequest = "https://accounts.google.com/o/oauth2/auth?client_id="
                + Config.GoogleAppId
                + "&scope=https://www.googleapis.com/auth/userinfo.email&response_type=token&redirect_uri=" + successRedirectUrl;

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

                var accessToken = OAuthHelper.GetAccessTokenFromUrl(e.Url);

                if (accessToken != "")
                {
                    var requestUrl = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + accessToken;
                    var profile = await OAuthHelper.GetProfileAsync<GoogleInfo>(requestUrl);

                    App.IsUserLoggedIn = true;
                    user = profile;
                    App.user = new UserInfo()
                    {
                        Email = user.email,
                        FirstName = user.given_name,
                        LastName = user.family_name,
                        PictureUrl = user.picture
                    };
                    var userResult = await LoginHelper.UserLoginProcess(App.user.Email, App.user.FirstName, App.user.LastName);

                    Navigation.InsertPageBefore(new MainMenu(), this);
                    await Navigation.PopAsync();

                }
                else
                {
                    // user hit back or there was error, response is like: https://www.facebook.com/connect/login_success.html?error=access_denied&error_code=200&error_description=Permissions+error&error_reason=user_denied#_=_
/*
                    String queryString = e.Url.Replace(GoogleHelper.OAuthUrl, String.Empty);
                    if (queryString.StartsWith("?"))
                        queryString = queryString.Substring(1);
                    var parms = StringUtil.ParseQueryString(queryString);

                    String errorMsg = String.Empty;
                    if (parms.ContainsKey("error"))
                        errorMsg = parms["error"];
*/
                    String errorMsg = OAuthHelper.GetParmFromUrl(e.Url, "error");

                    await App.mainPage.Navigation.PopToRootAsync();
                    var page = ((NavigationPage)(App.mainPage));
                    ((LoginSplash)page.CurrentPage).SetMessage("error: " + errorMsg);
                }
            }


        }

    }
}
