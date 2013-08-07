/******************************************************
FileName:
Copyright (c) 2004-xxxx *********公司技术开发部
Writer:
create Date:
Rewriter:
Rewrite Date:
Impact:
Main Content(Function Name、parameters、returns)
　　 ******************************************************/
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
using System.IO;
namespace JobHub.API.Http
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncHttpRequest
    {
        public int Timeout { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Charset = Encoding.UTF8;

        /// <summary>
        /// 需要请求的地址
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public Parameters Parameters { get; set; }

        /// <summary>
        /// OAuthEndAction方法
        /// </summary>
        public Action<string, Action<string>> OAuthEndAction { get; set; }

        /// <summary>
        /// 回应方法
        /// </summary>
        public Action<string> OAuthCallback { get; set; }

        /// <summary>
        /// 将要发送的文件
        /// </summary>
        public Files Files { set; get; }

        public AsyncHttpRequest(string url):this(url,null,Encoding.UTF8)
        { }
         public AsyncHttpRequest(string url, Encoding charset): this(url, null, charset)
        { }
        public AsyncHttpRequest(string url, Action<string> func, Encoding charset)
         {
            this.Url = url;
            this.Timeout = 30000;
            this.Charset = charset;
         }
        private HttpWebRequest CreateRequest(string mothod, string url, int timeout)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            string cookiesName = (string)JobHub.API.Util.Isolated.Get("cookiesName");
          
            if (cookiesName != null)
            {
                string cookiesValue = (string)JobHub.API.Util.Isolated.Get(cookiesName);
                CookieCollection cookieCollection = new CookieCollection();
                cookieCollection.Add(new Cookie(cookiesName, cookiesValue));
                request.CookieContainer.Add(new Uri("https://www.ybole.com"), cookieCollection);
            }
            request.AllowReadStreamBuffering = false;
            request.Method = mothod;
            
            return request;
        }
        #region 方法
        
        /// <summary>
        ///  GET请求
        /// </summary>
        /// <param name="responseEnd"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public void Get(Action<string,Action<string>> responseEnd, Action<string> callback)
        {
            this.OAuthCallback = callback;
            this.OAuthEndAction = responseEnd;
            this.Get();
        }
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="asynchronousResult"></param>
        /// /// <summary>
        private void Get()
        {
            string queryString = this.Parameters == null ? "" : this.Parameters.BuildQueryString(true);
            string url = this.Url;
            if(!string.IsNullOrEmpty(queryString))
            {
                url=string.Concat(url,url.IndexOf('?')==-1 ? '?':'&',queryString);
            }
            var request=CreateRequest("GET",url,this.Timeout);
            request.BeginGetResponse(AsyncResponseCallback,request);
        }
       
        /// Post请求
        /// </summary>
        /// <param name="responseEnd"></param>
        /// <param name="callback"></param>
        public void Post(Action<string, Action<string>> responseEnd, Action<string> callback)
        {
            this.OAuthCallback = callback;
            this.OAuthEndAction = responseEnd;
            this.Post("application/x-www-form-urlencoded");
        }
        /// <summary>
        /// Post请求,带图片
        /// </summary>
        /// <param name="responseEnd"></param>
        /// <param name="files"></param>
        /// <param name="callback"></param>
        public void PostFile(Action<string, Action<string>> responseEnd, Files files, Action<string> callback)
        {
            this.OAuthCallback = callback;
            this.OAuthEndAction = responseEnd;
            this.Files = files;

            var request = CreateRequest("POST", this.Url, this.Timeout);
            if (this.Parameters != null && this.Parameters.Items.Count != 0)
            {
                request.BeginGetRequestStream(GetPostFileRequestStreamCallback, request);
            }
        }
        /// <summary>
        /// POST请求
        /// </summary>
        /// <returns></returns>
        private void Post(string contentType)
        {
            var request = CreateRequest("POST", this.Url, this.Timeout);
            request.ContentType = contentType;
            request.AllowReadStreamBuffering = true;

            if (this.Parameters != null && this.Parameters.Items.Count != 0)
            {
                request.BeginGetRequestStream(GetRequestStreamCallback, request);
            }
        }
        #endregion
        private void AsyncResponseCallback(IAsyncResult asynchronousResult)
        {
                var request = (HttpWebRequest)asynchronousResult.AsyncState;
                var response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                var streamResponse = response.GetResponseStream();
                var streamRead = new StreamReader(streamResponse,Encoding.UTF8);
                var responseString = streamRead.ReadToEnd();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    foreach (Cookie cookieValue in response.Cookies)
                    {
                        JobHub.API.Util.Isolated.Set(cookieValue.Name, cookieValue.Value);
                        JobHub.API.Util.Isolated.Set("cookiesName", cookieValue.Name);
                    }
                   
                }
                streamResponse.Close();
                streamRead.Close();
                response.Close();
              
                if (this.OAuthEndAction != null)
                {
                    this.OAuthEndAction(responseString, this.OAuthCallback);
                }
            
            //catch
            //{

            //    if (this.OAuthEndAction != null)
            //    {
            //        this.OAuthEndAction("", this.OAuthCallback);
            //    }
            //}
        }
        /// <summary>
        /// 获取POST流，并发送数据，调用回调
        /// </summary>
        /// <param name="asynchronousResult"></param>
        public void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var request = (HttpWebRequest)asynchronousResult.AsyncState;
                var stream = request.EndGetRequestStream(asynchronousResult);
                var queryString = this.Parameters.BuildQueryString(true);
                if (this.Parameters.Items[0].Key != "")
                {
                    var data = this.Charset.GetBytes(queryString);
                    stream.Write(data, 0, data.Length);
                   
                }
                stream.Close();
                    // StreamWriter postStream = new StreamWriter(request.EndGetRequestStream(asynchronousResult));

                    request.BeginGetResponse(AsyncResponseCallback, request);
                
            }
            catch (Exception)
            {
                if (this.OAuthEndAction != null)
                {
                    this.OAuthEndAction("", this.OAuthCallback);
                }
            }
        }

        /// <summary>
        /// 获取POSTFILE流，并发送数据，调用回调
        /// </summary>
        /// <param name="asynchronousResult"></param>
        public void GetPostFileRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            var request = (HttpWebRequest)asynchronousResult.AsyncState;
            var stream = request.EndGetRequestStream(asynchronousResult);
            #region 生成流
            var files = this.Files;
            var boundary = string.Concat("--", JobHub.API.Util.Utils.GenerateRndNonce());
            request.ContentType = string.Concat("multipart/form-data; boundary=", boundary);
            using (var ms = new MemoryStream())
            {
                byte[] boundaryData = this.Charset.GetBytes("\r\n--" + boundary + "\r\n");
                if (this.Parameters != null && this.Parameters.Items.Count != 0)
                {
                    //写入参数
                    const string parameterData = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    foreach (var p in this.Parameters.Items)
                    {
                        string item = string.Format(parameterData, p.Key, p.Value);
                        byte[] data = this.Charset.GetBytes(item);
                        ms.Write(boundaryData, 0, boundaryData.Length);
                        ms.Write(data, 0, data.Length);
                    }
                }

                if (files != null)
                {
                    //写入文件数据
                    const string fileData = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                    foreach (var p in files.Items)
                    {
                        if (p.Value != null)
                        {
                            string item = string.Format(fileData, p.Key, p.Value.FileName, p.Value.ContentType);
                            byte[] data = this.Charset.GetBytes(item);
                            ms.Write(boundaryData, 0, boundaryData.Length);
                            ms.Write(data, 0, data.Length);
                            p.Value.WriteTo(ms);
                        }
                    }
                }

                //写入结束线
                boundaryData = this.Charset.GetBytes("\r\n--" + boundary + "--\r\n");
                ms.Write(boundaryData, 0, boundaryData.Length);
                ms.WriteTo(stream);
                stream.Close();
            }
            #endregion
            request.BeginGetResponse(AsyncResponseCallback, request);
        }
    }
}
