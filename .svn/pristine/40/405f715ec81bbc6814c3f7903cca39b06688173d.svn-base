using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Peers.Helpers
{


    static class StringUtil
    {

        public static String hashString(string raw)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(raw);
                byte[] hashBytes = sha1.ComputeHash(bytes);

                return HexStringFromBytes(hashBytes);
            }
        }

        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }


        public static int ComputeLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }

        public static Dictionary<String, String> ParseQueryString(String querystring)
        {
            var parmKeyValues = new Dictionary<string, string>();

            if (querystring.Length <= 1)
                return parmKeyValues;
            if (querystring[0] == '?')
                querystring = querystring.Substring(1);

            string[] items = querystring.Split('&');
            foreach (var i in items)
            {
                var kv = i.Split('=');
                if (kv.Length == 2)
                    parmKeyValues.Add(kv[0], kv[1]);
            }

            return parmKeyValues;
        }



    }




}
