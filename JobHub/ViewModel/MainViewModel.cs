using GalaSoft.MvvmLight;
using JobHub.Model;
using JobHub.API.Model;
using System.Collections.Generic;
using System.Windows.Threading;
using JobHub.Dal;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

using System.Device.Location;
using Microsoft.Phone.Shell;
using JobHub.Helper;
using System.Windows;
using System.Linq;
using UmengSDK;
namespace JobHub.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        
        public ObservableCollection<Citys> HotCityList
        {
            set;
            get;
        }
        public ObservableCollection<Job> JobsList
        {
            set;
            get;
        }
        public ObservableCollection<string> UList
        {
            set;
            get;
        }
        public ObservableCollection<CollectJob> CollectJobList
        {
            set;
            get;
        }
        public ObservableCollection<JobNews> JobNewsList
        {
            set;
            get;
        }
        private string currentUnivesity;
        public string CurrentUniversity { set; get; }
        private string currentCity;
        public string CurrentCity
        {
            set
            {
                if (currentCity != value)
                {
                    currentCity = value;
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            OnPropertyChanged("CurrentCity");
                        });
                }
            }
            get
           {
             return currentCity;
           }
        }
        public RelayCommand<Job> SelectJobCommand { get; set; }
        public RelayCommand<Citys> SelectCityCommand { set; get; }
        public RelayCommand<int> ChangeSchoolCommand { set; get; }
        public RelayCommand<CollectJob> SelectCollectJobCommand { set; get; }
        public RelayCommand<JobNews> SelectJobNewsCommand { set; get; }
        public RelayCommand<int> SelectMoreItemCommand { set; get; }
        private void NavigatedJobDetail(Job job)
        {
            if (job == null) return;
            foreach (CollectJob collectJob in CollectJobList)
            {
                if (collectJob.JobId.Equals(job.Id))
                {
                    NavigatedJobDetail(collectJob);
                    return;
                }
            }
            DetailJob detailJob = new DetailJob()
            {
                AddressDetail = job.Address.Detail,
                CompanyName=job.Company.Name,
                IsCollected=false,
                JobId=job.Id,
                StartTime=job.StartTime,
                University=job.Address.University,
                Url=job.Url,
                Status="宣讲会",
                OfficalWeibo=job.Company.OfficialWeibo,
                NoteText=""
            };
            if (job.Address.Accurate != null)
            {
                detailJob.Longitude = job.Address.Accurate.Longitude;
                detailJob.Latitude = job.Address.Accurate.Latitude;
                detailJob.IsPathAvailable = true;
            }
            else
            {
                detailJob.IsPathAvailable = false;
            }
           
            (App.Current as App).CurrentJob = detailJob;
            Messenger.Default.Send<string>(@"/View/JobDetailPage.xaml?jobId=" + job.Id, "Navigate");
        }
        private void NavigatedJobDetail(CollectJob job)
        {
            if (job == null) return;
            DetailJob detailJob = new DetailJob()
            {
                AddressDetail = job.AddressDetail,
                CompanyName = job.CompanyName,
                IsCollected = true,
                JobId = job.JobId,
                StartTime = job.StartTime,
                University = job.University,
                Latitude=job.Latitude,
                Longitude=job.Longitude,
                Url=job.Url,
                Status=job.Status,
                NoteText=job.NoteText,
                OfficalWeibo=job.OfficalWeibo
            };
            if (!double.IsNaN(job.Latitude)) detailJob.IsPathAvailable = true;
            else
                detailJob.IsPathAvailable = false;
            (App.Current as App).CurrentJob = detailJob;
            Messenger.Default.Send<string>(@"/View/JobDetailPage.xaml?jobId=" + job.JobId, "Navigate");
        }
        private long getCurrentTime()
        {
            DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
            long time = (long)javaSpan.TotalMilliseconds / 1000;

            //DateTime dateTime = new DateTime(time);
            //DateTime dateTime1 = new DateTime(javaSpan.Ticks);

            return time;
        }
        private void OnChangeCity(Citys city)
        {
            //JobsList.Clear();
           // UList.Clear();
            Isolated.Set("CurrentCity", city.City);
            JobDal.Instance.GetJobslist(getCurrentTime(), city.City, null, GetJobListEnd);
            CityDal.Instance.GetCityUniversity((string)Isolated.Get("CurrentCity"), GetCityUniversityEnd);
           
            Messenger.Default.Send<string>(city.City, "ChangeCityComplete");
        }
        private void OnChangeSchool(string city,string university)
        {
           // JobsList.Clear();
            if (university!=null&&!university.Equals("全部学校"))
            {
                CurrentUniversity = university;
                JobDal.Instance.GetJobslist(getCurrentTime(), city, university, GetJobListEnd);
                Isolated.Set("CurrentUniversity", university);
            }
            else
                JobDal.Instance.GetJobslist(getCurrentTime(), city, null, GetJobListEnd);
            
        
        }
        //public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChangedHandler;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //string c = (string)Isolated.Get("CurrentCity");
           // string cookie = (string)Isolated.Get("cookiesName");
            JobsList=new ObservableCollection<Job>();
            HotCityList = new ObservableCollection<Citys>();
            UList = new ObservableCollection<string>();
            JobNewsList = new ObservableCollection<JobNews>();
            CollectJobList = new ObservableCollection<CollectJob>(CollectJobDal.Instance.GetCollectJobList());
            CityDal.Instance.GetHotCitys(GetHotCityListEnd);
            JobNewsDal.Instance.GetJobNewsList(getCurrentTime(), GetJobNewsListEnd);
           
            
            var immediate = new ImmediateLocation(x =>{Get_CityNameOnMap(x);});
            immediate.GetLocation();

          
            SelectJobCommand = new RelayCommand<Job>((job) => NavigatedJobDetail(job));
            SelectCollectJobCommand = new RelayCommand<CollectJob>((job)=>NavigatedJobDetail(job));
            SelectCityCommand = new RelayCommand<Citys>((city) => OnChangeCity(city));
            SelectJobNewsCommand = new RelayCommand<JobNews>((jobNews) =>
            {
                if(jobNews!=null)
                Messenger.Default.Send<string>(@"/View/JobNewsDetailPage.xaml?id=" + jobNews.Id+"&name="+jobNews.Title, "NavigateToJobNewsDetail");
            });
            SelectMoreItemCommand = new RelayCommand<int>((index) => DealWithMoreItem(index));
                
            ChangeSchoolCommand = new RelayCommand<int>((i) =>
            {
                string city = (string)Isolated.Get("CurrentCity");
                if (i>=0&&i<UList.Count&&!string.IsNullOrEmpty(UList[i])) 
                {
                    OnChangeSchool(city, UList[i]);
                }
            });
            Messenger.Default.Register<string>(this, "collectJobCompleted",
            jobId =>
            {
                if (jobId != null)
                {
                    CollectJobList.Add(CollectJobDal.Instance.GetNewInsertCollectJobItem(jobId));
                    //foreach(Job job in JobsList)
                    //    if(job.Id.Equals(jobId))
                    //    {
                    //        JobsList.Remove(job);
                    //        break;
                    //    }
                }
            }
            );
            Messenger.Default.Register<string>(this, "UpdateCollectJobList",
            empty =>
            {
                if (empty != null)
                {
                    DetailJob detailJob=(App.Current as App).CurrentJob;
                    foreach(CollectJob job  in CollectJobList)
                        if (job.JobId.Equals(detailJob.JobId))
                        {
                            job.AddressDetail = detailJob.AddressDetail;
                            job.StartTime = detailJob.StartTime;
                            job.Status = detailJob.Status;
                            job.University = detailJob.University;
                            job.NoteText = detailJob.NoteText;
                        }
                }
            }
            );
            Messenger.Default.Register<string>(this, "UnCollectJobCompleted",
            jobId =>
            {
                if (jobId != null)
                {
                    DetailJob detailJob = (App.Current as App).CurrentJob;
                    foreach (CollectJob job in CollectJobList)
                    {
                        if (detailJob.JobId.Equals(job.JobId))
                        {
                            CollectJobList.Remove(job);
                            break;
                        }
                    }
                }
            }
            );
            Messenger.Default.Register<string>(this, "Refresh",
            empty =>
            {
                JobDal.Instance.GetJobslist(getCurrentTime(), CurrentCity, null
                 , GetJobListEnd);
                JobNewsDal.Instance.GetJobNewsList(getCurrentTime(), GetJobNewsListEnd);
            }
            );
            Messenger.Default.Register<string>(this, "SearchCityCompleted",
           cityName =>
           {
               OnChangeCity(new Citys() { City = cityName, Count = "0"});
           }
           );
           Messenger.Default.Register<string>(this, "ShareJobList",
           empty =>
           {
               string shareContent = "大家快来关注";
               if(CurrentCity!=null)
                   shareContent += CurrentCity;
               if (CurrentUniversity != null)
                   shareContent += " " + CurrentUniversity;
                 shareContent+="的宣讲会吧！(来自 @歪伯乐校园招聘 http://www.ybole.com/xyzp";
               SinaShareDal.Instance.SinaShareWithCityAndUniversity(CurrentCity, CurrentUniversity, shareContent, () =>
                   {
                       DispatcherHelper.CheckBeginInvokeOnUI(() =>
                           {
                               MessageBox.Show("宣讲会分享成功");
                           });
                   });
           }
           );
           Messenger.Default.Register<string>(this, "JobListBoxRefresh",
       empty =>
       {
           JobDal.Instance.GetJobslist(getCurrentTime(), CurrentCity, CurrentUniversity
                 , GetJobListEnd);
       }
       );
           Messenger.Default.Register<string>(this, "JobListBoxLoad",
       empty =>
       {
           JobDal.Instance.GetJobslist(JobsList[JobsList.Count - 1].startTime, CurrentCity, CurrentUniversity
                 , GetAddJobListEnd);
       }
       );
           Messenger.Default.Register<Job>(this, "JobListSelectionChange",
   job =>
   {
       SelectJobCommand.Execute(job);
   }
   );
       }


        private void DealWithMoreItem(int index)
        {
            if (index == -1) return;
            switch (index)
                    {
                        case 0: Isolated.Remove("IsSinaLogin"); Messenger.Default.Send<string>(@"/View/LoginPage.xaml", "NavigateToOtherPage"); break;
                        case 1: var result=MessageBox.Show("您确定删除所以数据吗", "提示", MessageBoxButton.OKCancel);
                                if(result==MessageBoxResult.OK) {};  break;
                        case 2: Messenger.Default.Send<string>(@"/View/AboutJobHubPage.xaml", "NavigateToOtherPage"); break;
                        case 3: Messenger.Default.Send<string>(@"/View/PrivacyPage.xaml", "NavigateToOtherPage"); break;
                        case 4: Messenger.Default.Send<string>(@"/View/FeekBackPage.xaml", "NavigateToOtherPage"); break;
                        case 5: UmengAnalytics.update("4fa79487527015184e00003c"); break;
                    };
           
        }
        /// <summary>
        /// 获取当前城市名称
        /// </summary>
        /// <returns></returns>
       public void Get_CityNameOnMap(GeoCoordinate coord)
        {
            double[] latLong = new double[2];
            if (coord.IsUnknown != true)
            {
                latLong[0] = coord.Latitude;
                latLong[1] = coord.Longitude;
            }
            (App.Current as App).CurrentLocation = new Com.AMap.Maps.Api.BaseTypes.MLngLat(coord.Longitude, coord.Latitude);
           CityDal.Instance.GetCurrentCity(latLong, GetCurrentCityEnd);
            //MapAndLocationHelper.GetCurrentCity(latLong, GetCurrentCityEnd);
        }

       private void GetCurrentCityEnd(string city)
       {
           CurrentCity = city;
           Isolated.Set("CurrentCity", CurrentCity);
           JobDal.Instance.GetJobslist(getCurrentTime(), CurrentCity, null
                , GetJobListEnd);
           CityDal.Instance.GetCityUniversity(CurrentCity, GetCityUniversityEnd);
       }

       private void GetJobNewsListEnd(List<JobNews> jobNewsList)
       {
           DispatcherHelper.CheckBeginInvokeOnUI(() =>
               {
                   JobNewsList.Clear();
                   foreach (JobNews item in jobNewsList)
                   {
                       JobNewsList.Add(item);
                   }
                 
               });
       }
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}

       /// <summary>
       /// 获取结束回调
       /// </summary>
       /// <param name="users"></param>
       private void GetAddJobListEnd(ICollection<Job> jobs)
       {
           //GalaSoft.MvvmLight.Threading.DispatcherHelper
           //            .CheckBeginInvokeOnUI(() =>

           DispatcherHelper.CheckBeginInvokeOnUI(() =>
           {
               Messenger.Default.Send<int>(JobsList.Count, "JobLoadCompleted");
               foreach (Job job in jobs)
               {
                   job.Color = JobsList.Count % 3 + "";
                   JobsList.Add(job);
               }
               if (JobsList.Count == 0)
                   Messenger.Default.Send<string>("", "JobEmpty");
              

           });



       }
        /// <summary>
        /// 获取结束回调
        /// </summary>
        /// <param name="users"></param>
        private void GetJobListEnd(ICollection<Job> jobs)
        {
            //GalaSoft.MvvmLight.Threading.DispatcherHelper
            //            .CheckBeginInvokeOnUI(() =>

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            {
                               
                                JobsList.Clear();
                                foreach (Job job in jobs)
                                {
                                    //Boolean flag = false;
                                    //foreach(CollectJob cjob in CollectJobList)
                                    //    if (cjob.JobId.Equals(job.Id))
                                    //    {
                                    //        flag = true;
                                    //        break;
                                    //    }
                                    // if(!flag)   
                                    job.Color = JobsList.Count % 3+"";
                                    JobsList.Add(job);

                                }
                               
                                if(JobsList.Count==0)
                                    Messenger.Default.Send<string>("", "JobEmpty");
                                else 
                                    Messenger.Default.Send<string>("", "JobRefreshCompleted");

                            });
                         
         
            
        }
        /// <summary>
        /// 获取结束回调
        /// </summary>
        /// <param name="users"></param>
        private void GetHotCityListEnd(ICollection<Citys> citys)
        {
            //GalaSoft.MvvmLight.Threading.DispatcherHelper
            //            .CheckBeginInvokeOnUI(() =>
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                foreach (Citys city in citys)
                {
                    HotCityList.Add(city);
                }

            });
        }
        /// <summary>
        /// 获取结束回调
        /// </summary>
        /// <param name="users"></param>
        private void GetCityUniversityEnd(List<string> us)
        {
            //GalaSoft.MvvmLight.Threading.DispatcherHelper
            //            .CheckBeginInvokeOnUI(() =>
            
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
              
                //for (int i = 0; i < UList.Count; i++)
                //    UList.RemoveAt(i);
                
                if(UList.Count==0) UList.Add("全部学校");
                while (UList.Count > 1) UList.RemoveAt(UList.Count-1);
                foreach (string u in us)
                {
                    UList.Add(u); 
                }
               
               
            });
        }
    }
}