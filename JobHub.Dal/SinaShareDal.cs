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

namespace JobHub.Dal
{
    public class SinaShareDal
    {
        private static SinaShareDal sinaShareDal = new SinaShareDal();
        public static SinaShareDal Instance
        {
            get
            {
                return sinaShareDal;
            }
        }
        readonly SinaShareApi _Api = new SinaShareApi();
        public void LoginShare(Action callback)
        {
            _Api.LoginShare((s) =>
                {
                    callback();
                });
        }
        public void SinaShareWithCityAndUniversity(string city, string university, string text, Action callback)
        {
            _Api.SinaShareWithCityAndUniversity(city, university, text, (s) => 
            {
                callback();
            });
        }

        public void SinaTextShare(string text, Action callback)
        {
            _Api.SinaTextShare(text, (s) =>
                {
                    callback();
                });
        }
    }
}
