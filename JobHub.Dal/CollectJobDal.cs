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
using JobHub.Dal.db;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using JobHub.API.Model;
namespace JobHub.Dal
{
    public class CollectJobDal : BaseDal
    {
        private static CollectJobDal collectJobDal=null;
        /// <summary>
        /// 实例
        /// </summary>
        public static CollectJobDal Instance
        {
            get
            {
                if (collectJobDal == null)
                {
                    collectJobDal = new CollectJobDal();
                }
                return collectJobDal;
            }
        }
        public CollectJob GetNewInsertCollectJobItem(string Id)
        {

            FavouriteTable j = db.favouriteTable.SingleOrDefault(c => c.Job_ID.Equals(Id));
            CollectJob job = new CollectJob()
            {
                AddressDetail = j.Job_AddressDetail,
                CompanyName = j.Job_CompanyName,
                StartTime = j.Job_StartTime,
                Status = j.Fav_Status,
                University = j.Job_University,
                JobId = j.Job_ID,
                Latitude=j.Job_AddressLatitude,
                Longitude=j.Job_AddressLongitude,
                NoteText=j.Fav_Remarks,
                Url=j.Job_Url,
                OfficalWeibo=j.Job_OfficalWeibo
            };
            return job;
        }
        public List<CollectJob> GetCollectJobList()
        {
             //IQueryable<FavouriteTable> query = 
             // from c in db.favouriteTable
             //where true
             //  select c;
            
            List<CollectJob> collectJobList = new List<CollectJob>();
            foreach (FavouriteTable j in db.favouriteTable)
            {
                CollectJob job = new CollectJob()
                {
                    AddressDetail=j.Job_AddressDetail,
                    CompanyName=j.Job_CompanyName,
                    StartTime=j.Job_StartTime,
                    Status=j.Fav_Status,
                    University=j.Job_University,
                    JobId=j.Job_ID,
                    Url=j.Job_Url,
                    Latitude=j.Job_AddressLatitude,
                    Longitude=j.Job_AddressLongitude,
                    NoteText=j.Fav_Remarks,
                    OfficalWeibo=j.Job_OfficalWeibo
                };
                collectJobList.Add(job);
            }
            return collectJobList;
           
        }
        public void Update(string id,string status, string university,string address, string time,string noteText)
        {
            FavouriteTable item= db.favouriteTable.SingleOrDefault(c=>c.Job_ID.Equals(id));
            item.Fav_Status = status;
            item.Job_University = university;
            item.Job_AddressDetail = address;
            item.Job_StartTime = time;
           
            item.Fav_Remarks = noteText;
            db.SubmitChanges();
        }
        public void Delete(DetailJob job)
        {
            db.favouriteTable.DeleteOnSubmit(db.favouriteTable.SingleOrDefault(c => c.Job_ID.Equals(job.JobId)));
            db.SubmitChanges();
        }
        public void Add(DetailJob job)
        {
            FavouriteTable favourtie = new FavouriteTable()
            {
                Job_ID = job.JobId,
                //Job_AddressCity = job.Address.City,
                Job_AddressDetail = job.AddressDetail,
                //Job_CompanyId = job.CompanyId,
                Job_CompanyName = job.CompanyName,
                Job_Url = job.Url,
                Job_StartTime = job.StartTime,
                Fav_Remarks=job.NoteText,
                Fav_Status = "宣讲会",
                Job_IsRemind = false,
                Job_University = job.University,
                Job_OfficalWeibo=job.OfficalWeibo
                //Job_ID = job.Job_ID,
                //Job_AddressCity = job.Job_AddressCity,
                //Job_AddressDetail = job.Job_AddressDetail,
                //Job_AddressLatitude = job.Job_AddressLatitude,
                //Job_AddressLongitude = job.Job_AddressLongitude,
                //Job_CompanyId = job.Job_CompanyId,
                //Job_CompanyName = job.Job_CompanyName,
                //Job_Url = job.Job_Url,
                //Job_StartTime = job.Job_StartTime,
                //Job_University = job.Job_University,
                //Job_CompanyWeiboid = job.Job_CompanyWeiboid,
                //Fav_Remarks = "",
                //Fav_Status = "宣讲会",
                //Job_Detail = job.Job_Detail,
                //Job_IsRemind = false
            };
            if (favourtie.Fav_Remarks == null) favourtie.Fav_Remarks = "";
            if (job.Latitude != null&&job.Longitude!=null)
            {
                favourtie.Job_AddressLatitude = job.Latitude;
                favourtie.Job_AddressLongitude = job.Longitude;
            }
            db.favouriteTable.InsertOnSubmit(favourtie);
            db.SubmitChanges();
            
        }
    }
}
