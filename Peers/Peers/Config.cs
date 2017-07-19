﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Peers
{




    public class Config
    {
        public static readonly string ENVIRONMENT_LOCAL = "LOCAL";
        public static readonly string ENVIRONMENT_DEV = "DEVELOPMENT";
        public static readonly string ENVIRONMENT_STAGING = "STAGING";
        public static readonly string ENVIRONMENT_PRODUCTION = "PRODUCTION";
        public static readonly string FacebookAppId = "494090304124486";
        public static readonly string GoogleAppId = "553148756781-beqc7jliv4n919rkq5utgoq7s8rbr06t.apps.googleusercontent.com"; // YIZZ_UIsJPm_1i0vhuqq2iYU
        public static readonly string LinkedInAppId = "86du6v9e9hj61x";
        public static readonly string LinkedInClientSecret = "PKa0CJMUgnVpftUo";


        public readonly static int APIVersion = 3;

        public static string BackgroundColor
        {
            get { return "#110e13"; }
        }

        public static string ButtonBackgroundColorFB
        {
            get { return "#3b5998";  }
        }

        public static string ButtonBackgroundColorGoogle
        {
            get { return "#dd4b39"; }
        }

        public static string ButtonBackgroundColorLinkedIn
        {
            get { return "#0077B5"; }
        }

        public static string BackgroundColorAlternate
        {
            get { return "#202836"; }
        }

        public static string ButtonBackgroundColor
        {
            get { return "#282932"; }
        }

        public static string ButtonTextColor
        {
            get { return "#f8f8f8"; }
        }

        public static string EntryBackgroundColor
        {
            get { return "#282932"; }
        }

        public static string HeaderBackgroundColor
        {
            get { return "#202836"; }
        }

        public static string IconTextColor
        {
            get { return "#f8f8f8"; }
        }
        public static string EntryTextColor
        {
            get { return "#f8f8f8"; }
        }

        public static string ItemTextColor
        {
            get { return "#efefef"; }
        }

        public static string ItemSeparatorColor
        {
            get { return "#808080"; }
        }
        public static string MessageTextColor
        {
            get { return "#a0a0a0"; }
        }



        public readonly static String ServicePath = "/api/";

#if __ENV_LOCAL__
        public readonly static String ServiceBase = "http://192.168.1.5:59911";
        public readonly static String AWSReceiptBucket = "peersimagesdev";
        public readonly static String AWSCognitoAWSCredential = "us-west-2:ebc4aa30-302c-49e8-b81f-777777777777";
        public readonly static String AndroidUpdateUrl = "http://market.android.com/details?id=com.squareteams.peers";
        public readonly static String IOSUpdateUrl = "https://itunes.apple.com/us/app/appnamehere/id11111111?ls=1&mt=8";
        public readonly static String EnvironmentName = ENVIRONMENT_LOCAL;

#elif __ENV_DEVELOPMENT__
        public readonly static String ServiceBase = "https://dev-peers.apporilla.com";
        public readonly static String AWSReceiptBucket = "peersimagesdev";
        public readonly static String AWSCognitoAWSCredential = "us-west-2:ebc4aa30-302c-49e8-b81f-777777777777";
        public readonly static String AndroidUpdateUrl = "http://market.android.com/details?id=com.squareteams.peers";
        public readonly static String IOSUpdateUrl = "https://itunes.apple.com/us/appappnamehere/id11111111?ls=1&mt=8";
        public readonly static String EnvironmentName = ENVIRONMENT_DEV;

#elif __ENV_STAGING__
        public readonly static String ServiceBase = "https://staging-peers.apporilla.com";
        public readonly static String AWSReceiptBucket = "peersimagesstaging";
        public readonly static String AWSCognitoAWSCredential = "us-west-2:6bae675d-03ee-4910-856e-777777777777";
        public readonly static String AndroidUpdateUrl = "http://market.android.com/details?id=com.squareteams.peers";
        public readonly static String IOSUpdateUrl = "https://itunes.apple.com/us/app/appnamehere/id11111111?ls=1&mt=8";
        public readonly static String EnvironmentName = ENVIRONMENT_STAGING;

#elif __ENV_PRODUCTION__
        public readonly static String ServiceBase = "https://peers.squareteams.com";
        public readonly static String AWSReceiptBucket = "peersimagesproduction";
        public readonly static String AWSCognitoAWSCredential = "us-west-2:4a975193-dafc-4839-af69-777777777777";
        public readonly static String AndroidUpdateUrl = "http://market.android.com/details?id=com.squareteams.peers";
        public readonly static String IOSUpdateUrl = "https://itunes.apple.com/us/app/appnamehere/id11111111?ls=1&mt=8";
        public readonly static String EnvironmentName = ENVIRONMENT_PRODUCTION;

#endif
        public readonly static String ServiceEndpoint = ServiceBase + ServicePath;



    }
}
