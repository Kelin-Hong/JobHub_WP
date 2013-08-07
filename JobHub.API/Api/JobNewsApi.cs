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
using System.Text;
using JobHub.API.Object;
using JobHub.API.Model;
using Newtonsoft.Json.Linq;

namespace JobHub.API.Api
{
    public class JobNewsApi:RequestBase
    {
        public JobNewsApi()
        {
            Charset = Encoding.UTF8;
        }
        Action<ResponseData<JobNews>> JobNewsListCallback { get; set; }
        Action<String> JobNewsContentCallback { set; get; }
        public void GetJobNewsList(long time,Action<ResponseData<JobNews>> callback)
        {
            JobNewsListCallback = callback;
            var parameters = new Parameters();
            parameters.Add("time", time);
            GetData(RequestBase.URL_API_JOB_NEWS, parameters, GetJobNewsListEnd);
        }
        private void GetJobNewsListEnd(string json)
        {
            var obj = Util.Utils.JsonToObject<ResponseData<JobNews>>(json);
            JobNewsListCallback(obj);
        }
        public void GetJobNewsContent(string id,Action<string> callback)
        {
            string url=RequestBase.URL_API_JOB_NEWS_DETAIL.Replace("%s", id);
            JobNewsContentCallback = callback;
            GetData(url, null, GetJobNewsEnd);
        }
        private void GetJobNewsEnd(string json)
        {
            JObject j = JObject.Parse(json);
            string html = j["val"].ToString();
            JobNewsContentCallback(html);
            //var obj = Util.Utils.JsonToObject<ResponseData<JobNews>>(json);
           // JobNewsListCallback(obj);
        }
    }
}
