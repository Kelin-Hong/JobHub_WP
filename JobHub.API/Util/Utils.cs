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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Diagnostics;
using Newtonsoft.Json;
namespace JobHub.API.Util
{
    public class Utils
    {
        // static string str = "{\"content\": [{\"url\": \"http://job.hust.edu.cn/show/recruitcouncil/recruitfair.htm?id=20830\", \"company\": {\"index\": [\"哈哈67d0\", \"哈哈91cd哈哈5e86哈哈5e02\", \"哈哈91cd哈哈5e86\", \"哈哈653f哈哈6cd5\", \"哈哈673a哈哈5173\"], \"name\": \"哈哈91cd哈哈5e86哈哈5e02哈哈67d0哈哈653f哈哈6cd5哈哈673a哈哈5173\", \"alias\": [], \"id\": \"50e25f89b041be4510f09cf8\"}, \"starttime\": 1357606832.0, \"address\": {\"university\": \"哈哈534e哈哈4e2d哈哈79d1哈哈6280哈哈5927哈哈5b66\", \"city\": \"哈哈6b66哈哈6c49\", \"accurate\": {\"name\": \"哈哈5927哈哈5b66哈哈751f哈哈6d3b哈哈52a8哈哈4e2d哈哈5fc3\", \"longitude\": 114.4141960144043, \"latitude\": 30.51835035703118, \"id\": \"4f4858f1cea1755b6c000000\"}, \"detail\": \"哈哈5927哈哈5b66哈哈751f哈哈6d3b哈哈52a8哈哈4e2d哈哈5fc3302哈哈5ba4\"}, \"endtime\": 1357615800.0, \"id\": \"50e25f89b041be4510f09cfa\"}, {\"url\": \"http://job.hust.edu.cn/show/recruitcouncil/recruitfair.htm?id=20914\", \"company\": {\"index\": [\"哈哈5e7f哈哈897f\", \"哈哈8d28哈哈76d1哈哈5c40\", \"哈哈8d28哈哈76d1\"], \"name\": \"哈哈5e7f哈哈897f哈哈8d28哈哈76d1哈哈5c40\", \"alias\": [], \"id\": \"50ea9ef2b041be0b2669b246\"}, \"starttime\": 1357608654.0, \"address\": {\"city\": \"哈哈6b66哈哈6c49\", \"university\": \"哈哈534e哈哈4e2d哈哈79d1哈哈6280哈哈5927哈哈5b66\", \"detail\": \"哈哈4e3b哈哈6821哈哈533a哈哈6559哈哈5de5哈哈6d3b哈哈52a8哈哈4e2d哈哈5fc3\"}, \"endtime\": 1357615800.0, \"id\": \"50ea9ef2b041be0b2669b248\"}, {\"url\": \"http://job.hust.edu.cn/show/recruitcouncil/recruitfair.htm?id=20907\", \"company\": {\"index\": [\"哈哈53d1哈哈5c55\", \"哈哈7535哈哈529b\", \"哈哈6c47\", \"哈哈96c6哈哈56e2\", \"哈哈4e30\", \"哈哈6c47哈哈8fbe哈哈4e30\", \"哈哈73e0哈哈6d77\", \"哈哈8fbe\"], \"name\": \"哈哈73e0哈哈6d77哈哈6c47哈哈8fbe哈哈4e30哈哈7535哈哈529b哈哈53d1哈哈5c55哈哈ff08哈哈96c6哈哈56e2\", \"alias\": [], \"id\": \"50ea9ef2b041be0b2669b243\"}, \"starttime\": 1357803004.0, \"address\": {\"city\": \"哈哈6b66哈哈6c49\", \"university\": \"哈哈534e哈哈4e2d哈哈79d1哈哈6280哈哈5927哈哈5b66\", \"detail\": \"哈哈897f哈哈4e5d哈哈697c224哈哈5ba4\"}, \"endtime\": 1357813800.0, \"id\": \"50ea9ef2b041be0b2669b245\"}, {\"url\": \"http://job.hust.edu.cn/show/recruitcouncil/recruitfair.htm?id=20717\", \"company\": {\"index\": [\"哈哈89c1哈哈9762\", \"哈哈6bd5哈哈4e1a哈哈751f\", \"哈哈6bd5哈哈4e1a\", \"哈哈4f9b哈哈9700\", \"哈哈5c4a\", \"哈哈4f9b哈哈9700哈哈89c1哈哈9762\", \"哈哈4e1a哈哈751f\", \"哈哈9762哈哈4f1a\", \"哈哈89c1哈哈9762哈哈4f1a\", \"哈哈4f1a\", \"2013\"], \"name\": \"2013哈哈5c4a哈哈6bd5哈哈4e1a哈哈751f哈哈4f9b哈哈9700哈哈89c1哈哈9762哈哈4f1a\", \"alias\": [], \"id\": \"50d07331b041be3b0b10f707\"}, \"starttime\": 1363654837.0, \"address\": {\"university\": \"哈哈534e哈哈4e2d哈哈79d1哈哈6280哈哈5927哈哈5b66\", \"city\": \"哈哈6b66哈哈6c49\", \"accurate\": {\"name\": \"哈哈5149哈哈8c37哈哈4f53哈哈80b2哈哈9986\", \"longitude\": 114.41835880279541, \"latitude\": 30.508552777754073, \"id\": \"4f485921cea1755b6d00000f\"}, \"detail\": \"哈哈5149哈哈8c37哈哈4f53哈哈80b2哈哈9986\"}, \"endtime\": 1363680000.0, \"id\": \"50e25f89b041be4510f09cf7\"}], \"deleted\": [], \"count_query\": 4}";
        /// <summary>
        /// UrlEncode
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlEncode(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            StringBuilder buffer = new StringBuilder(text.Length);
            byte[] data = Encoding.UTF8.GetBytes(text);
            foreach (byte b in data)
            {
                char c = (char)b;
                if (!(('0' <= c && c <= '9') || ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'))
                    && "-_.~".IndexOf(c) == -1)
                {
                    buffer.Append('%' + Convert.ToString(c, 16).ToUpper());
                }
                else
                {
                    buffer.Append(c);
                }
            }
            return buffer.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            //Encoding gb2312 = Encoding.GetEncoding("gb2312");
            //Encoding utf8 = Encoding.UTF8;
            //首先用utf-8进行解码 
           // Decoder decoder= UnicodeEncoding.UTF8.GetDecoder();
           // json = str;
           //string json_str = HttpUtility.HtmlDecode(json);
           //Debug.WriteLine(json);
          // Debug.WriteLine("   ");
         //  Debug.WriteLine(json_str);

            //if (!string.IsNullOrEmpty(json))
            //{
            //    var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                
            //    var ser = new DataContractJsonSerializer(typeof(T));
               
            //    return (T)ser.ReadObject(ms);
            //}

            T t=JsonConvert.DeserializeObject<T>(json);
            return t;
        }

        /// <summary>
        /// 随机种子
        /// </summary>
        private static Random RndSeed = new Random();
        /// <summary>
        /// 生成一个随机码
        /// </summary>
        /// <returns></returns>
        public static string GenerateRndNonce()
        {
            return string.Concat(
            Utils.RndSeed.Next(1, 99999999).ToString("00000000"),
            Utils.RndSeed.Next(1, 99999999).ToString("00000000"),
            Utils.RndSeed.Next(1, 99999999).ToString("00000000"),
            Utils.RndSeed.Next(1, 99999999).ToString("00000000"));
        }

        public static DateTime ConvertJavaMiliSecondToDateTime(long javaMS)
        {
            DateTime UTCBaseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dt=UTCBaseTime.Add(TimeSpan.FromMilliseconds(javaMS*1000).Add(new TimeSpan(8,0,0)));
            //new TimeSpan(javaMS*1000*
            //DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
            //long time = (long)javaSpan.TotalMilliseconds / 1000;
            //DateTime dt = UTCBaseTime.Add(new TimeSpan(javaMS *

            //TimeSpan.TicksPerMillisecond)).ToLocalTime();

            return dt;

        }
       
    }
}
