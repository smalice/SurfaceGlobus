using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using InfoStrat.VE.NUI;
using InfoStratSample.Model;

namespace InfoStratSample
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class WorldSurfaceWindow : SurfaceWindow
    {
        private bool isCollapsed;
        private DataModel dataModel;
        private int oldTimeStamp;
        private bool isInDoubleClick = false;

        public WorldSurfaceWindow()
        {
            dataModel = new DataModel();
            InitializeComponent();
            this.DataContext = dataModel;
            AddActivationHandlers();
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Aerial;
            zoomItem.ZoomIn += MapZoomIn;
            zoomItem.ZoomOut += MapZoomOut;
        }

        #region unnecessary
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            RemoveActivationHandlers();
        }

        private void AddActivationHandlers()
        {
            ApplicationLauncher.ApplicationActivated += OnApplicationActivated;
            ApplicationLauncher.ApplicationPreviewed += OnApplicationPreviewed;
            ApplicationLauncher.ApplicationDeactivated += OnApplicationDeactivated;
        }

        private void RemoveActivationHandlers()
        {
            ApplicationLauncher.ApplicationActivated -= OnApplicationActivated;
            ApplicationLauncher.ApplicationPreviewed -= OnApplicationPreviewed;
            ApplicationLauncher.ApplicationDeactivated -= OnApplicationDeactivated;
        }

        private void OnApplicationActivated(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        private void OnApplicationPreviewed(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        private void OnApplicationDeactivated(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }
        #endregion

        private void RBAriel_Click(object sender, RoutedEventArgs e)
        {
            if (SVEMap != null)
                SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Aerial;
        }

        private void RBHybrid_Click(object sender, RoutedEventArgs e)
        {
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Hybrid;
        }

        private void RBRoad_Click(object sender, RoutedEventArgs e)
        {
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Road;
        }

        private void Stavanger_Click(object sender, RoutedEventArgs e)
        {
            SVEMap.FlyTo(new InfoStrat.VE.VELatLong(58.9008184604821, 5.70409334303234), -80, 0, 30000, null);
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            if (isCollapsed)
            {
                ImageArrow.Source = new BitmapImage(new Uri( ImageArrow.Source.ToString().Replace("icon2.png", "icon1.png")));
                SPMenuHiding.Visibility = Visibility.Visible;
                Storyboard OpenMenyStack = (Storyboard)FindResource("OnOpenMeny");
                OpenMenyStack.Begin();
            }
            else
            {
                ImageArrow.Source = new BitmapImage(new Uri(ImageArrow.Source.ToString().Replace("icon1.png", "icon2.png")));
                SPMenuHiding.Visibility = Visibility.Hidden;
                Storyboard CloseMenyStack = (Storyboard)FindResource("OnCloseMeny");
                CloseMenyStack.Begin();
            }
            isCollapsed = !isCollapsed;
        }

        private void SVEMap_ContactDown(object sender, ContactEventArgs e)
        {
            //if (e.Contact.IsFingerRecognized && isInDoubleClick && (e.Timestamp - oldTimeStamp) < 300)
            //{
            //    var p = e.Contact.GetPosition(SVEMap);
            //    var ll = SVEMap.PointToLatLong(p);
            //    SVEMap.FlyTo(ll, SVEMap.Pitch, SVEMap.Yaw, SVEMap.Altitude / 5, null);
            //    isInDoubleClick = false;
            //}
            //else if (e.Contact.IsFingerRecognized && isInDoubleClick && (e.Timestamp - oldTimeStamp) > 299)
            //{
            //    isInDoubleClick = false;
            //}
            //if (e.Contact.IsFingerRecognized && !isInDoubleClick)
            //{
            //    oldTimeStamp = e.Timestamp;
            //    isInDoubleClick = true;
            //}

        }

        private void SVEMap_PreviewContactDown(object sender, ContactEventArgs e)
        {
            //if (RBNybrid.IsChecked.Value)
            //{
            //    SurfaceVEPushPin pp = new SurfaceVEPushPin();
            //    var pos = e.Contact.GetPosition(SVEMap);
            //    //var p = SVEMap.PointFromScreen(pos);
            //    var ll = SVEMap.PointToLatLong(pos);
            //    pp.Longitude = ll.Longitude;
            //    pp.Latitude = ll.Latitude;
            //    pp.Content = string.Format("position1: x:{0} y:{1}, positionfrom screen x:{2} y:{3}, latitude: {4}, longitude: {5}",
            //        pos.X, pos.Y, pos.X, pos.Y, ll.Longitude, ll.Latitude);
            //    SVEMap.Items.Add(pp);
            //    return;
            //}

            //if (e.Contact.IsFingerRecognized && isInDoubleClick && (e.Timestamp - oldTimeStamp) < 300)
            //{
            //    var p = e.Contact.GetPosition(SVEMap);
            //    var ll = SVEMap.PointToLatLong(p);
            //    SVEMap.FlyTo(ll, SVEMap.Pitch, SVEMap.Yaw, SVEMap.Altitude / 5, null);
            //    isInDoubleClick = false;
            //}
            //else if (e.Contact.IsFingerRecognized && isInDoubleClick && (e.Timestamp - oldTimeStamp) > 299)
            //{
            //    isInDoubleClick = false;
            //}
            //if (e.Contact.IsFingerRecognized && !isInDoubleClick)
            //{
            //    oldTimeStamp = e.Timestamp;
            //    isInDoubleClick = true;
            //}

        }

        
        private void SVEMap_ContactHoldGesture(object sender, ContactEventArgs e)
        {
            zoomItem.Visibility = Visibility.Visible;
            zoomItem.StartAnimation();

            var p = e.Contact.GetPosition(MainGrid);
            var p1 = e.Contact.GetPosition(SVEMap);

            zoomItem.Margin = new Thickness(p.X-50.0, p.Y-50.0, 0, 0);
        }



        private void MapZoomIn(object sender, ContactEventArgs e)
        {
            var p = e.Contact.GetPosition(SVEMap);
            var ll = SVEMap.PointToLatLong(p);
            SVEMap.FlyTo(ll, SVEMap.Pitch, SVEMap.Yaw, SVEMap.Altitude / 5, null);
        }

        private void MapZoomOut(object sender, ContactEventArgs e)
        {
            var p = e.Contact.GetPosition(SVEMap);
            var ll = SVEMap.PointToLatLong(p);
            SVEMap.FlyTo(ll, SVEMap.Pitch, SVEMap.Yaw, SVEMap.Altitude * 5, null);
        }
    }
}