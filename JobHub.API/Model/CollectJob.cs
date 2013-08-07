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
using JobHub.API.Util;
using System.ComponentModel;

namespace JobHub.API.Model
{
    public class CollectJob : INotifyPropertyChanged
    {
    
        public string  JobId { set; get; }
    
        public string CompanyName { set; get; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public string Url { set; get; }
        public string NoteText { set; get; }
        public string OfficalWeibo { set; get; }
        private string university;
        public string University
        {
            set
            {
                if (university != value)
                {
                  
                    university = value;
                    OnPropertyChanged("University");
                }
            }
            get
            {
                return university;
            }
        }
     
        private string addressDetail;
        public string AddressDetail
        {
            set
            {
                if (addressDetail != value)
                {

                    addressDetail = value;
                    OnPropertyChanged("AddressDetail");
                }
            }
            get
            {
                if (addressDetail.Length > 19) return addressDetail.Substring(0, 19) + "..";
                else
                    
                return addressDetail;
            }
        }

        
        private string status;
        public string Status
        {
            set
            {
                if (status != value)
                {

                    status = value;
                    OnPropertyChanged("Status");
                }
            }
            get
            {
                return status;
            }
        }
        private string startTime;
        public string StartTime
        {
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    OnPropertyChanged("StartTime");
                }
            }
            get
            {
                return startTime;
            }
        }
        
        public string Color
        {
            get
            {
                Random r = new Random();
                int i = r.Next(3);
                switch (i)
                {
                    case 0: return "#e5927d"; break;
                    case 1: return "#5db5e6"; break;
                    case 2: return "#77c47f"; break;
                }
                return "e5927d";
            }

        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
       

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
