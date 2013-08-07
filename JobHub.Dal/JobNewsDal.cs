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
    public class JobNewsDal
    {
        private static JobNewsDal jobNewsDal = new JobNewsDal();
        /// <summary>
        /// 实例
        /// </summary>
        public static JobNewsDal Instance
        {
            get
            {
                return jobNewsDal;
            }
        }

        #region 私有变量

        /// <summary>
        /// API
        /// </summary>
        readonly JobNewsApi _Api = new JobNewsApi();

        #endregion

        public void GetJobNewsList(long time,Action<List<JobNews>> callback)
        {
            _Api.GetJobNewsList(time, (rs) =>
                {
                    if (rs.JobNewsData != null)
                    {
                        callback(rs.JobNewsData);
                    }
                });
        }
        public void GetJobNewsContent(string id, Action<string> callback)
        {
            _Api.GetJobNewsContent(id, (s) =>
                {
                    if (!String.IsNullOrEmpty(s))
                        callback(s);
                });
        }
    }
}
