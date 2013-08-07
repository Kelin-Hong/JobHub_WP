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
using JobHub.Dal;
using GalaSoft.MvvmLight.Threading;
using System.Text;

namespace JobHub.View
{
    public partial class JobNewsDetailPage : PhoneApplicationPage
    {
        public JobNewsDetailPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string id= NavigationContext.QueryString["id"] as string;
            string name = NavigationContext.QueryString["name"] as string;
            PageTitle.Text = name;
            JobNewsDal.Instance.GetJobNewsContent(id, (html) =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        webBrowser.NavigateToString(Unicode2HTML(html));
                    });
            });
        }
        public static string Unicode2HTML(string HTML)
        {
            StringBuilder str = new StringBuilder();
            char c;
            for (int i = 0; i < HTML.Length; i++)
            {
                c = HTML[i];
                if (Convert.ToInt32(c) > 127)
                {
                    str.Append("&#" + Convert.ToInt32(c) + ";");
                }
                else
                {
                    str.Append(c);
                }
            }
            return str.ToString();
        } 
    }
}