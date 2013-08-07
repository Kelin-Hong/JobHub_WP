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

namespace JobHub.API.Model
{
    public class User
    {
        public bool IsFollowing { set; get; }
        public bool Verified { set; get; }
        public bool IsFollower { set; get; }
        public string Face { set; get; }
        public long Followers_Count { set; get; }
        public long Followings_Count { set; get; }
        public long Statuses_Count { set; get; }
        public string NickName { set; get; }
        public string Id { set; get; }
        public string Description { set; get; }
    }
}
