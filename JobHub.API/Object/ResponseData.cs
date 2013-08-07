using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;

namespace JobHub.API.Object
{
  
    /// <summary>
    /// 服务端输出的结果数据
    /// </summary>
    [DataContract]
    public class ResponseData<T>
    {
        
        /// <summary>
        /// 数据
        /// </summary>
        [DataMember(Name = "cities",IsRequired=false)]
        public List<T> HotCityData { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [DataMember(Name = "content",IsRequired = false)]
        public List<T> Data { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [DataMember(Name = "val", IsRequired = false)]
        public List<T> JobNewsData { get; set; }

        [DataMember(Name = "statuses", IsRequired = false)]
        public List<T> RelativeWeiboData { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //[DataMember(Name = "deleted")]
        //public string[] Deleted { get; set; }
        ///// <summary>
        ///// 数据数量
        ///// </summary>
        //[DataMember(Name = "count_query")]
        //public int Data_Count { get; set; }

       
        

        /// <summary>
        /// 错误码枚举
        /// errcode=0 表示成功;  
        /// errcode=4 表示有过多脏话;  
        /// errcode=5 禁止访问，如城市，uin黑名单限制等;  
        /// errcode=6 删除时：该记录不存在。发表时：父节点已不存在;  
        /// errcode=8 内容超过最大长度：420字节 （以进行短url处理后的长度计）;  
        /// errcode=9 包含垃圾信息：广告，恶意链接、黑名单号码等 ; 
        /// errcode=10 发表太快，被频率限制;  
        /// errcode=11 源消息已删除，如转播或回复时;  
        /// errcode=12 源消息审核中;  
        /// errcode=13 重复发表 ;
        /// </summary>
        
        public enum EErrcode
        {
            Success = 0,
            DirtyWords = 4,
            AccessDeny = 5,
            NotExist = 6,
            Overflow = 8,
            JunkMessage = 9,
            PostLimit = 10,
            SourceNotExist = 11,
            SourcePending = 12,
            SameTweet = 13
        }
    }
}
