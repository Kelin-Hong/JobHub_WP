using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using JobHub.API.Api;
namespace JobHub.View
{
    public partial class OauthPage : PhoneApplicationPage
    {

        public OauthPage()
        {
            InitializeComponent();
           // webBrowser.
            //this.Visibility=System.Windows.Visibility.
            webBrowser.IsScriptEnabled = true;
           // webBrowser.Visibility = Visibility.Collapsed;
            webBrowser.Navigate(new Uri(JobApi.URL_LOGIN));
            webBrowser.Navigating += new EventHandler<NavigatingEventArgs>(webBrowser_Navigating);
        }

        void webBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.ToString().StartsWith(JobApi.URL_CALLBACK))
            {
                //e.Cancel = true;
             //   webBrowser.Navigate(e.Uri);
                var request = (HttpWebRequest)WebRequest.Create(e.Uri);
                
               // CookieContainer myCookieContainer = request.CookieContainer;
                //CookieCollection c = myCookieContainer.GetCookies(new Uri(JobApi.JOBHUB_DOMAIN));
            }
        }

        private void webBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.ToString().StartsWith(JobApi.URL_CALLBACK))
            {
                //在WebBrowser中登录cookie保存在WebBrowser.Document.Cookie中     
                CookieContainer myCookieContainer = new CookieContainer();

                //String 的Cookie　要转成　Cookie型的　并放入CookieContainer中
                CookieCollection collection = webBrowser.GetCookies();
            }
        }
    }
}