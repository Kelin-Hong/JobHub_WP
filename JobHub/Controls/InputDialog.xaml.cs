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
    public partial class InputDialog : UserControl
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>(tb_statusName.Text, "AddJobStatusCompleted");
            this.CloseMeAsPopup();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseMeAsPopup();
        }
    }
}
