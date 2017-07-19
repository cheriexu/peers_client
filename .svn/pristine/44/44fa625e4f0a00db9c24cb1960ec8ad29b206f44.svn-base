using System;
using System.Collections.Generic;
using System.Text;
using Peers.Models;

namespace Peers.Helpers
{

    public class SessionManager
    {
        public Dictionary<String, Cookie> cookies;

        public SessionManager()
        {
            cookies = new Dictionary<string, Cookie>();
        }

        public void ClearCookies()
        {
            cookies.Clear();
        }

        /*
sample:

AuthManager.ParseCookie("asdf=vxtjYCN9h2CL6eFn7Lycbq8qBkxc/8PoVO1h2tAmH1E=; expires=Tue, 25-Oct-2016 07:00:00 GMT; path=/");
AuthManager.ParseCookie("ASP.NET_SessionId=o5ra1cfbstwpr3sd4vblopb3; path=/; HttpOnly");
         
         */

        public static Cookie ParseCookie(String value)
        {
            Cookie cookie = null;

            try
            {

                String[] items = value.Split(new String[] { "; " }, StringSplitOptions.None);
                if (items.Length > 0)
                    cookie = new Cookie();
                for (int i = 0; i < items.Length; i++)
                {
                    String[] keyValue = items[i].Split(new char[] { '=' }, 2);
                    if (keyValue.Length != 2)
                        continue;
                    if (i == 0)
                    {
                        cookie.Name = keyValue[0];
                        cookie.Value = keyValue[1];
                    }
                    else
                    {
                        if (keyValue[0] == "path")
                            cookie.Path = keyValue[1];
                        else if (keyValue[0] == "expires")
                        {
                            DateTime dt;
                            if (DateTime.TryParse(keyValue[1], out dt))
                            {
                                cookie.Expires = dt;
                            }

                        }

                    }
                }
            }
            catch (Exception e)
            {

            }

            return cookie;
        }

        public void AddCookies(string cookieString)
        {
            cookieString = cookieString.Replace(", ", "[comma] ");
            String[] cookieItems = cookieString.Split(',');
            foreach (var c in cookieItems)
            {
                string line = c.Replace("[comma] ", ", ");
                Cookie parsedCookie = ParseCookie(line);
                if (parsedCookie != null && !String.IsNullOrEmpty(parsedCookie.Name))
                    cookies[parsedCookie.Name] = parsedCookie;
            }
        }

        private static String GetCookieAsString(Cookie cookie)
        {
            return cookie.Name + "=" + cookie.Value;
        }

        public List<String> GetActiveCookies()
        {
            var cookie = new List<String>();

            if (cookies != null)
            {
                foreach (var c in cookies)
                {
                    if ((c.Value.Expires.HasValue && DateTime.Now < c.Value.Expires.Value) || !c.Value.Expires.HasValue)
                        cookie.Add(GetCookieAsString(c.Value));
                }
            }
            return cookie;
        }

        public String GetActiveCookieString()
        {
            StringBuilder cookie = new StringBuilder(100);

            if (cookies != null)
            {
                foreach (var c in cookies)
                {
                    if (cookie.Length > 0)
                        cookie.Append("; ");
                    if ((c.Value.Expires.HasValue && DateTime.Now < c.Value.Expires.Value) || !c.Value.Expires.HasValue)
                        cookie.Append(GetCookieAsString(c.Value));
                }
            }
            return cookie.ToString();
        }

    }
}
