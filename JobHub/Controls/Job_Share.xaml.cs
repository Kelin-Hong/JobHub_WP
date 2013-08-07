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
using Microsoft.Phone.Tasks;
using JobHub.Dal;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using JobHub.API.Model;
using GalaSoft.MvvmLight.Threading;

namespace JobHub.Controls
{
    public partial class Job_Share : UserControl
    {
        public DetailJob job;
        public string shareText;
        public string weiShareText;
        public Job_Share()
        {
            InitializeComponent();
            job = (App.Current as App).CurrentJob;
            List<ShareItem> ShareItemList = new List<ShareItem>();
            ShareItemList.Add(new ShareItem("短信分享", ""));
            ShareItemList.Add(new ShareItem("邮件分享", ""));
            ShareItemList.Add(new ShareItem("微博分享", ""));
            ShareListBox.ItemsSource = ShareItemList;
            shareText=job.CompanyName+Environment.NewLine+job.University+Environment.NewLine
                +job.AddressDetail+Environment.NewLine
                +job.StartTime+Environment.NewLine+"来自：校园招聘(http://www.ybole.com/xyzp)";
            weiShareText = job.CompanyName + "  "+ job.University + " "
                + job.AddressDetail + " "
                + job.StartTime + " " + "来自：校园招聘(http://www.ybole.com/xyzp)";
          
        }

        private void ShareListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send<string>("", "JobDetailShareCompleted");
            if (ShareListBox.SelectedIndex == 0)
            {
                SmsComposeTask task = new SmsComposeTask();
                task.Body = shareText;
                task.Show();
            }
            else if(ShareListBox.SelectedIndex==1)
            {
                EmailComposeTask task = new EmailComposeTask();
                task.Body = shareText;
                task.Show();
            }
            else if (ShareListBox.SelectedIndex == 2)
            {
                if (!(bool)Isolated.Get("IsSinaLogin"))
                {
                    Messenger.Default.Send<string>("", "NavigateToLoginPage");
                    //if (Application.Current.RootVisual is Page)
                    //{
                    //    ((Page)(Application.Current.RootVisual)).NavigationService.Navigate(uri);
                    //}
                }
                else
                {
                    SinaShareDal.Instance.SinaTextShare(weiShareText, () =>
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            {
                                MessageBox.Show("分享成功");
                            });
                    });
                }
            }
        }
       
    }
   public class ShareItem
    {
        public ShareItem(string name,string imageUrl)
        {
            Name = name;
            imageUrl = ImageUrl;
        }
        public string Name { set; get; }
        public string ImageUrl { set; get; }
    }
}
