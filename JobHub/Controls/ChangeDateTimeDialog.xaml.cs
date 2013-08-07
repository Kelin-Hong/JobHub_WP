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
    public partial class ChangeDateTimeDialog : UserControl
    {
        public ChangeDateTimeDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
           // Messenger.Default.Send<string>(tb_statusName.Text, "AddJobStatusCompleted");
          //this.CloseMeAsPopup();
            Messenger.Default.Send<string>("ok", "SetDateTimeCompleted");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //this.CloseMeAsPopup();
            Messenger.Default.Send<string>("cancel", "SetDateTimeCompleted");
        }

        private void datePicker_ValueChanged(object sender, Microsoft.Phone.Controls.DateTimeValueChangedEventArgs e)
        {
            
           
        }

        private void timePicker_ValueChanged(object sender, Microsoft.Phone.Controls.DateTimeValueChangedEventArgs e)
        {
            //Messenger.Default.Send<string>("", "ShowDateTimeDialog");
        }
    }
}
