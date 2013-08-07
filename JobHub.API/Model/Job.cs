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
using System.Runtime.Serialization;
using JobHub.API.Util;
namespace JobHub.API.Model
{
    [DataContract]
    public class Job
    {
        [DataMember(Name = "url")]
        public string Url { set; get; }
        [DataMember(Name = "company")]
        public Company Company { set; get; }
        
        //[DataMember(Name = "starttime")]
        //public string StartTime { set; get; }
        [DataMember(Name = "address")]
        public Address Address { set; get; }
        //private long endTime;
        //[DataMember(Name="endtime")]
        //public long EndTime 
        //{
        //    set
        //    {
        //        if (value == null)
        //        {
        //            endTime = 0;
        //        }
        //        else
        //            endTime = value;
        //    }

        //    get
        //    {
        //        return endTime;
        //    }
        //}
        //private long endTime;
        //[DataMember(Name = "endtime",IsRequired=false)]
        //public string EndTime
        //{
        //    get
        //    {
        //        if (endTime != null)
        //        {
        //            DateTime dt = Utils.ConvertJavaMiliSecondToDateTime(this.endTime);
        //            //DateTime dt=new DateTime(this.StartTime);

        //            return string.Format("{0:M}", dt) +
        //                  "  " + string.Format("{0:t}", dt);
        //        }
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        if(value!=null)
        //        endTime = long.Parse(value);
        //    }

        //}

        public long startTime;
        [DataMember(Name = "starttime")]
        public string StartTime
        {
            get
            {
                DateTime dt = Utils.ConvertJavaMiliSecondToDateTime(this.startTime);
                //DateTime dt=new DateTime(this.StartTime);

              return string.Format("{0:M}", dt) + 
                    "  "+string.Format("{0:t}", dt);
            }
            set
            {
                startTime = long.Parse(value);
            }
       
        }
        //public string Time
        //{
        //    get
        //    {
        //        if (EndTime != null)
        //            return StartTime + "-" + EndTime;
        //        else
        //            return StartTime;
        //    }
        //}
        private string index;
        [DataMember(IsRequired=false)]
        public string Color
        {
            get
            {
                //Random r = new Random();
                //int i=r.Next(3);
                int i = int.Parse(index);
                switch (i)
                {
                    case 0: return "#e5927d"; break;
                    case 1: return "#5db5e6"; break;
                    case 2: return "#77c47f"; break; 
                }
                return "e5927d";
            }
            set
            {
                index = value;
            }

        }
        [DataMember(Name="id")]
        public string Id { set; get; }
      
        
        
        
         
    }
}
