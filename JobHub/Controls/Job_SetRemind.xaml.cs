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
using Microsoft.Phone.Scheduler;
using GalaSoft.MvvmLight.Messaging;
namespace JobHub.Controls
{
    public partial class Job_SetRemind : UserControl
    {
        string startTime;
        public Job_SetRemind()
        {
            InitializeComponent();
        }
        private void btn_finish_Tap(object sender, GestureEventArgs e)
        {
            startTime = (string)datePicker.Tag + (string)timePicker.Tag;
            DateTime date = (DateTime)datePicker.Value;
            DateTime time = (DateTime)timePicker.Value;
            DateTime beginTime = date + time.TimeOfDay;
            Reminder reminder = new Reminder(System.Guid.NewGuid()+"");
            reminder.Title = "校园招聘-提醒";
            reminder.Content = tb_company.Text + "  " + tb_status.Text + Environment.NewLine + tbox_address.Text + Environment.NewLine + startTime;
            reminder.BeginTime = beginTime;
            //reminder.ExpirationTime = expirationTime;
            //reminder.RecurrenceType = recurrence;
            //reminder.NavigationUri = navigationUri;

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);
            Messenger.Default.Send<string>("", "setRemindCompleted");
            
        }
    }
}
