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
using System.Data.Linq.Mapping;

namespace JobHub.Dal.db
{
    [Table]
    public class FavouriteTable : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private int id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    OnPropertyChanging("Id");
                    id = value;
                    OnPropertyChanged("Id");
                }

            }
        }
        private string job_id;
        [Column]
        public string Job_ID
        {
            get { return job_id; }
            set
            {
                if (job_id != value)
                {
                    OnPropertyChanging("Job_ID");
                    job_id = value;
                    OnPropertyChanged("Job_ID");
                }

            }
        }
            private string job_detail;
            [Column]
            public string Job_Detail
            {
                get { return job_detail; }
                set
                {
                    if (job_detail != value)
                    {
                        OnPropertyChanging("Job_Detail");
                        job_detail = value;
                        OnPropertyChanged("Job_Detail");
                    }
                }
            }
            private string job_url;
            [Column]
            public string Job_Url
            {
                get { return job_url; }
                set
                {
                    if (job_url != value)
                    {
                        OnPropertyChanging("Job_Url");
                        job_url = value;
                        OnPropertyChanged("Job_Url");
                    }
                }
            }
         private string job_university;
            [Column]
            public string Job_University
            {
                get { return job_university; }
                set
                {
                    if (job_university != value)
                    {
                        OnPropertyChanging("Job_University");
                        job_university = value;
                        OnPropertyChanged("Job_University");
                    }
                }
            }
         private string job_companyid;
            [Column]
            public string Job_CompanyId
            {
                get { return job_companyid; }
                set
                {
                    if (job_companyid != value)
                    {
                        OnPropertyChanging("Job_CompanyId");
                        job_companyid = value;
                        OnPropertyChanged("Job_CompanyId");
                    }
                }
            }
         private string job_companyname;
            [Column]
            public string Job_CompanyName
            {
                get { return job_companyname; }
                set
                {
                    if (job_companyname != value)
                    {
                        OnPropertyChanging("Job_CompanyName");
                        job_companyname = value;
                        OnPropertyChanged("Job_CompanyName");
                    }
                }
            }
          private string job_companynameweiboid;
            [Column]
            public string Job_CompanyWeiboid
            {
                get { return job_companynameweiboid; }
                set
                {
                    if (job_companynameweiboid != value)
                    {
                        OnPropertyChanging("Job_CompanyWeiboid");
                        job_companynameweiboid = value;
                        OnPropertyChanged("Job_CompanyWeiboid");
                    }
                }
            }

          private string job_addresscity;
            [Column]
            public string Job_AddressCity
            {
                get { return job_addresscity; }
                set
                {
                    if (job_addresscity != value)
                    {
                        OnPropertyChanging("Job_AddressCity");
                        job_addresscity = value;
                        OnPropertyChanged("Job_AddressCity");
                    }
                }
            }
          private string job_adddressdetail;
            [Column]
            public string Job_AddressDetail
            {
                get { return job_adddressdetail; }
                set
                {
                    if (job_adddressdetail != value)
                    {
                        OnPropertyChanging("Job_AddressDetail");
                        job_adddressdetail = value;
                        OnPropertyChanged("Job_AddressDetail");
                    }
                }
            }
          private double job_adddresslatitude;
            [Column]
            public double Job_AddressLatitude
            {
                get { return job_adddresslatitude; }
                set
                {
                    if (job_adddresslatitude != value)
                    {
                        OnPropertyChanging("Job_AddressLatitude");
                        job_adddresslatitude = value;
                        OnPropertyChanged("Job_AddressLatitude");
                    }
                }
            }
            private double job_adddresslongitude;
            [Column]
            public double Job_AddressLongitude
            {
                get { return job_adddresslongitude; }
                set
                {
                    if (job_adddresslongitude != value)
                    {
                        OnPropertyChanging("Job_AddressLongitude");
                        job_adddresslongitude = value;
                        OnPropertyChanged("Job_AddressLongitude");
                    }
                }
            }
               private string job_starttime;
            [Column]
            public string Job_StartTime
            {
                get { return job_starttime; }
                set
                {
                    if (job_starttime != value)
                    {
                        OnPropertyChanging("Job_StartTime");
                        job_starttime = value;
                        OnPropertyChanged("Job_StartTime");
                    }
                }
            }

              private string job_endtime;
            [Column]
            public string Job_EndTime
            {
                get { return job_endtime; }
                set
                {
                    if (job_endtime != value)
                    {
                        OnPropertyChanging("Job_EndTime");
                        job_endtime = value;
                        OnPropertyChanged("Job_EndTime");
                    }
                }
            }
          private bool job_remind;
            [Column]
            public bool Job_IsRemind
            {
                get { return job_remind; }
                set
                {
                    if (job_remind != value)
                    {
                        OnPropertyChanging("Job_IsRemind");
                        job_remind = value;
                        OnPropertyChanged("Job_IsRemind");
                    }
                }
            }
        
           private string fav_status;
            [Column]
            public string Fav_Status
            {
                get { return fav_status; }
                set
                {
                    if (fav_status != value)
                    {
                        OnPropertyChanging("Fav_Status");
                        fav_status = value;
                        OnPropertyChanged("Fav_Status");
                    }
                }
            }
      
         private string fav_remarks;
            [Column]
            public string Fav_Remarks
            {
                get { return fav_remarks; }
                set
                {
                    if (fav_remarks != value)
                    {
                        OnPropertyChanging("Fav_Remarks");
                        fav_remarks = value;
                        OnPropertyChanged("Fav_Remarks");
                    }
                }
            }
            private string job_OfficalWeibo;
            [Column]
            public string Job_OfficalWeibo
            {
                get { return job_OfficalWeibo; }
                set
                {
                    if (job_OfficalWeibo != value)
                    {
                        OnPropertyChanging("Job_OfficalWeibo");
                        job_OfficalWeibo = value;
                        OnPropertyChanged("Job_OfficalWeibo");
                    }
                }
            }
            
            public event PropertyChangingEventHandler PropertyChanging;

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanging(string str)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(str));
                }
            }
            private void OnPropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        
    }
    
}

