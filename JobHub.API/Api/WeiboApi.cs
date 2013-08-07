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
using JobHub.API.Model;
using JobHub.API.Object;
using Newtonsoft.Json.Linq;
using JobHub.API.Util;

namespace JobHub.API.Api
{
    public class WeiboApi:RequestBase
    {
        Action<ResponseData<RelativeWeibo>> WeiBoListCallback;
        public void GetRelativeWeiboList(int count,int page,string name,Action<ResponseData<RelativeWeibo>> callback)
        {
            WeiBoListCallback = callback;
            Parameters parameter=new Parameters();
            parameter.Add("count",count);
            parameter.Add("page",page);
            parameter.Add("screen_name",name);
            GetData(URL_API_JOB_RELATIVE_WEIBO, parameter, GetRelativeWeiboListEnd);
        }
        private void GetRelativeWeiboListEnd(string json)
        {
            JObject jObject = JObject.Parse(json);
            
            //string newjson = jObject.SelectToken("val").ToString();
            JToken j = jObject["val"]["users"];
            string s = j.ToString();
            var obj = Utils.JsonToObject<ResponseData<RelativeWeibo>>(j.ToString());
            WeiBoListCallback(obj);
        }
    }
}
