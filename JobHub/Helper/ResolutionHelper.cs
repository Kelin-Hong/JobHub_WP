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
using System.Reflection;

namespace JobHub.Helper
{

   public enum Resolutions { WVGA, WXGA, HD720p };

   public static class ResolutionHelper
   {
       public static string Resolution
       {
           get { return GetResolution(); }
       }
       private static string GetResolution()
       {
           string resolution = "WVGA";
           var content = App.Current.Host.Content;
           Type type = content.GetType();
           PropertyInfo prop = type.GetProperty("ScaleFactor");
           if (prop != null)
           {
               int value = (int)prop.GetValue(content, null);
               switch (value)
               {
                   case 100:
                       resolution = "WVGA";
                       break;
                   case 150:
                       resolution = "720p";
                       break;
                   case 160:
                       resolution = "WXGA";
                       break;
               }
           }
           return resolution;
       }

   }
    
}
