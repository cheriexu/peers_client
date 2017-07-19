using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Peers.Helpers
{
    public class RequestHelper
    {

        public static void AddRequestHeaders(WebClient webClient)
        {
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            //string authHeader = App.authManager.AuthorizationHeader;
            //if (!String.IsNullOrEmpty(authHeader))
            //    webClient.Headers[HttpRequestHeader.Authorization] = authHeader;
            var cookieHeader = App.sessionManager.GetActiveCookieString();
            if (cookieHeader.Length > 0)
                webClient.Headers.Add("Cookie", cookieHeader);

        }

        public static void AddRequestHeaders(HttpWebRequest webClient)
        {
            //string authHeader = App.authManager.AuthorizationHeader;
            //if (!String.IsNullOrEmpty(authHeader))
            //    webClient.Headers[HttpRequestHeader.Authorization] = authHeader;
            var cookieHeader = App.sessionManager.GetActiveCookieString();
            if (cookieHeader.Length > 0)
                webClient.Headers.Add("Cookie", cookieHeader);

        }

        // get request
        public static async Task<T> RequestData<T>(Uri uri)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            RequestHelper.AddRequestHeaders(request);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                var parsed = JsonConvert.DeserializeObject<T>(result);
                return parsed;
            }


/*
            using (var webClient = new WebClient())
            {
                RequestHelper.AddRequestHeaders(webClient);
                try
                {
                    var result = await webClient.DownloadString(uri);
                    var parsed = JsonConvert.DeserializeObject<T>(result);

                    var cookies = webClient.ResponseHeaders["Set-Cookie"];
                    if (cookies != null)
                        App.sessionManager.AddCookies(cookies);
                    return parsed;
                }
                catch (WebException we)
                {
                    if ((we.Response != null) && ((HttpWebResponse)we.Response).StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        LogoutAll();
                    }

                    throw we;
                }
            }
*/
        }

        public static async Task<T> RequestData<T>(Uri uri, string postData)
        {
            using (var webClient = new WebClient())
            {
                RequestHelper.AddRequestHeaders(webClient);
                try
                {
                    var result = await webClient.UploadStringTaskAsync(uri, "POST", postData);

                    //var data = JObject.Parse(result).SelectToken("d").ToString();   // remove wrapper
                    //var parsed = JsonConvert.DeserializeObject<T>(data);
                    var parsed = JsonConvert.DeserializeObject<T>(result);

                    var cookies = webClient.ResponseHeaders["Set-Cookie"];
                    if (cookies != null)
                        App.sessionManager.AddCookies(cookies);
                    return parsed;
                }
                catch (WebException we)
                {
                    if ((we.Response != null) && ((HttpWebResponse)we.Response).StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        LogoutAll();
                    }

                    throw we;
                }
            }
        }


        async public static void LogoutAll()
        {

            if (App.IsUserLoggedIn)
            {
                App.IsUserLoggedIn = false;
                var existingPages = App.mainPage.Navigation.NavigationStack.ToList();

                await App.mainPage.Navigation.PopToRootAsync();
                App.mainPage.Navigation.InsertPageBefore(new Pages.LoginSplash(), App.mainPage.Navigation.NavigationStack.First());
                await App.mainPage.Navigation.PopAsync();
            }
        }

    }
}
