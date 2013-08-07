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
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Linq;
namespace JobHub.API.Api
{
    public class CityApi:RequestBase
    {
        public CityApi():base()
        {
            
        }
        Action<ResponseData<Citys>> GetHotCityCallback { get; set; }
        Action<string> GetCurrentCityCallback { set; get; }
        Action<List<string>> GetCityUniversityCallback { set; get; }
        public void GetCityUniversity(string city,Action<List<string>> callback)
        {
            GetCityUniversityCallback = callback;
            string url = RequestBase.URL_API_JOBHUB_CITY_UNIVERSITY.Replace("%s", city);
            GetData(url, null, GetGetCityUniversityEnd);
        }
        public void GetHotCity(Action<ResponseData<Citys>> callback)
        {
            GetHotCityCallback = callback;
            GetData(RequestBase.URL_API_JOBHUB_HOT_CITY, null, GetHotCityEnd);
        }
        public void GetCurrentCity(string url, Action<string> callback)
        {
            GetCurrentCityCallback = callback;
            GetData(url, null, GetCurrentCityEnd);
        }
        private void GetCurrentCityEnd(string json)
        {
            JObject jObject = JObject.Parse(json);
            string ss=jObject["results"][0]["address_components"].ToString();
            JArray jArray = JArray.Parse(ss);
            JToken jToken= jArray.FirstOrDefault(c => c["types"][0].ToString().Equals("locality"));
            string s = jToken["short_name"].ToString();
            GetCurrentCityCallback(s);
        }
        private void GetHotCityEnd(string json)
        {
            var obj = Util.Utils.JsonToObject<ResponseData<Citys>>(json);
            GetHotCityCallback(obj);
        }
        private void GetGetCityUniversityEnd(string json)
        {
            JObject jObject = JObject.Parse(json);
           
             var universities=jObject.SelectToken("universities").Select(p => p["university"]).ToList();
             List<string> UList = new List<string>();
             foreach (var u in universities)
             {
                 UList.Add(u.ToString());
             }
             GetCityUniversityCallback(UList);
        }
       
    }
}
