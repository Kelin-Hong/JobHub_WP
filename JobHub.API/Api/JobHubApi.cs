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

namespace JobHub.API.Api
{
    public class JobHubApi:RequestBase
    {
        public JobHubApi()
            : base()
        { Charset = Encoding.UTF8; }
        Action PostFeedbackCallback { set; get; }
        public void PostFeedback(string content,Action callback)
        {
            PostFeedbackCallback = callback;
            var parameters = new Parameters();

            parameters.Add("description", content);

            PostData(RequestBase.URL_API_JOBHUB_FEEDBACK_ADD, parameters, PostFeedbackEnd);
        }
        private void PostFeedbackEnd(string s)
        {
            if (s.Contains("ok"))
            {
                PostFeedbackCallback();
            }
        }
    }
}
