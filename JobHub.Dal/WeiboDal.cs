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
using System.Collections.Generic;
using JobHub.API.Model;

namespace JobHub.Dal
{
    public class WeiboDal:BaseDal
    {
        private static WeiboDal weiboDal = new WeiboDal();
        public static WeiboDal Instance
        {
            get
            {
                return weiboDal;
            }
        }
       
     
        readonly WeiboApi _Api = new WeiboApi();

        public void GetRelativeWeiboList(int count, int page, string name, Action<List<RelativeWeibo>> callback)
        {
            _Api.GetRelativeWeiboList(count, page, name, (rs) =>
                {
                    if (rs.RelativeWeiboData != null)
                    {
                        callback(rs.RelativeWeiboData);
                    }
                });
        }
      
    }
}
