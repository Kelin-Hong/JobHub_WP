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
using JobHub.API.Model;
using JobHub.API.Util;
using System.Text;
using Newtonsoft.Json.Linq;
namespace JobHub.API.Api
{
    public class JobApi:RequestBase
    {
        public JobApi()
            : base()
        { Charset = Encoding.UTF8; }


        Action<ResponseData<Job>> Callback { get; set; }
        Action<string> GetJobDetailCallback { set;get; }
        public void GetJobDetail(string jobId,Action<string> callback)
        {
           string url= URL_API_JOBHUB_EVENT_DETAIL.Replace("%s", jobId);
           this.GetJobDetailCallback = callback;
           GetData(url, null, GetJobDetailEnd);
        }
        /// <summary>
        /// 获取宣讲会
        /// </summary>
        /// <param name="time">起始时间</param>
        /// <param name="city">所在城市</param>
        /// <param name="university">所在大学</param>
        /// <param name="callback">回调函数</param>
        public void GetJobslist(long time, string  city, string university, Action<ResponseData<Job>> callback)
        {
            this.Callback = callback;
            var parameters = new Parameters();
           // parameters.Add("format", this.ResponseDataFormat.ToString().ToLower());
            parameters.Add("time", time);
            parameters.Add("city", city);
            parameters.Add("university", university);
            GetData(RequestBase.URL_API_JOBS, parameters, GetJobsListEnd);
        }
        /// <summary>
        /// 结束回调
        /// </summary>
        /// <param name="json"></param>
        private void GetJobsListEnd(string json)
        {
            if (Callback != null)
            {
                JObject jObject = JObject.Parse(json);
                string newjson=jObject.SelectToken("val").ToString();
                var obj = Utils.JsonToObject<ResponseData<Job>>(newjson);
                Callback(obj);
            }
        }

        private void GetJobDetailEnd(string json)
        {
            if (GetJobDetailCallback != null)
            {
                JObject jObject = JObject.Parse(json);
                string newjson = jObject.SelectToken("val").ToString();
                //var obj = Utils.JsonToObject<string>(newjson);

                GetJobDetailCallback(newjson);
            }
        }
      
    }
}
