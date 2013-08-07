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
using System.ComponentModel;
using JobHub.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Tasks;
using GalaSoft.MvvmLight.Threading;
using JobHub.API.Model;
using Com.AMap.Maps.Api.BaseTypes;

using Microsoft.Phone.Shell;
using System.Text;
using JobHub.Controls;

namespace JobHub.View
{
    public partial class JobDetailPage : PhoneApplicationPage
    {
        private bool isChangeStatusVisible = false;
        private bool isSetRemindVisible = false;
        private bool isJobMapVisible = false;
        private bool isJobShareVisible = false;
        private bool isJobChangeDateTimeDialogVisible = false;
        ApplicationBarIconButton btn0;
        ApplicationBarIconButton btn2;
        DetailJob detailJob;

        public JobDetailPage()
        {
            InitializeComponent();
            detailJob= (App.Current as App).CurrentJob;
             btn0=  (ApplicationBarIconButton)ApplicationBar.Buttons[0];
             btn2=  (ApplicationBarIconButton)ApplicationBar.Buttons[2];
            btn2.Text = detailJob.CollectBtnText;
            btn0.IsEnabled = detailJob.IsPathAvailable;
            if (string.IsNullOrEmpty(detailJob.OfficalWeibo))
            {
                tb_relativeWeiboEmpty.Visibility = Visibility.Visible;
                relativeWeiboListBox.Visibility = Visibility.Collapsed;
            }
            
          
           
        }
        private void registerMessenger()
        {
            Messenger.Default.Send<string>("", "SendJobToDetail");
            Messenger.Default.Register<string>(this, "AddJobStatus",
           empty =>
           {
               PopupCotainer popupContainer = new PopupCotainer(this);
               popupContainer.Show(new InputDialog());
           }
           );
            Messenger.Default.Register<string>(this, "setRemindCompleted", (s) =>
            {
                Job_SetRemind_Up.Begin();
                isSetRemindVisible = !isSetRemindVisible;
            });
            Messenger.Default.Register<string>(this, "changeStatus", (s) =>
            {
                Job_ChangeStatus_Down.Begin();
                isChangeStatusVisible = !isChangeStatusVisible;
            });
            Messenger.Default.Register<string>(this, "ChangeStatusCompleted", (s) =>
            {
                Job_ChangeStatus_Up.Begin();
                isChangeStatusVisible = !isChangeStatusVisible;
                tbk_status.Text = s;
            });
            Messenger.Default.Register<string>(this, "JobDetailShareCompleted", (s) =>
            {
                Job_Share_Up.Begin();
                isJobShareVisible = !isJobShareVisible;

            });
            Messenger.Default.Register<string>(this, "NavigateToLoginPage", (s) =>
            {
                NavigationService.Navigate(new Uri("/View/LoginPage.xaml", UriKind.Relative));
            });
            Messenger.Default.Register<string[]>(this, "JobDetailChangeAddress", (s) =>
            {
                tbox_university.Text = s[0];
                tbox_address.Text = s[1];
            });
            Messenger.Default.Register<string>(this, "SetDateTimeCompleted", (msg) =>
            {
                if (msg.Equals("ok"))
                {
                    datePicker.Value = job_ChangeDateTimeDialog.datePicker.Value;
                    timePicker.Value = job_ChangeDateTimeDialog.timePicker.Value;
                }
                isJobChangeDateTimeDialogVisible = !isJobChangeDateTimeDialogVisible;
                Job_ChangeDateTimeDialog_Up.Begin();
                // PopupCotainer popupContainer = new PopupCotainer(this);

                //control.datePicker.Value = datePicker.Value;
                //control.timePicker.Value = timePicker.Value;

            });


            Messenger.Default.Register<string>(this, "job_detail", (s) =>
            {
                if (!s.StartsWith("http"))
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        webBrowser.NavigateToString(Unicode2HTML(s));
                        webBrowser.Visibility = Visibility.Visible;
                        btnSeeDetailInBrowser.Visibility = Visibility.Collapsed;
                    });
                }
                else
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {

                        webBrowser.Visibility = Visibility.Collapsed;
                        btnSeeDetailInBrowser.Visibility = Visibility.Visible;

                    });
                }
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
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
                base.OnNavigatingFrom(e);
                UmengSDK.UmengAnalytics.onPageEnd("jobdetail_page");
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            registerMessenger();
            UmengSDK.UmengAnalytics.onPageStart("jobdetail_page");
           
        } 
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (isChangeStatusVisible)
            {
                Job_ChangeStatus_Up.Begin();
                isChangeStatusVisible = !isChangeStatusVisible;
                e.Cancel = true;
            }
            if (isSetRemindVisible)
            {
                Job_SetRemind_Up.Begin();
                isSetRemindVisible = !isSetRemindVisible;
                e.Cancel = true;
            }
            if (isJobMapVisible)
            {
                Job_Map_Up.Begin();
                isJobMapVisible = !isJobMapVisible;
                e.Cancel = true;
            }
            if (isJobShareVisible)
            {
                Job_Share_Up.Begin();
                isJobShareVisible = !isJobShareVisible;
                e.Cancel = true;
            }
            if (isJobChangeDateTimeDialogVisible)
            {
                Job_ChangeDateTimeDialog_Up.Begin();
                isJobChangeDateTimeDialogVisible =! isJobChangeDateTimeDialogVisible;
                e.Cancel = true;
            }
            if (e.Cancel==false&&!btn2.Text.Equals("添加收藏"))
            {
                Messenger.Default.Send<bool>(true, "UpdateCollectJobData");
            }
            else
            {

            }
            base.OnBackKeyPress(e);
            
        }
        private void btn_change_status(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!detailJob.IsCollected)
            {
                if (MessageBox.Show("该宣讲会还未收藏，您在更改状态前是否先收藏", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Messenger.Default.Send<string>(detailJob.JobId, "CollectJob");
                    detailJob.IsCollected = true;
                    btn2.Text = "取消收藏";
                }
                else return;
            }
            Messenger.Default.Send<string>("", "changeStatus");
           // Job_ChangeStatus_Down.Begin();
            //isChangeStatusVisible = !isChangeStatusVisible;
        }

        private void btnSeeDetailInBrowser_Click(object sender, RoutedEventArgs e)
        {

            if (detailJob.Url != null)
            {
                WebBrowserTask webBrowserTask = new WebBrowserTask();
                webBrowserTask.Uri = new Uri(detailJob.Url, UriKind.RelativeOrAbsolute);
                webBrowserTask.Show();
            }
        }

        private void appbar_collecct_Click(object sender, EventArgs e)
        {
            if (!detailJob.IsCollected)
            {
                Messenger.Default.Send<string>(detailJob.JobId, "CollectJob");
                MessageBox.Show("收藏成功");
                btn2.Text = "取消收藏";
            }
            else
            {
                Messenger.Default.Send<string>(detailJob.JobId, "UnCollectJob");
                MessageBox.Show("取消收藏成功");
                btn2.Text = "添加收藏";
            }
            detailJob.IsCollected = !detailJob.IsCollected;
        }

     

        private void appbar_remind_Click(object sender, EventArgs e)
        {
            Job_SetRemind_Down.Begin();
            isSetRemindVisible = !isSetRemindVisible;
        }

        private void btn_time_edit_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!detailJob.IsCollected)
            {
                if (MessageBox.Show("该宣讲会还未收藏，您在更改时间前是否先收藏", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Messenger.Default.Send<string>(detailJob.JobId, "CollectJob");

                    detailJob.IsCollected = true;
                    btn2.Text = "取消收藏";
                }
                else return;
            }
            Job_ChangeDateTimeDialog_Down.Begin();
            isJobChangeDateTimeDialogVisible = !isJobChangeDateTimeDialogVisible;
            job_ChangeDateTimeDialog.datePicker.Value = datePicker.Value;
            job_ChangeDateTimeDialog.timePicker.Value = timePicker.Value;
             //popupContainer = new PopupCotainer(this);
           // ChangeDateTimeDialog control = new ChangeDateTimeDialog();
            //control.datePicker.Value =datePicker.Value;
            //control.timePicker.Value = timePicker.Value;
          //  popupContainer.Show(control);

           // datePicker.Focus();
           // timePicker.Focus();
           // datePicker.BorderThickness = new Thickness(1.5);
           // timePicker.BorderThickness = new Thickness(1.5);
            
        }

        private void datePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            //if (datePicker != null)
            //    datePicker.Value = e.NewDateTime;
           
        }

        private void timePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            TimePicker timepicker = (TimePicker)sender;
            //if (timepicker != null)
            //    timepicker.Value = e.NewDateTime;
        }

        private void btn_address_edit_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!detailJob.IsCollected)
            {
                if (MessageBox.Show("该宣讲会还未收藏，您在更改地址前是否先收藏", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Messenger.Default.Send<string>(detailJob.JobId, "CollectJob");

                    detailJob.IsCollected = true;
                    btn2.Text = "取消收藏";

                }
                else return;
            }
            PopupCotainer popupContainer = new PopupCotainer(this);
            TwoInputDialog control = new TwoInputDialog();
            control.tb_addressDetail.Text = tbox_address.Text;
            control.tb_university.Text = tbox_university.Text;
            popupContainer.Show(control);
           
        }

        

        private void appbar_path_Click(object sender, EventArgs e)
        {
            if (!double.IsNaN(detailJob.Latitude))
            {
                Job_Map_Down.Begin();
                isJobMapVisible = !isJobMapVisible;
                Messenger.Default.Send<MLngLat>(new MLngLat(detailJob.Longitude, detailJob.Latitude), "mLngLat");
            }
        }

        private void btn_write_note_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!detailJob.IsCollected)
            {
                if (MessageBox.Show("该宣讲会还未收藏，您在添加备忘前是否先收藏", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Messenger.Default.Send<string>(detailJob.JobId, "CollectJob");
                    detailJob.IsCollected = true;
                    btn2.Text = "取消收藏";
                }
                else return;
            }
            Image btn = (Image)sender;
            ((StackPanel)btn.Parent).Visibility = Visibility.Collapsed;
            tb_note.Visibility = Visibility.Visible;
            tb_note.Focus();
           
        }

        private void tb_note_LostFocus(object sender, RoutedEventArgs e)
        {
           // Isolated.Set("Note",tb_note.Text);
        }

        private void tb_note_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_note.Text))
            {
                sp_NoteEmpty.Visibility = Visibility.Collapsed;
                tb_note.Visibility = Visibility.Visible;
            }
            else
            {
                sp_NoteEmpty.Visibility = Visibility.Visible;
                tb_note.Visibility = Visibility.Collapsed;
            }
        }

        private void appbar_share_Click(object sender, EventArgs e)
        {
            Job_Share_Down.Begin();
            isJobShareVisible = !isJobShareVisible;
        }

        private void relativeWeiboListBox_RefreshRequested(object sender, EventArgs e)
        {
            Messenger.Default.Send<string>("", "RelativeWeiboListBoxRefresh");
        }

        private void relativeWeiboListBox_LoadRequested(object sender, EventArgs e)
        {
            Messenger.Default.Send<string>("", "RelativeWeiboListBoxLoad");
        }

       

        
    }
}