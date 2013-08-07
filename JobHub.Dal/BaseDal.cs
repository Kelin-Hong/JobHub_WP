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
using JobHub.Dal.db;
using System.Threading;

namespace JobHub.Dal
{
    public class BaseDal
    {
        protected bool networkIsAvailable;
        public static AutoResetEvent OperationOnDatabase  = new AutoResetEvent(true); 
        protected DataBase db = new DataBase(DataBase.contectString);
        public BaseDal()
        {
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }
            networkIsAvailable = Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
