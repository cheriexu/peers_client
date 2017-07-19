using Peers.Helpers;
using Peers.Models;
using Peers.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Peers
{
	public class App : Application
	{

        public static bool IsUserLoggedIn { get; set; }
        public static SessionManager sessionManager;
        public static Page mainPage;
        public static UserInfo user;

        public App()
		{
            /*
			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};
            */
            if (!IsUserLoggedIn)
            {
                
                MainPage = new NavigationPage(new LoginSplash())
                {
                    BarBackgroundColor = Color.Black //.FromRgb(0x30, 0x30, 0x30),              
                };
                sessionManager = new SessionManager();
            }
            else
            {
                MainPage = new NavigationPage(new MainMenu())
                {
                    BarBackgroundColor = Color.Black //.FromRgb(0x30, 0x30, 0x30),
                };

            }
            mainPage = MainPage;

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
