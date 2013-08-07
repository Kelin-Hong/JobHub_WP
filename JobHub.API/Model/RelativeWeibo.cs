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
using System.Globalization;

namespace JobHub.API.Model
{
    [DataContract]
    public class RelativeWeibo
    {
        [DataMember(Name = "id")]
        public string Id { set; get; }
        [DataMember(Name = "thumbnail_pic",IsRequired=false)]
        public string Thumbnail { set; get; }
        //public string Url { set; get; }
        private string time;
        [DataMember(Name = "created_at")]
        public string Time {
            set
            {
               // string t = value.Substring(value.IndexOf(' ') + 1, value.IndexOf('+') - value.IndexOf(' ') - 2);
                IFormatProvider culture = new CultureInfo("en-US");
                DateTime dt = DateTime.ParseExact(value, "ddd MMM dd HH:mm:ss zzz yyyy", culture);
                time=string.Format("{0:M}", dt) +
                    "  " + string.Format("{0:t}", dt);
            }
            get
            {
                return time;
            }
        }
        [DataMember(Name = "text")]
        public string Text { set; get; }
        private string source;
        [DataMember(Name = "source")]
        public string Source { 
            set
              {
                  source ="from "+ value.Substring(value.IndexOf('>') + 1, value.LastIndexOf('<') - value.IndexOf('>') - 1);
              }

            get
            {
                return source;
            }
        }
        
        
        //[DataMember(Name="original_pic")]
        //public String Origpic { set; get; }
        [DataMember(Name = "user")]
        public SinaUser User { set; get; }
    }
    [DataContract]
    public class SinaUser
    {
        [DataMember(Name="screen_name")]
        public string SinaName{set;get;}
        [DataMember(Name = "profile_image_url")]
        public string Avatar { set; get; }
    }
}
