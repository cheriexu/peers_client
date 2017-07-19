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
    public partial class LoginPageFB : ContentPage
    {

        private static string successRedirectUrl = "https://www.facebook.com/connect/login_success.html";
        private static FacebookProfile user;
        private static WebView webView;

        public LoginPageFB()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            String apiRequest = "https://www.facebook.com/dialog/oauth?client_id=" + Config.FacebookAppId
                + "&display=popup&scope=email,public_profile&response_type=token&redirect_uri=" + successRedirectUrl;

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

                    var requestUrl = "https://graph.facebook.com/v2.7/me?fields=name,picture,cover,age_range,devices,email,first_name,last_name,gender,is_verified&access_token=" + accessToken;
                    var profile = await OAuthHelper.GetProfileAsync<FacebookProfile>(requestUrl);

                    App.IsUserLoggedIn = true;
                    user = profile;
                    App.user = new UserInfo()
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PictureUrl = user.Picture.Data.Url
                    };

                    var userResult = await LoginHelper.UserLoginProcess(App.user.Email, App.user.FirstName, App.user.LastName);

                    Navigation.InsertPageBefore(new MainMenu(), this);
                    await Navigation.PopAsync();

                }
                else
                {
                    // user hit back or there was error, response is like: https://www.facebook.com/connect/login_success.html?error=access_denied&error_code=200&error_description=Permissions+error&error_reason=user_denied#_=_

                    String errorMsg = OAuthHelper.GetParmFromUrl(e.Url, "error");
                    await App.mainPage.Navigation.PopToRootAsync();
                    var page = ((NavigationPage)(App.mainPage));
                    ((LoginSplash)page.CurrentPage).SetMessage("error: " + errorMsg);
                }
            }


        }

    }
}
