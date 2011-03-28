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
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WorldSurfaceWindow()
        {
            dataModel = new DataModel();
            InitializeComponent();
            this.DataContext = dataModel;
            // Add handlers for Application activation events
            AddActivationHandlers();
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Aerial;
            //Contacts.PreviewContactDownEvent += 
            //    new RoutedEvent(rout);
            //SurfaceVEPushPin pp = new SurfaceVEPushPin(new InfoStrat.VE.VELatLong(58.9, 5.9));
            //pp.Content = "Stavanger";
           
            ////SVEMap.AddRegisteredPosition(pp, new InfoStrat.VE.VELatLong(58.9, 5.9));
            //SVEMap.Items.Add(pp);
            
        }

        //private void rout()
        //{
        //    bool f = true;
        //}

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for Application activation events
            RemoveActivationHandlers();
        }

        /// <summary>
        /// Adds handlers for Application activation events.
        /// </summary>
        private void AddActivationHandlers()
        {
            // Subscribe to surface application activation events
            ApplicationLauncher.ApplicationActivated += OnApplicationActivated;
            ApplicationLauncher.ApplicationPreviewed += OnApplicationPreviewed;
            ApplicationLauncher.ApplicationDeactivated += OnApplicationDeactivated;
        }

        /// <summary>
        /// Removes handlers for Application activation events.
        /// </summary>
        private void RemoveActivationHandlers()
        {
            // Unsubscribe from surface application activation events
            ApplicationLauncher.ApplicationActivated -= OnApplicationActivated;
            ApplicationLauncher.ApplicationPreviewed -= OnApplicationPreviewed;
            ApplicationLauncher.ApplicationDeactivated -= OnApplicationDeactivated;
        }

        /// <summary>
        /// This is called when application has been activated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationActivated(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when application is in preview mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationPreviewed(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        ///  This is called when application has been deactivated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationDeactivated(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void Rectangle_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

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
            SVEMap.FlyTo(new InfoStrat.VE.VELatLong(58.9, 5.9), -80, 0,30000, null);
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            if (isCollapsed)
            {
                MainGrid.ColumnDefinitions.First().Width = new GridLength(200);
                SPMenuHiding.Visibility = Visibility.Visible;
                ImageArrow.Source = new BitmapImage(new Uri( ImageArrow.Source.ToString().Replace("icon2.png", "icon1.png")));
            }
            else
            {
                MainGrid.ColumnDefinitions.First().Width = new GridLength(60);
                SPMenuHiding.Visibility = Visibility.Hidden;
                ImageArrow.Source = new BitmapImage(new Uri(ImageArrow.Source.ToString().Replace("icon1.png", "icon2.png")));
            }
            isCollapsed = !isCollapsed;
        }

        private int oldTimeStamp;
        private bool isInDoubleClick = false;

        private void SVEMap_PreviewContactDown(object sender, ContactEventArgs e)
        {
            if (RBNybrid.IsChecked.Value)
            {
                SurfaceVEPushPin pp = new SurfaceVEPushPin();
                var pos = e.Contact.GetPosition(SVEMap);
                //var p = SVEMap.PointFromScreen(pos);
                var ll = SVEMap.PointToLatLong(pos);
                pp.Longitude = ll.Longitude;
                pp.Latitude = ll.Latitude;
                pp.Content = string.Format("position1: x:{0} y:{1}, positionfrom screen x:{2} y:{3}, latitude: {4}, longitude: {5}",
                    pos.X, pos.Y, pos.X, pos.Y, ll.Longitude, ll.Latitude);
                SVEMap.Items.Add(pp);
                return;
            }

            if (e.Contact.IsFingerRecognized && isInDoubleClick && (e.Timestamp - oldTimeStamp) < 300)
            {
                var p = e.Contact.GetPosition(SVEMap);
                var ll = SVEMap.PointToLatLong(p);
                SVEMap.FlyTo(ll, SVEMap.Pitch, SVEMap.Yaw, SVEMap.Altitude / 5, null);
                isInDoubleClick = false;
            }
            else if (e.Contact.IsFingerRecognized && isInDoubleClick && (e.Timestamp - oldTimeStamp) > 299)
            {
                isInDoubleClick = false;
            }
            if (e.Contact.IsFingerRecognized && !isInDoubleClick)
            {
                oldTimeStamp = e.Timestamp;
                isInDoubleClick = true;
            }

            
        }

        //private void SVEMap_ContactDown(object sender, ContactEventArgs e)
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
        //}
    }
}