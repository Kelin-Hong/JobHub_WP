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
using GalaSoft.MvvmLight.Command;
using JobHub.Dal;
using JobHub.Dal.db;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Diagnostics;
using JobHub.API.Model;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Threading;
namespace JobHub.ViewModel
{
    public class JobDetailViewModel:ViewModelBase
    {
        //RelayCommand<string> JobDetailLoadedCommand { set; get; }
        private Boolean isValueChange = false;
        public ObservableCollection<RelativeWeibo> RelativeWeiboList { set; get; }
        public ObservableCollection<StatusTile> StatusList { set; get; }
        public RelayCommand ChangeStatusCommand { set; get; }
        public RelayCommand<StatusTile> StatusChangeCompletedCommand { set; get; }
        public RelayCommand UpdateCollectJobDataCommand { set; get; }
        public int currentPage = 1;
        DetailJob currentJob;
        public JobDetailViewModel()
        {
            currentJob = (App.Current as App).CurrentJob;
            ObservableCollection<StatusTile> statusList = IsolateFile.Get<ObservableCollection<StatusTile>>("statusList.xml");
            RelativeWeiboList = new ObservableCollection<RelativeWeibo>();
            
            if (statusList != null)
            {
                StatusList = statusList;
            }
            else
            {
                StatusList = new ObservableCollection<StatusTile>();
                StatusList.Add(new StatusTile("宣讲会", "#e5927d"));
                StatusList.Add(new StatusTile("网申", "#e5927d"));
                StatusList.Add(new StatusTile("笔试", "#e97c65"));
                StatusList.Add(new StatusTile("一面", "#5db5e6"));
                StatusList.Add(new StatusTile("二面", "#5db5e6"));
                StatusList.Add(new StatusTile("三面", "#5db5e6"));
                StatusList.Add(new StatusTile("拿Offer", "#77c47f"));
                StatusList.Add(new StatusTile("添加", "#77c47f"));
                IsolateFile.Set<ObservableCollection<StatusTile>>("statusList.xml", StatusList);
            }
            ObservableCollection<StatusTile> stList = IsolateFile.Get<ObservableCollection<StatusTile>>("statusList.xml");
            Messenger.Default.Register<string>(this, "SendJobToDetail",
              (empty) =>
              {
                  DetailJob job = (App.Current as App).CurrentJob;
                      Status = job.Status;
                      University = job.University;
                      Address =job.AddressDetail;
                      StartTime = job.StartTime;
                      CompanyName = job.CompanyName;
                      NoteText = job.NoteText;
                      //if (date == null && time=!= null)
                      {
                          Date = DateTime.Parse(StartTime).ToLongDateString();
                          Time = DateTime.Parse(StartTime).ToLongTimeString();
                      }
                     // Debug.WriteLine(CompanyName + "     " + Address);
                      JobDal.Instance.GetJobDetail(job.JobId,(html)=>
                          {
                              Detail=html;
                              if (!string.IsNullOrEmpty(html))
                              {
                                  Messenger.Default.Send<string>(Detail, "job_detail");
                              }
                              else
                                  Messenger.Default.Send<string>(job.Url, "job_detail");
                          });
                     
                  
              }
              );
            ChangeStatusCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<string>("", "changeStatus");
            });
            Messenger.Default.Register<string>(this, "RelativeWeiboListBoxRefresh",
              empty =>
              {
                  if (!string.IsNullOrEmpty(currentJob.OfficalWeibo))
                      WeiboDal.Instance.GetRelativeWeiboList(20,1, currentJob.OfficalWeibo, GetRelativeWeiboListEnd);

              }
              );

