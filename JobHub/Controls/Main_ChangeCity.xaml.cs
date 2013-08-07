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
    public partial class Main_ChangeCity : UserControl
    {
        public Main_ChangeCity()
        {
            InitializeComponent();
        }

       

        private void btn_search_city_Tap(object sender, GestureEventArgs e)
        {
            Messenger.Default.Send<string>(tb_cityName.Text, "SearchCityCompleted");
        }
    }
}
