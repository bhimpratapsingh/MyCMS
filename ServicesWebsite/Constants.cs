using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesWebsite
{
    public class Constants
    {
        public const string WEBSITE_NAME = "MyCMS";

        //Dynamic website image folder (control panel)
        public static string IMAGE_FOLDER_URL = System.Configuration.ConfigurationManager.AppSettings["imageUrl"];
        public static string PDF_FOLDER_URL = System.Configuration.ConfigurationManager.AppSettings["pdfUrl"];

        //Enter tag line for your website
        public static string TAG_LINE = "Website tag line";

        //Social site URL
        public const string TWITTER_URL = "https://twitter.com/?lang=en";
        public const string FACEBOOK_URL = "https://www.facebook.com/";
        public const string LINKED_IN_URL = "https://www.linkedin.com/";
        public const string YOUTUBE_URL = "https://www.youtube.com/";


        //Email credentials for query form contact page (PLEASE REPLACE FROM_EMAIL AND TO_EMAIL WITH ACTUAL EMAIL ADDRESS)
        public const string QUERY_FORM_FROM_EMAIL = "FROM_EMAIL";
        public const string QUERY_FORM_FROM_EMAIL_PASSWORD = "FROM_EMAIL_PASSWORD";
        public const string QUERY_FORM_FROM_EMAIL_HOST_NAME = "HOST_NAME";  // For eg: smtp.gmail.com is host name for gmail.
        public const bool QUERY_FORM_FROM_EMAIL_ENABLE_SSL = true; //Enable SSL details can be taken from the email provider
        public const int QUERY_FORM_FROM_EMAIL_PORT = 587; //Port details can be taken from the email provider. Port 587 is used by gmail.

        public const string QUERY_FORM_TO_EMAIL = "TO_EMAIL";

    }
}