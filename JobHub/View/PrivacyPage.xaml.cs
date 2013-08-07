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

namespace JobHub.View
{
    public partial class PrivacyPage : PhoneApplicationPage
    {
        public PrivacyPage()
        {
            InitializeComponent();
            tbk_privacyText.Text = "    用户同意个人隐私信息是指那些能够对用户进行个人辨识或涉及个人通信的信息，包括下列信息：用户微博，手机通讯录，IP地址，GPS位置信息。而非个人隐私信息是指用户对本服务的操作状态以及使用习惯等一些明确且客观反映在”校园招聘”服务器端的基本记录信息和其他一切个人隐私信息范围外的普通信息；以及用户同意公开的上述隐私信息。" + Environment.NewLine +
   "尊重用户个人隐私信息的私有性是”校园招聘”的一贯制度，”校园招聘”将会采取合理的措施保护您的个人信息不被未经授权的访问、使用或泄漏。除法律或有法律赋予权限的政府部门要求或用户同意等原因外，”校园招聘”未经用户同意不向除合作单位以外的第三方公开、 透露用户个人隐私信息。 但是，用户在使用时选择同意，或用户与”校园招聘”及合作单位之间就用户个人隐私信息公开或使用另有约定的除外，同时用户应自行承担因此可能产生的任何风险，“校园招聘”对此不予负责。" + Environment.NewLine +
　　"如果有下列事项发生，我们不承担任何法律责任："+Environment.NewLine+
  "１．我们可能根据法律规定或相关政府的要求提供您的个人信息；" + Environment.NewLine +
  "２．由于您将用户密码告知他人或与他人共享注册帐户，由此导致的任何个人信息的泄漏，或其他非因”校园招聘”导致的个人信息的泄漏；" + Environment.NewLine +
  "３．任何由于黑客攻击、电脑病毒侵入或政府管制而造成的用户损失；" + Environment.NewLine +
  "４．因不可抗力导致的任何后果；" + Environment.NewLine +
  "５．用户以自己的独立判断从事与交友相关的行为，并独立承担可能产生的责任，“校园招聘”不承担任何法律责任； 	" + Environment.NewLine +
　　"我们保留视情形变化而不断更新隐私权政策的权利。";
        }
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            UmengSDK.UmengAnalytics.onPageEnd("privacy_page");
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UmengSDK.UmengAnalytics.onPageStart("privacy_page");

        } 
    }
}