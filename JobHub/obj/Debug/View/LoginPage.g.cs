﻿#pragma checksum "C:\Users\hkl\Documents\Visual Studio 2010\Projects\WindowsPhone_Application\WPDemo\JobHub\JobHub\View\LoginPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AFF81CE72484499B051FD5D2C2642FD4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace JobHub.View {
    
    
    public partial class LoginPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.PhoneTextBox tb_name;
        
        internal System.Windows.Controls.PasswordBox tb_password;
        
        internal System.Windows.Controls.ProgressBar pb_login;
        
        internal System.Windows.Controls.CheckBox ts_isShare;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbar_btn_login;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/JobHub;component/View/LoginPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tb_name = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("tb_name")));
            this.tb_password = ((System.Windows.Controls.PasswordBox)(this.FindName("tb_password")));
            this.pb_login = ((System.Windows.Controls.ProgressBar)(this.FindName("pb_login")));
            this.ts_isShare = ((System.Windows.Controls.CheckBox)(this.FindName("ts_isShare")));
            this.appbar_btn_login = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbar_btn_login")));
        }
    }
}

