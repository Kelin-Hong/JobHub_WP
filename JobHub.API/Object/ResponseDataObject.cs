using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace JobHub.API.Object
{
    /// <summary>
    /// 输出的对象
    /// </summary>
    [DataContract]
    public class ResponseDataObject<T>
    {
        

        /// <summary>
        /// 结果集合数组 
        /// </summary>
        [DataMember(Name = "content")]
        public List<T> Info { get; set; }

    }
}
