using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace JobHub.API.Model
{
    public class DetailJob : INotifyPropertyChanged
    {
        public string  JobId { set; get; }
        public string CompanyName { set; get; }
        public string University { set; get; }
        public string AddressDetail { set; get; }
        public string Date { set; get; }
        public string Time { set; get; }
        public string StartTime{  get; set; }
        public bool IsPathAvailable { set; get; }
        public bool IsCollected { set; get; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public string Url { set; get; }
        public string Status { set; get; }
        public string NoteText { set; get; }
        public string OfficalWeibo { set; get; }
        public string CollectBtnText
        {
            get
            {
                if (IsCollected) return "取消收藏";
                else
                    return "添加收藏";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
