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
using Microsoft.Phone.Controls;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Search.API.Options;
using Com.AMap.Search.API;
using Com.AMap.Maps.Api.Overlays;
using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.Events;
using System.Device.Location;
using GalaSoft.MvvmLight.Messaging;

namespace JobHub.Controls
{
    public partial class Job_Map : PhoneApplicationPage
    {
        MLngLat start;
        MLngLat end;
        public Job_Map()
        {
            InitializeComponent();
            start = (App.Current as App).CurrentLocation;
            AGeoCoordinateWatcher amapGeoCoordinateWatcher = new AGeoCoordinateWatcher();
            amapGeoCoordinateWatcher.PositionChanged += amapGeoCoordinateWatcher_PositionChanged;
            amapGeoCoordinateWatcher.StatusChanged += amapGeoCoordinateWatcher_StatusChanged;
            amapGeoCoordinateWatcher.Start();
            map.Center = start;
            
            Messenger.Default.Register<MLngLat>(this,"mLngLat", (mLngLat) =>
            {
                end = mLngLat;
                if (start != null)
                {
                    map.Children.Add(new MMarker()
                    {
                        LngLat = start,
                        IsEditable = true,
                        IconURL = "/Resource/Image/WVGA/bus_start_pic.jpg",
                        Anchor = new Point(0.5, 1)
                    });
                }
                if (end != null)
                {
                    map.Children.Add(new MMarker()
                    {
                        LngLat = end,
                        IconURL = "/Resource/Image/WVGA/bus_end_pic.jpg",
                        Anchor = new Point(0.5, 1)
                    });
                }
                RouteSearchWithOption();
            });
        }
        void amapGeoCoordinateWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {

        }
        void amapGeoCoordinateWatcher_PositionChanged(object sender, AGeoPositionChangedEventArgs e)
        {
            start = e.LngLat;
            (App.Current as App).CurrentLocation = start;
        }
       
        
      

       

        private void RouteSearchWithOption()
        {
            MRouteSearchOption m_RouteSearchOption = new MRouteSearchOption();
            m_RouteSearchOption.AvoidanceType = 1; //1:区域避让2：名称避让 3 :表示区域避让和名称避让。 
            m_RouteSearchOption.Ext = "1";/// 当 ext=0 时，表示不返回途经城市；当 ext=1 时，表示返回途经城市列表，城市做排重处理；当 ext=2 时，表示返回途经城市列表，城市不做排重处理。 
            m_RouteSearchOption.Name = ""; //起终点：石碑胡同 避让的一条到路名：
            m_RouteSearchOption.Per = 50;
            //  m_RouteSearchOption.Region = "116.388054,39.90539;116.3881,39.905743";

            m_RouteSearchOption.Config = "R";//路径导航时，config=R ,当查询导航路径距离时，config=CDR
            m_RouteSearchOption.Encode = "GBK";
            m_RouteSearchOption.RouteType = 0;


            m_RouteSearchOption.X1 = start.LngX;
            m_RouteSearchOption.Y1 = start.LatY;
            m_RouteSearchOption.X2 = end.LngX;
            m_RouteSearchOption.Y2 = end.LatY;
            m_RouteSearchOption.Xs = new double[0] { };
            m_RouteSearchOption.Ys = new double[0] { };
            MRouteSearch.RouteSearchWithOption(m_RouteSearchOption, CallBack);
        }


        void CallBack(MRouteSearchResult sender)
        {
            if (sender.Erro == null)
            {
                List<MOverlay> list = new List<MOverlay>();
                // MessageBox.Show("通过设置MRouteSearchOption对象属性进行导航路径搜索信息 如下：\nBounds" + sender.Bounds + "Coors：" + sender.Coors + "Count：" + sender.Count.ToString() + "Length：" + sender.Length.ToString() + " Routes.count：" + sender.Routes.Count + " SearchTime：" + sender.SearchTime.ToString());
                for (int i = 0; i < sender.Routes.Count; i++)
                {
                    //string url = "AccessorialInfo：" + sender.Routes[i].AccessorialInfo + "\n" + "Action：" + sender.Routes[i].Action + "\n" + "Coor：" + sender.Routes[i].Coor.ToString() + "\n" + "Direction：" + sender.Routes[i].Direction + "\n" + "DriveTime：" + sender.Routes[i].DriveTime + "\n" + "Form：" + sender.Routes[i].Form + "\n" + "Grade：" + sender.Routes[i].Grade + "\n" + "RoadLength：" + sender.Routes[i].RoadLength + "\n" + "RoadName：" + sender.Routes[i].RoadName + "\n" + "TextInfo：" + sender.Routes[i].TextInfo + "\n";
                    //MessageBox.Show("第" + i + "个路段信息 如下：\n" + url);



                    MLngLatCollection mc = sender.Routes[i].LngLats;//构造经纬度序列集合

                    MPolyline polyline = new MPolyline();
                    polyline.LngLats = mc;//mc为组成线的经纬度坐标串
                    polyline.LineColor = Utilities.HexToColor("#022672");//线的颜色
                    polyline.LineThickness = 6;//线的粗细
                    polyline.CanShowTip = false;
                    map.Children.Add(polyline);//添加到地图           
                    list.Add(polyline);
                }
                map.SetFitview(list);

                if (sender.ViaCities != null)
                {
                    for (int i = 0; i < sender.ViaCities.Count; i++)
                    {
                        string url = "CityEnglishName：" + sender.ViaCities[i].CityEnglishName + "\n" + "CityName：" + sender.ViaCities[i].CityName + "\n" + "Code：" + sender.ViaCities[i].Code + "\n" + "Telnum：" + sender.ViaCities[i].Telnum;
                      //  MessageBox.Show("途径第" + i + "个城市信息 如下：\n" + url);
                    }
                }
                else
                {
                    return;
                }

            }
            else
            {
                MessageBox.Show(sender.Erro.Message);
            }
        }
    }
}