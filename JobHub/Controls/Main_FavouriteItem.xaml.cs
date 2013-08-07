using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace JobHub.Controls
{
    public partial class Main_FavouriteItem : UserControl
    {
        public Main_FavouriteItem()
        {
            InitializeComponent();
        }
        public DependencyProperty CompanyProperty = DependencyProperty.Register("Company", typeof(string), typeof(Main_FavouriteItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            var control = e1 as Main_FavouriteItem;
            if (control != null && e2.NewValue != null)
            {
                control.company.Text = (string)e2.NewValue;
            }

        })));

        public DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(Main_FavouriteItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            var control = e1 as Main_FavouriteItem;
            if (control != null && e2.NewValue != null)
            {
                control.time.Text = (string)e2.NewValue;
            }

        })));
        public DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(Main_FavouriteItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            var control = e1 as Main_FavouriteItem;
            if (control != null && e2.NewValue != null)
            {
                control.status.Text = (string)e2.NewValue;
            }

        })));
        public DependencyProperty SchoolProperty = DependencyProperty.Register("School", typeof(string), typeof(Main_FavouriteItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            var control = e1 as Main_FavouriteItem;
            if (control != null && e2.NewValue != null)
            {
                control.school.Text = (string)e2.NewValue;
            }

        })));
        public DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(Main_FavouriteItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            var control = e1 as Main_FavouriteItem;
            if (control != null && e2.NewValue != null)
            {
                control.address.Text = (string)e2.NewValue;
            }

        })));

        public DependencyProperty BgProperty = DependencyProperty.Register("Bg", typeof(string), typeof(Main_FavouriteItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            var control = e1 as Main_FavouriteItem;
            if (control != null && e2.NewValue != null)
            {
                string s = (string)e2.NewValue;
                
                control.statusBorder.Background = new SolidColorBrush(ToColor(s));
            }

        })));


        public string Company
        {
            get
            {
                return base.GetValue(CompanyProperty) as string;
            }
            set
            {
                base.SetValue(CompanyProperty, value);
            }
        }
        public string Status
        {
            get
            {
                return base.GetValue(StatusProperty) as string;
            }
            set
            {
                base.SetValue(StatusProperty, value);
            }
        }
        public string Bg
        {
            get
            {
                return base.GetValue(BgProperty) as string;
            }
            set
            {
                base.SetValue(BgProperty, value);
            }
        }
        public string School
        {
            get
            {
                return base.GetValue(SchoolProperty) as string;
            }
            set
            {
                base.SetValue(SchoolProperty, value);
            }
        }
        public string Address
        {
            get
            {
                return base.GetValue(AddressProperty) as string;
            }
            set
            {
                base.SetValue(AddressProperty, value);
            }
        }
        public string Time
        {
            get
            {
                return base.GetValue(TimeProperty) as string;
            }
            set
            {
                base.SetValue(TimeProperty, value);
            }
        }
        public static Color ToColor(string colorName)
        {
            if (colorName.StartsWith("#"))
                colorName = colorName.Replace("#", string.Empty);
            int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);
            return new Color()
            {
                A = 255,
                R = Convert.ToByte((v >> 16) & 255),
                G = Convert.ToByte((v >> 8) & 255),
                B = Convert.ToByte((v >> 0) & 255)
            };
        }
    }
}
