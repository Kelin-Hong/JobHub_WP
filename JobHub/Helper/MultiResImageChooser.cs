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
using System.Windows.Media.Imaging;
namespace JobHub.Helper
{
    public class MultiResImageChooser 
    {
        private string str = "/Resource/Image/";
        public string IC_Location
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_location.png";
            }
        }
        public string IC_Forward
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_forward.png";
            }
        }
        public string BG_Main
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/bg_main.png";
            }
        }
        public string Login_PassWord
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_login_password.png";
            }
        }
        public string Login_UserName
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_login_username.png";
            }
        }
        public string Main_More_Change_Account
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_change_account.png";
            }
        }
        public string Main_More_Check_Update
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_check_update.png";
            }
        }
        public string Main_More_Clear_Cache
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_clear_cache.png";
            }
        }
        public string Main_More_Secret_Agreement
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_secret_agreement.png";
            }
        }
        public string Main_More_User_Feekback
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_user_feekback.png";
            }
        }
        public string Main_More_About_Jobhub
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_about_jobhub.png";
            }
        }
        public string Job_Detail_Title_Header
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/bg_title_bar.png";
            }
        }
        public string Job_Detail_EditOrAdd
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_editor_add.png";
            }
        }
        public string Job_Detail_Write_Note
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_write.png";
            }
        }
        public string Job_Detail_Break_Line
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/img_break_line.png";
            }
        }
        public string Job_Appbar_Share
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/appbar.share.rest.png";
            }
        }
        public string Job_Appbar_Collect
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/appbar.favs.rest.png";
            }
        }
        public string Job_Appbar_Path
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/appbar.path.rest.png";
            }
        }
        public string Job_Appbar_Remind
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/appbar.school.rest.png";
            }
        }
        public string Job_ChangeCity_Current_Location
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_current_location.png";
            }
        }
        public string Job_ChangeCity_Edit_Province
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_edit.png";
            }
        }
        public string Job_SetRemind_Header
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/bg_set_remind_header.png";
            }
        }
        public string Job_SetRemind_Change_Address
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_change_address.png";
            }
        }
        public string Job_SetRemind_Change_Time
        {
            get
            {
                return str + ResolutionHelper.Resolution + "/ic_change_time.png";
            }
        }
        //public Uri BestResolutionImage 
        // {
        // get { 
        //    switch (ResolutionHelper.Resolution) 
        //     { 
        //        case Resolutions.HD720p: return new Uri("Assets/MyImage.screen-720p.jpg", UriKind.Relative); 
        //        case Resolutions.WXGA: return new Uri("Assets/MyImage.screen-wxga.jpg", UriKind.Relative);
        //        case Resolutions.WVGA: return new Uri("Assets/MyImage.screen-wvga.jpg", UriKind.Relative); 
        //        default: throw new InvalidOperationException("Unknown resolution type"); 
        //     } 
        //   }
        //} 
    }
}
