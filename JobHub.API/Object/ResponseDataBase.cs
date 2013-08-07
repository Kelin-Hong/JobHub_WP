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
using System.Runtime.Serialization;

namespace JobHub.API.Object
{
    [DataContract]
    public class ResponseDataBase<T>
    {
        ///// <summary>
        ///// 错误信息
        ///// </summary>
        //[DataMember(Name = "msg")]
        //public string Msg { get; set; }

        ///// <summary>
        ///// 返回码:
        /////Ret=0 成功返回;
        /////Ret=1 参数错误; 
        /////Ret=2 频率受限; 
        /////Ret=3 鉴权失败; 
        /////Ret=4 服务器内部错误 
        ///// </summary>
        //[DataMember(Name = "ret")]
        //public int Ret { get; set; }

        ///// <summary>
        ///// 错误码
        ///// </summary>
        //[DataMember(Name = "errcode")]
        //public EErrcode Errcode { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}
