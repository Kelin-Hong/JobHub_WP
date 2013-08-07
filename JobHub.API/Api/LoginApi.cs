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
using JobHub.API.Util;
using System.Text;
using Newtonsoft.Json.Linq;
namespace JobHub.API.Api
{
    public class LoginApi:RequestBase
    {
        public LoginApi():base()
        {
            Charset = Encoding.UTF8;
        }
        Action<User> Callback { get; set; }

        public void LogOut(Action<string> callback)
        {

        }

        public void GetUser(string userName, string passWord, Action<User> callback)
        {
            this.Callback = callback;
            var parameters = new Parameters();
          
            parameters.Add("username", userName);
            parameters.Add("password", passWord);
            PostData(RequestBase.URL_API_Login, parameters, GetUserEnd);
        }

        void GetUserEnd(string json)
        {
            if (Callback != null)
            {

                var obj = Utils.JsonToObject<User>(JObject.Parse(json)["val"].ToString());
                Callback(obj);
                Isolated.Set("IsSinaLogin", true);
            }
        }
    }
}
