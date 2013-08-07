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
using System.Text;
namespace JobHub.Dal
{
    public class LoginDal:BaseDal
    {
        private static LoginDal loginDal = new LoginDal();
        /// <summary>
        /// 实例
        /// </summary>
        public static LoginDal Instance
        {
            get
            {
                return loginDal;
            }
        }

        #region 私有变量

        /// <summary>
        /// API
        /// </summary>
        readonly LoginApi _Api = new LoginApi();

        #endregion
        /// <summary>
        /// 获取制定用户的听众列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="startindex"></param>
        /// <param name="callback"></param>
        public void GetUser(string username, string password,Action<User> callback)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            
            _Api.GetUser(username, Convert.ToBase64String(bytes), (rs) =>
            {
                if (rs != null)
                {
                    callback(rs);
                }
            });
         
        }
    }
}
