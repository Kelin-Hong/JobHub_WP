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
using GalaSoft.MvvmLight.Messaging;

namespace JobHub.Controls
{
    public partial class TwoInputDialog : UserControl
    {
        public TwoInputDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string[] s = { tb_university.Text, tb_addressDetail.Text };
            Messenger.Default.Send<string[]>(s, "JobDetailChangeAddress");
            this.CloseMeAsPopup();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseMeAsPopup();
        }
    }
}
