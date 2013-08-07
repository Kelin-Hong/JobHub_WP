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
using JobHub.API.Object;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JobHub.API.Api
{
    public class SinaShareApi:RequestBase
    {
        public SinaShareApi()
        {
            Charset = Encoding.UTF8; 
        }
        Action<string> LoginShareCallback { set; get; }
        Action<string> SinaShareTextCallback { set; get; }
        Action<string> SinaShareWithCityAndUniversityCallback { set; get; }
        public void LoginShare(Action<string> callback)
        {
            this.LoginShareCallback = callback;
            var parameters = new Parameters();

            parameters.Add("", "");
            PostData(RequestBase.URL_API_SINA_LOGIN_SHARE, parameters, LoginShareEnd);
        }
        private void LoginShareEnd(string s)
        {
            if (s.Contains("ok"))
                LoginShareCallback(s);
        }
        public void SinaShareWithCityAndUniversity(string city, string university, string text, Action<string> callback)
        {
            SinaShareWithCityAndUniversityCallback = callback;
            string url = null;
            if (city != null)
            {
                url = URL_API_JOBHUB_CITY_GENERATE_WEIBO.Replace("%s", city);
            }
            if (university != null)
            {
                url = URL_API_JOBHUB_UNIVERSITY_GENERATE_WEIBO.Replace("%s", university);
            }
            var parameters = new Parameters();

            parameters.Add("text", text);
            PostData(url, parameters, SinaShareWithCityAndUniversityEnd);
        }
        private void SinaShareWithCityAndUniversityEnd(string s)
        {
            if (JObject.Parse(s)["msg"].ToString().Equals("ok")) 
            SinaShareWithCityAndUniversityCallback(s);
        }
        public void SinaTextShare(string text,Action<string> callback)
        {
            this.SinaShareTextCallback = callback;
            var parameters = new Parameters();
            parameters.Add("text", text);
            PostData(RequestBase.URL_API_SINA_SHARE, parameters, SinaTextShareEnd);
        }

        private void SinaTextShareEnd(string s)
        {
            if (JObject.Parse(s)["msg"].ToString().Equals("ok")) 
            this.SinaShareTextCallback(s);
        }

    }
}
