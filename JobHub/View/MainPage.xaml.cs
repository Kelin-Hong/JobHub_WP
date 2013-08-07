using Microsoft.Phone.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using JobHub.View;
using Com.AMap.Maps.Api;
using Microsoft.Phone.Shell;
using GalaSoft.MvvmLight.Threading;
using JobHub.API.Model;
using System.Collections.ObjectModel;

namespace JobHub
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool isChangeCityVisible=false;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            AMapConfig.Key = "2b5d786b9df88425dd1aa3b863a5a1a2";
            
            Messenger.Default.Register<string>(this, "Navigate",
               uri =>
               {
                   if (uri != null)
                   {
                       NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                   }
               }
               );
            Messenger.Default.Register<string>(this, "NavigateToJobNewsDetail",
               uri =>
               {
                   if (uri != null)
                   {
                       NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                   }
               }
               );
            Messenger.Default.Register<string>(this, "NavigateToOtherPage",
              uri =>
              {
                  if (uri != null)
                  {
                      NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                  }
              }
              );
            Messenger.Default.Register<string>(this, "ChangeCityComplete",
              (s) =>
              {
                  Main_ChangeCity_Up.Begin();
                  isChangeCityVisible = !isChangeCityVisible;
              }
              );
            Messenger.Default.Register<string>(this, "JobLoadCompleted",
              (empty) =>
              {
                  DispatcherHelper.CheckBeginInvokeOnUI(() =>
                  {
                      tb_jobEmpty.Visibility = System.Windows.Visibility.Collapsed;
                      pb_loadJob.Visibility = System.Windows.Visibility.Collapsed;
                  });
              }
              );
            Messenger.Default.Register<string>(this, "JobEmpty",
             (empty) =>
             {
                 DispatcherHelper.CheckBeginInvokeOnUI(() =>
                     {
                         tb_jobEmpty.Visibility = System.Windows.Visibility.Visible;
                         pb_loadJob.Visibility = System.Windows.Visibility.Collapsed;
                     });
             }
             );
            
            Messenger.Default.Register<int>(this, "JobLoadCompleted",
            (count) =>
            {
               // this.jobListBox.ScrollIntoView
              //  this.jobListBox.mainPanel.ScrollIntoView(jobListBox.mainPanel.Items[count - 1]);
             //  this.jobListBox.UpdateLayout();
             //  this.jobListBox.mainPanel.UpdateLayout();
                jobListBox.IsLoading = false;
                
            }
            );
            Messenger.Default.Register<string>(this, "JobRefreshCompleted",
            (empty) =>
            {
                jobListBox.IsRefreshing = false;
            }
            );
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            collectionJobListBox.SelectedIndex = -1;
            jobListBox.mainPanel.SelectedIndex = -1;
            //jobListBox.IsHitTestVisible = true;
            
            jobNewsListBox.SelectedIndex = -1;
            moreItemListBox.SelectedIndex = -1;
            UmengSDK.UmengAnalytics.onPageStart("main_page");
            
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            UmengSDK.UmengAnalytics.onPageEnd("main_page");
            base.OnNavigatedFrom(e);
        }
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (isChangeCityVisible)
            {
                Main_ChangeCity_Up.Begin();
                isChangeCityVisible = !isChangeCityVisible;
                e.Cancel = true;
            }
            if (e.Cancel == false)
            {
                Exit();
            }
            base.OnBackKeyPress(e);

        }
        private void Exit()
        {
            while (NavigationService.CanGoBack)
                NavigationService.RemoveBackEntry();
            this.IsHitTestVisible = this.IsEnabled = false;
            
        }
        private void appbar_btn_change_city(object sender, System.EventArgs e)
        {
            Main_ChangeCity_Down.Begin();
            isChangeCityVisible = !isChangeCityVisible;
        }
        void MenuContent_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.FinalVelocities.LinearVelocity.Y < -500)
            {
                Main_ChangeCity_Up.Begin();

            }
        }

        void MenuContent_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Grid grid = sender as Grid;
            CompositeTransform transform = grid.RenderTransform as CompositeTransform;
            if (e.DeltaManipulation.Translation.Y < 0)
                transform.TranslateY += e.DeltaManipulation.Translation.Y;
            if (transform.TranslateY < 400)
            {
                Main_ChangeCity_Up.Begin();
            }
        }

        private void Main_JobItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string s = "";
            
        }

        private void appbar_share_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<string>("", "ShareJobList");
        }

        private void appbar_refresh_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<string>("", "Refresh");
        }

        private void moreItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (moreItemListBox.SelectedIndex == -1) return;
            moreItemListBox.SelectedIndex =-1;
        }

        private void jobListBox_RefreshRequested(object sender, EventArgs e)
        {
            if (!jobListBox.IsLoading)
            {
                jobListBox.IsRefreshing = true;
                
                Messenger.Default.Send<string>("", "JobListBoxRefresh");
                
            }
        }

        private void jobListBox_LoadRequested(object sender, EventArgs e)
        {
            if (!jobListBox.IsRefreshing)
            {
                jobListBox.IsLoading = true;
                Messenger.Default.Send<string>("", "JobListBoxLoad");
            }
           
        }

        private void jobListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (jobListBox.mainPanel.SelectedIndex == -1) return;
            Job job = jobListBox.mainPanel.SelectedItem as Job;
            if (job == null) return;
           // jobListBox.s
           Messenger.Default.Send<Job>(job, "JobListSelectionChange");
           
        }

       

        
       
    }
}
