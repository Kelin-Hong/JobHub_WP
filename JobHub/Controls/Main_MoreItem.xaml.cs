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
using System.Windows.Media.Imaging;
namespace JobHub.Controls
{
    public partial class Main_MoreItem : UserControl
    {
        public Main_MoreItem()
        {
            InitializeComponent();
        }
        public DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(string), typeof(Main_MoreItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            Main_MoreItem control = (Main_MoreItem)e1;
            if (e1 != null && e2.NewValue != null)
            {
                control.image.Source = new BitmapImage(new Uri((string)e2.NewValue, UriKind.Relative));
            }
        })));
        public DependencyProperty TileNameProperty = DependencyProperty.Register("TileName", typeof(string), typeof(Main_MoreItem), new PropertyMetadata(new PropertyChangedCallback((e1, e2) =>
        {
            Main_MoreItem control = (Main_MoreItem)e1;
            if (e1 != null && e2.NewValue != null)
            {
                control.name.Text = (string)e2.NewValue;
            }
        })));
        public string TileName
        {
            get
            {
                return base.GetValue(TileNameProperty) as string;
            }
            set
            {
                base.SetValue(TileNameProperty, value);
            }
        }
        public string Image
        {
            get
            {
                return base.GetValue(ImageProperty) as string;
            }
            set
            {
                base.SetValue(ImageProperty, value);
            }
        }
    }
}