            Messenger.Default.Register<string>(this, "RelativeWeiboListBoxLoad",
             empty =>
             {
                 if (!string.IsNullOrEmpty(currentJob.OfficalWeibo))
                     WeiboDal.Instance.GetRelativeWeiboList(20, ++currentPage, currentJob.OfficalWeibo, GetRelativeWeiboListEnd);

             }
             );
            Messenger.Default.Register<string>(this, "CollectJob",
             jobId =>
             {
                 if (jobId != null)
                 {
                     // JobTable job = JobDal.Instance.GetJobById(jobId);
                     DetailJob job = (App.Current as App).CurrentJob;
                     CollectJobDal.Instance.Add(job);

                     Messenger.Default.Send<string>(job.JobId, "collectJobCompleted");
                 }
             }
             );
            Messenger.Default.Register<string>(this, "AddJobStatusCompleted",
              statusName =>
              {
                  StatusList.RemoveAt(statusList.Count - 1);
                  StatusList.Add(new StatusTile(statusName, "#5db5e6"));
                  StatusList.Add(new StatusTile("添加", "#77c47f"));
                  //int count = 0;
                  //if (Isolated.Get("StatusCount") != null) count = (int)Isolated.Get("StatusCount");
                  //Isolated.Set("StatusCount", count  + 1);
                  //Isolated.Set(count + "", statusName);
                  IsolateFile.Set<ObservableCollection<StatusTile>>("statusList.xml", StatusList);
                  Messenger.Default.Send<string>(statusName, "ChangeStatusCompleted");
              }
              );
            Messenger.Default.Register<string>(this, "UnCollectJob",
             jobId =>
             {
                 if (jobId != null)
                 {
                     // JobTable job = JobDal.Instance.GetJobById(jobId);
                     DetailJob job = (App.Current as App).CurrentJob;
                     CollectJobDal.Instance.Delete(job);
                     Messenger.Default.Send<string>(job.JobId, "UnCollectJobCompleted");
                 }
             }
             );
            Messenger.Default.Register<bool>(this, "UpdateCollectJobData",
              isCollected =>
              {
                  DetailJob job = (App.Current as App).CurrentJob;
                
                  if (isCollected && isValueChange)
                  {
                      isValueChange = false;
                      //(App.Current as App).isCurrentJobChange = false;
                      job.NoteText = NoteText;
                      job.Status = Status;
                      job.AddressDetail = Address;
                      job.University = University;
                      job.StartTime = Date.Substring(Date.IndexOf("年")+1)+ Time;
                      job.Date = Date;
                      job.Time = Time;
                      CollectJobDal.Instance.Update(job.JobId, Status, job.University,job.AddressDetail,job.StartTime,NoteText);
                      
                      Messenger.Default.Send<string>("", "UpdateCollectJobList");
                  }
                  address = null;
                  status = null;
                  noteText = null;
                  date = null;
                  time = null;
                  university = null;
              }
              );
            StatusChangeCompletedCommand = new RelayCommand<StatusTile>((item) =>
                {
                    if (item == null) return;
                    
                    Messenger.Default.Send<string>(item.Name, "ChangeStatusCompleted");
                    if (!item.Name.Equals("添加"))  Status = item.Name;
                    else
                    {
                        Messenger.Default.Send<string>("", "AddJobStatus");
                    }
                });

            if (!string.IsNullOrEmpty(currentJob.OfficalWeibo))
                WeiboDal.Instance.GetRelativeWeiboList(20, 1, currentJob.OfficalWeibo, GetRelativeWeiboListEnd);
           
        }
        private void GetRelativeWeiboListEnd(List<RelativeWeibo> list)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RelativeWeiboList.Clear();
                    foreach (RelativeWeibo item in list)
                    {
                        RelativeWeiboList.Add(item);
                    }
                });

        }
        private void GetAddRelativeWeiboListEnd(List<RelativeWeibo> list)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                foreach (RelativeWeibo item in list)
                {
                    RelativeWeiboList.Add(item);
                }
            });

        }
        private string status;
        public string Status
        {
            set
            {
                if (status!=value)
                {
                    if (status != null) isValueChange = true;
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
            get
            {
                return status;
            }
        }
        private string address;
        public string Address 
        {
            set
            {
                if (address != value)
                {
                    if (address != null) isValueChange = true;
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
            get
            {
             
                return address;
            }
        }
        public string UniversityAndAddress
        {
            
            get
            {

                return University + " " + Address;
            }
        }
        private string date;
        public string Date
        {
            set
            {
                if (date != value)
                {
                    if (date != null) isValueChange = true;
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
            get
            {

                return date;
            }
        }
        private string time;
        public string Time
        {
            set
            {
                if (time != value)
                {
                    if (time != null) isValueChange = true;
                    time = value;
                    OnPropertyChanged("Time");
                }
            }
            get
            {
                return time;
            }
        }
        public string StartTime { set; get; }
        public string Detail { set; get; }
        private string university;
        public string University
        {
            set
            {
                if (university != value)
                {
                    if (university != null) isValueChange = true;
                    university = value;
                    OnPropertyChanged("University");
                }
            }
            get
            {
                return university;
            }
        }
        private string noteText;
        public string NoteText 
        {
            set
            {
                if (noteText != value)
                {
                    if (noteText != null) isValueChange = true;
                    noteText = value;
                    OnPropertyChanged("NoteText");
                }
            }
            get
            {
                return  noteText;
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChangedHandler;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string CompanyName { get; set; }
    }
    [DataContract]
    public class StatusTile
    {
        public StatusTile(string name,string color)
        {
            this.Name = name;
            this.Color = color;
        }
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string Color { set; get; }
    }
}
