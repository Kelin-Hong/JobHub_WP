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
using Com.AMap.Search.API.Options;
using Com.AMap.Search.API;

namespace JobHub.Helper
{
    public class MapAndLocationHelper
    {
        static Action<string> GetCurrentCityCallback { set; get; }
        public static void GetCurrentCity(double[] data,Action<string> callback)
        {
            GetCurrentCityCallback = callback;
            MReGeoCode.PoiToAddress(data[1], data[0], (result) =>
                {
                    GetCurrentCityCallback(result.resultList[0].City.Name);
                });
            
           
            
        }
       
    }
}
