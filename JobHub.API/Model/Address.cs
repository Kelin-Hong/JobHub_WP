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

namespace JobHub.API.Model
 { 
    [DataContract]
    public class Address
    {
        [DataMember(Name = "university")]
        public String University { set; get; }
        [DataMember(Name = "city")]
        public String City { set; get; }
        [DataMember(Name="accurate",IsRequired=false)]
        public Accurate Accurate { set; get; }
        
        [DataMember(Name="detail")]
         public String Detail { set; get; }
       [DataMember(IsRequired=false)]
        public string JobListDetail
        {
            get
            {
                if (Detail.Length > 19) return Detail.Substring(0,19) + "..";
                else
                    return Detail;
            }
        }
    }
    
    [DataContract]
    public class Accurate
    {
        [DataMember(Name = "name")]
        public string Name { set; get; }
        [DataMember(Name = "longitude")]
        public double Longitude { set; get; }
        [DataMember(Name="latitude")]
        public double Latitude { set; get; }
       
        [DataMember(Name = "id")]
        public string Id { set; get; }
        [DataMember(Name = "detail")]
        public string Detail { set; get; }

    }
    [DataContract]
    public class Company
    {
        [DataMember(Name = "id")]
        public string Id { set; get; }
        //[DataMember(Name = "index")]
        //public string[] Index { set; get; }
        private string name;
        [DataMember(Name = "name")]
        public string Name {
            set
            {
                if (value.Length > 16) name = value.Substring(0, 16) + "..";
                else name = value;

            }

            get
            {
                return name;
            }
        }

        [DataMember(Name = "official_weibo", IsRequired = false)]
        public string OfficialWeibo { set; get; }

        //[DataMember(Name="alias")]
        //public string[] alias { set; get; }
       
       
       
    }
}
