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

namespace JobHub.API.Model
{
    public class JobNews
    {
        private string cover;
        public string Cover
        {
            set
            {
                cover = RequestBase.JOBHUB_API_DOMAIN + value;
            }
            get
             {
              return cover;
             }
        }
        public string Id{set;get;}
        public long Time{set;get;}
        private string DetailTitle
        {
            get{
                return title;
               }
        }
        private string title;
        public string Title 
        {
            set
            {
                title = value;
            }
            get
            {
             //   return 
                  if(title.Length >11) return  title.Substring(0, 10) + "..";
                else
                return title;
            }
        }
    }
}
