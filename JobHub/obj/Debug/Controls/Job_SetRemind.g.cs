﻿#pragma checksum "C:\Users\hkl\Documents\Visual Studio 2010\Projects\WindowsPhone_Application\WPDemo\JobHub\JobHub\Controls\Job_SetRemind.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "82FD320C2FF1A754AE5328283A3F21D5"
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


namespace JobHub.Controls {
    
    
    public partial class Job_SetRemind : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock tb_company;
        
        internal System.Windows.Controls.TextBlock tb_status;
        
        internal Microsoft.Phone.Controls.DatePicker datePicker;
        
        internal Microsoft.Phone.Controls.TimePicker timePicker;
        
        internal System.Windows.Controls.TextBox tbox_address;
        
        internal System.Windows.Controls.Border btn_finish;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/JobHub;component/Controls/Job_SetRemind.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.tb_company = ((System.Windows.Controls.TextBlock)(this.FindName("tb_company")));
            this.tb_status = ((System.Windows.Controls.TextBlock)(this.FindName("tb_status")));
            this.datePicker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("datePicker")));
            this.timePicker = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("timePicker")));
            this.tbox_address = ((System.Windows.Controls.TextBox)(this.FindName("tbox_address")));
            this.btn_finish = ((System.Windows.Controls.Border)(this.FindName("btn_finish")));
        }
    }
}

