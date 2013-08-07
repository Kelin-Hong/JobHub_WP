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
using JobHub.API.Model;
using JobHub.API.Api;
using GalaSoft.MvvmLight.Threading;
namespace JobHub.View
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();

            
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            UmengSDK.UmengAnalytics.onPageEnd("login_page");
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UmengSDK.UmengAnalytics.onPageStart("login_page");

        } 
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
          
            //SinaShareApi _api = new SinaShareApi();
            //_api.LoginShare((s) =>
            //{
            //    string ss = s;
            //});
        }
        private void getUserEnd(User user)
        {

            User u = user;
           
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    if (string.IsNullOrEmpty(u.Id))
                    {
                        MessageBox.Show("用户名或密码错误");
                        return;
                    }
                    Isolated.Set("IsSinaLogin", true);
                    pb_login.Visibility = Visibility.Collapsed;
                    this.IsHitTestVisible = true;
                    if (ts_isShare.IsChecked.Value)
                    {
                        SinaShareApi _api = new SinaShareApi();

                        _api.LoginShare((s) =>
                        {
                            string ss = s;
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                {
                                    MessageBox.Show("分享成功");
                                });
                        });
                    }
                    NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));
                });
            
            
        }

       

        private void appbar_btn_login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_name.Text.Trim()))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (string.IsNullOrEmpty(tb_password.Password.Trim()))
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            pb_login.Visibility = Visibility.Visible;
            this.IsHitTestVisible = false;
            LoginDal.Instance.GetUser(tb_name.Text, tb_password.Password, getUserEnd);
            
        }

        private void tb_name_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_name.BorderBrush = new SolidColorBrush(new Color() { A = 255, B = 0, G = 0, R = 0 });
        }

        private void tb_password_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_password.BorderBrush = new SolidColorBrush(new Color() { A = 255, B = 0, G = 0, R = 0 });
        }
    }
}