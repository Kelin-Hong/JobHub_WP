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
using System.Text;
using JobHub.API.Model;
using JobHub.API.Object;
using System.Collections.Generic;

namespace JobHub.Dal
{
    public class CityDal
    {
        private static CityDal cityDal;
        /// <summary>
        /// 实例
        /// </summary>
        /// 
        public static CityDal Instance
        {
            get
            {
                if (cityDal == null)
                {
                    cityDal = new CityDal();
                }
                return cityDal;
            }
        }

        #region 私有变量

        /// <summary>
        /// API
        /// </summary>
        readonly CityApi _Api = new CityApi();

        #endregion
        /// <summary>
        /// 获取热门城市
        /// </summary>
        /// <param name="account"></param>
        /// <param name="startindex"></param>
        /// <param name="callback"></param>
        public void GetHotCitys(Action<List<Citys>> callback)
        { 
            _Api.GetHotCity((rs)=>

            {
                if (rs.HotCityData != null)
                {
                    callback(rs.HotCityData);
                }
            });
         
        }
        public void GetCurrentCity(double[] data,Action<string> callback)
        {
           // string urlString = "http://loc.desktop.maps.svc.ovi.com/geocoder/rgc/1.0?" + "lat=" + data[0] + "&long=" + data[1] + "&output=json" + "&language=zh-CN";
            string urlString = "http://maps.google.com/maps/api/geocode/json?latlng="+data[0]+","+data[1]+"&sensor=true&language=zh-CN";
            _Api.GetCurrentCity(urlString, (s)=>
                {
                    callback(s);
                });
           
        }
        public void GetCityUniversity(string city, Action<List<string>> callback)
        {
            _Api.GetCityUniversity(city, (rs) =>
            {
                if (rs != null)
                {
                    callback(rs);
                }
            });
        }
    }
    
}
