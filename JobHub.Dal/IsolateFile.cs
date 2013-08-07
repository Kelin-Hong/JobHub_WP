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
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization.Json;
namespace JobHub.Dal
{

    public class IsolateFile
    {
        private static string IsolateFileDir = "isolatefileDir";
       // private const string StatusListFile = "status_list_file.xml";

        public static void Set<T>(string fileName, T t) where T : class
        {
            SaveSettingToFile<T>(IsolateFileDir,fileName, t);
        }

        public static T Get<T>(string fileName) where T:class
        {
            T t=RetrieveSettingFromFile<T>(IsolateFileDir, fileName);
            return RetrieveSettingFromFile<T>(IsolateFileDir, fileName);
        }

        private static T RetrieveSettingFromFile<T>(string dir, string file) where T : class
        {
            IsolatedStorageFile isolatedFileStore = IsolatedStorageFile.GetUserStoreForApplication();
            if (isolatedFileStore.DirectoryExists(dir))
            {
                try
                {
                    using (var stream = new IsolatedStorageFileStream(System.IO.Path.Combine(dir, file), FileMode.Open, isolatedFileStore))
                    {
                        return (T)SerializationHelper.DeserializeData<T>(stream);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Could not retrieve file " + dir + "\\" + file + ". With Exception: " + ex.Message);
                }
            }
            return null;
        }

        private static void SaveSettingToFile<T>(string dir, string file, T data)
        {
            IsolatedStorageFile isolatedFileStore = IsolatedStorageFile.GetUserStoreForApplication();
            if (!isolatedFileStore.DirectoryExists(dir))
                isolatedFileStore.CreateDirectory(dir);
            try
            {
                string fn = System.IO.Path.Combine(dir, file);
                if (isolatedFileStore.FileExists(fn)) isolatedFileStore.DeleteFile(fn); //mostly harmless, used because isolatedFileStore is stupid :D

                using (var stream = new IsolatedStorageFileStream(fn, FileMode.CreateNew, FileAccess.ReadWrite, isolatedFileStore))
                {
                    SerializationHelper.SerializeData<T>(data, stream);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Could not save file " + dir + "\\" + file + ". With Exception: " + ex.Message);
            }
        }
          
          
    }
    public static class SerializationHelper
    {
        public static void SerializeData<T>(this T obj, Stream streamObject)
        {
            if (obj == null || streamObject == null)
                return;

            var ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(streamObject, obj);
        }

        public static T DeserializeData<T>(Stream streamObject)
        {
            if (streamObject == null)
                return default(T);

            var ser = new DataContractJsonSerializer(typeof(T));
            return (T)ser.ReadObject(streamObject);
        }
    }
}
