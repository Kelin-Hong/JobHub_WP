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
using GalaSoft.MvvmLight.Threading;

namespace JobHub.View
{
    public partial class FeekBackPage : PhoneApplicationPage
    {
        public FeekBackPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            UmengSDK.UmengAnalytics.onPageEnd("feekback_page");
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UmengSDK.UmengAnalytics.onPageStart("feekback_page");

        }

        private void btn_commit_Click(object sender, RoutedEventArgs e)
        {
            JobHubApi api = new JobHubApi();
            api.PostFeedback(tb_feedback.Text, () =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            NavigationService.GoBack();
                        });
                });
        }

        private void tb_feedback_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_feedback.BorderBrush = new SolidColorBrush(new Color() { A = 255, B = 0, G = 0, R = 0 });
        } 
    } 
}