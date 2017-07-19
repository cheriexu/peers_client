using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace Peers.Helpers
{

    // login:
    // https://developer.xamarin.com/guides/xamarin-forms/web-services/authentication/oauth/
    // https://www.facebook.com/dialog/oauth?client_id=494090304124486&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html
    //
    // info about users
    // https://graph.facebook.com/v2.7/me?access_token=asdfasdfaf
    // https://graph.facebook.com/v2.7/me?fields=name,picture,cover,age_range,devices,email,first_name,last_name,gender,is_verified&access_token=asdfasdfaf

    class OAuthHelper
    {


        public static string GetQueryStringFromUrl(string url)
        {
            char[] qsStart = { '?', '#' };
            int pos = url.IndexOfAny(qsStart);
            if (pos > 0)
            {
                return url.Substring(pos + 1);
            }
            return string.Empty;
        }

        public static string GetAccessTokenFromUrl(string url)
        {
            string queryString = GetQueryStringFromUrl(url);
            if (queryString != string.Empty)
            {
                return ExtractAccessTokenQueryString(queryString);
            }
            return string.Empty;
        }

        public static string GetParmFromUrl(string url, string parm)
        {
            string queryString = GetQueryStringFromUrl(url);
            if (queryString != string.Empty)
            {
                return ExtractParmFromQueryString(queryString, parm);
            }
            return string.Empty;
        }


        public static string ExtractParmFromQueryString(string query, string parm)
        {
            if (query.Contains(parm))
            {
                var parms = StringUtil.ParseQueryString(query);
                return parms[parm];
            }
            return string.Empty;
        }

        public static string ExtractAccessTokenQueryString(string query)
        {
            return ExtractParmFromQueryString(query, "access_token");
        }

        public static async Task<T> GetProfileAsync<T>( string requestUrl)
        {
            
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);

            var userProfile = JsonConvert.DeserializeObject<T>(userJson);

            return userProfile;

        }



    }
}
