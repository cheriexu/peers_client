using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Peers.Pages
{
    public partial class LoginSplash : ContentPage
    {
        public LoginSplash()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
#if __ENV_PRODUCTION__
            environment.IsVisible = false;
            environment.Text = "";            
#else            
            environment.IsVisible = true;
            environment.Text = Config.EnvironmentName + " Build";
#endif
#if __ANDROID__
            loginButtonGoogle.IsVisible = false;
#elif __IOS__
            loginButtonGoogle.IsVisible = false;
#else
            loginButtonGoogle.IsVisible = false;
#endif



        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
     
        }

        void OnUserNameEntryUnfocused(object sender, EventArgs e)
        {
            String username = usernameEntry.Text;

            Helpers.Settings.UserName = username;

        }

        async void OnFBLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPageFB());
        }

        async void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPageGoogle());
        }

        async void OnLinkedInLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPageLinkedIn());
        }

        public void SetMessage(String message)
        {
            messageLabel.Text = message;
        }
    }
}
