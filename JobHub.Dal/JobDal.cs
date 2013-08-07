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
using JobHub.API.Api;
using System.Collections.Generic;
using JobHub.API.Model;
using JobHub.Dal.db;
using System.Linq;
using System.Collections;
namespace JobHub.Dal
{
    public class JobDal:BaseDal
    {
        private static JobDal dal = new JobDal();
        /// <summary>
        /// 实例
        /// </summary>
        
        public static JobDal Instance
        {
            get
            {
                return dal;
            }
        }

        #region 私有变量

        /// <summary>
        /// API
        /// </summary>
        readonly JobApi _Api = new JobApi();

        #endregion
        /// <summary>
        /// 获取制定用户的听众列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="startindex"></param>
        /// <param name="callback"></param>
        public void GetJobslist(long time, string city,string university,Action<ICollection<Job>> callback)
        {
            if (!networkIsAvailable)
            {

            }
            else
            {
                _Api.GetJobslist(time, city, university, (rs) =>
                {
                    var us = new List<Job>();
                    if (rs.Data != null)
                    {
                        us = rs.Data;
                    }
                    callback(us);
                 // SavaJobsToDatabase(us);
                });
            }
         
        }
        public void GetJobDetail(string jobId,Action<string> callback)
        {
            _Api.GetJobDetail(jobId, (html) =>
                {
                    callback(html);
                });
        }
        public JobTable GetJobById(string jobId)
        {
            try
            {
                //wait for my turn
                OperationOnDatabase.WaitOne();

                //interact with the database
                //use 'ToArray' to make sure we are indeed done
                //with our DataContext before we continue
                return db.jobTable.FirstOrDefault(c => c.Job_ID.Equals(jobId));
            }
            finally
            {
                //always give my turn away when done.
                OperationOnDatabase.Set();
            }
        }
        private void SavaJobsToDatabase(List<Job> jobs)
        {
            db.jobTable.DeleteAllOnSubmit(db.jobTable.ToList<JobTable>());
            foreach (Job job in jobs)
            {
                if (db.jobTable.Any(c => c.Job_ID.Equals(job.Id))) continue;
               JobTable jobItem= new JobTable() 
               {
                    Job_ID = job.Id, 
                    Job_AddressCity = job.Address.City,
                    Job_AddressDetail=job.Address.Detail,
                 //   Job_AddressLatitude=job.Address.Accurate.Latitude,
                   // Job_AddressLongitude=job.Address.Accurate.Longitude,
                    Job_CompanyId=job.Company.Id,
                    Job_CompanyName=job.Company.Name,
                    Job_Url=job.Url,
                     
                    Job_StartTime=job.StartTime,
                  //  Job_EndTime=job.EndTime+"",
                 Job_IsCollection=false,
                 Job_IsRemind=false,
                 Job_University=job.Address.University,
                 };
                if(job.Address.Accurate!=null)
                {
                     jobItem.Job_AddressLatitude=job.Address.Accurate.Latitude;
                     jobItem.Job_AddressLongitude = job.Address.Accurate.Longitude;
                 }
                try
                {
                    //wait for my turn
                    OperationOnDatabase.WaitOne();

                    //interact with the database
                    //use 'ToArray' to make sure we are indeed done
                    //with our DataContext before we continue
                    db.jobTable.InsertOnSubmit(jobItem);
                }
                finally
                {
                    //always give my turn away when done.
                    OperationOnDatabase.Set();
                }
             //  if(!db.jobTable.Contains<JobTable>(jobItem,new JobEqualityComparer()))
              
          }
   //         int count = db.jobTable.Count();
            db.SubmitChanges();
      //      count = db.jobTable.Count();
        }


      
    }
    class JobEqualityComparer : IEqualityComparer<JobTable>
    {

        public bool Equals(JobTable j1, JobTable j2)
        {
            if (j1.Job_ID == j2.Job_ID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int GetHashCode(JobTable obj)
        {
            return obj.GetHashCode();
        }
    }
}
