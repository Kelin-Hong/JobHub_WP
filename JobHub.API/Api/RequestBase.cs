using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

using System.Xml;
using System.Xml.Linq;
using JobHub.API.Http;
using JobHub.API.Object;

namespace JobHub.API.Api
{
    /// <summary>
    /// 接口请求的基类
    /// </summary>` 
    public abstract class RequestBase
    {
    public static readonly string JOBHUB_DOMAIN = "www.ybole.com";

    public static readonly string JOBHUB_API_DOMAIN_V1 = "http://www.ybole.com/api/1/";
    
    public static readonly string JOBHUB_API_DOMAIN = "http://www.ybole.com";

    public static readonly string JOBHUB_API_DOMAIN_DEV_V1 = "http://dev.ybole.com/api/1/";

    public static readonly string JOBHUB_API_DOMAIN_HTTPS = "https://www.ybole.com";

    public static readonly string URL_LOGIN = JOBHUB_API_DOMAIN_HTTPS + "/mobile_login";

    public static readonly string URL_CALLBACK = JOBHUB_API_DOMAIN_HTTPS + "/sina/mobile_callback";

    public static readonly string URL_API_Login = JOBHUB_API_DOMAIN_V1 + "/sina/classic_login";
    /**
     * 获取宣讲会
     */
    protected static readonly String URL_API_JOBS = JOBHUB_API_DOMAIN_V1 + "jobhub";

    protected static readonly String URL_API_SINA_SHARE = JOBHUB_API_DOMAIN_V1 + "tweet/post/99";

    /**
     * 登录发送微博
     */
    protected static readonly String URL_API_SINA_LOGIN_SHARE = JOBHUB_API_DOMAIN_V1
            + "jobhub/login/share_to_weibo";

    /**
     * 分享学校宣讲会
     */
    protected static readonly String URL_API_JOBHUB_UNIVERSITY_GENERATE_WEIBO = JOBHUB_API_DOMAIN_V1
            + "jobhub/university/%s/generate_weibo";

    /**
     * 分享城市宣讲会
     */
    protected static readonly String URL_API_JOBHUB_CITY_GENERATE_WEIBO = JOBHUB_API_DOMAIN_V1
            + "jobhub/city/%s/generate_weibo";

    /**
     * 宣讲会详细信息
     */
    protected static readonly String URL_API_JOBHUB_EVENT_DETAIL = JOBHUB_API_DOMAIN_V1
            + "jobhub/%s/detail";

    /**
    * 热门城市
    */
    protected static readonly String URL_API_JOBHUB_HOT_CITY = JOBHUB_API_DOMAIN
            + "/jobhub/hotcity";

    /**
   * 指定城市的大学
    */
    protected static readonly String URL_API_JOBHUB_CITY_UNIVERSITY = JOBHUB_API_DOMAIN
            + "/jobhub/university/%s";   

    /**
     * 校招咨询
     */
    protected static readonly String URL_API_JOB_NEWS_DETAIL = JOBHUB_API_DOMAIN_V1
            + "blog/507d1be7cea1755142378082/%s/content";
    /**
     * 校招咨询内容
    */
    protected static readonly String URL_API_JOB_NEWS = JOBHUB_API_DOMAIN_V1
            + "blog/507d1be7cea1755142378082/search";

    /**
      * 相关微博
     */
    protected static readonly String URL_API_JOB_RELATIVE_WEIBO = JOBHUB_API_DOMAIN_V1
            + "sina/user/tweets";

    /**
     * 用户反馈
    */
    protected static readonly String URL_API_JOBHUB_FEEDBACK_ADD = JOBHUB_API_DOMAIN_V1
            + "feedback/add";
        /// <summary>
        /// 根据请求基本地址实例化对象
        /// </summary>
        protected RequestBase()
        {
            this.ResponseDataFormat = ResponseDataFormat.JSON;
        }

        protected Encoding Charset
        {
            set;
            get;
        }
        /// <summary>
        /// 操作中最后一次发生的错误
        /// </summary>
        public Exception LastError { get; private set; }


        /// <summary>
        /// GET数据
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="parameters"></param>
        /// <param name="callback"></param>
        protected virtual void GetData(string requestUrl, Parameters parameters, Action<string> callback)
        {
            this.LastError = null;
            var request = new AsyncHttpRequest(requestUrl, Charset) { Parameters = parameters };
            request.Get(EndGetResponseData, callback);
        }

        /// <summary>
        /// POST数据
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="parameters"></param>
        /// <param name="callback"></param>
        protected virtual void PostData(string requestUrl, Parameters parameters, Action<string> callback)
        {
            this.PostData(requestUrl,parameters,null,callback);
        }

        /// <summary>
        /// POST数据
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="parameters"></param>
        /// <param name="files"></param>
        /// <param name="callback"></param>
        protected virtual void PostData(string requestUrl, Parameters parameters,Files files, Action<string> callback)
        {
            this.LastError = null;

            var request = new AsyncHttpRequest(requestUrl, Charset) { Parameters = parameters };
            if (files!=null)
            {
                request.PostFile(EndGetResponseData, files, callback);
            }
            else
            {
                request.Post(EndGetResponseData, callback);
            }
        }

        /// <summary>
        /// 完成数据请求，调用回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="callback"></param>

        private static void EndGetResponseData(string data,Action<string> callback) {
            if (!string.IsNullOrEmpty(data) && callback != null)
            {
                callback(data);
            }
        }


        /// <summary>
        /// 设置或返回获取数据的格式
        /// </summary>
        protected ResponseDataFormat ResponseDataFormat { get; set; }


    }
}
