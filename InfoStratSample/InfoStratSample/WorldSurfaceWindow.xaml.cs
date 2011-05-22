using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using InfoStratSample.Model;

namespace InfoStratSample
{

    public partial class WorldSurfaceWindow
    {
        private bool isCollapsed;
        private DataModel dataModel;
        private int oldTimeStamp;
        private bool isInDoubleClick = false;
        private const double pModSlider = 5.0;
        private bool manualSliderZoom = false;

        public WorldSurfaceWindow()
        {
            dataModel = new DataModel();
            InitializeComponent();
            this.DataContext = dataModel;
            AddActivationHandlers();
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Aerial;
            zoomItem.ZoomIn += MapZoomIn;
            zoomItem.ZoomOut += MapZoomOut;
            SVEMap.CameraChanged += new EventHandler<InfoStrat.VE.VECameraChangedEventArgs>(SVEMap_CameraChanged);
        }

        void SVEMap_CameraChanged(object sender, InfoStrat.VE.VECameraChangedEventArgs e)
        {
           var map = sender as InfoStrat.VE.VEMap;
           if (map != null)
           {
               if (manualSliderZoom == false)
               {
                   UpdateSlider(map.Altitude);    
               }
           } 
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

        }

        private void SVEMap_PreviewContactDown(object sender, ContactEventArgs e)
        {

        }

        protected override void  OnPreviewContactDown(ContactEventArgs e)
        {
            zoomItem.Visibility = Visibility.Collapsed;
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
			double currentHeight =SVEMap.Altitude * 5;
            SVEMap.FlyTo(ll, SVEMap.Pitch, SVEMap.Yaw, currentHeight, null);
        }

		private void UpdateSlider(double value)
		{
		    ZoomSlider.Value = (Math.Log(value, pModSlider));
		}

        private void ZoomSlider_ContactChanged(object sender, ContactEventArgs e)
        {
            SVEMap.FlyTo(SVEMap.GetCameraPosition(), SVEMap.Pitch, SVEMap.Yaw, Math.Pow(pModSlider, ZoomSlider.Value), null);   
        }

        private void ZoomSlider_ContactEnter(object sender, ContactEventArgs e)
        {
            manualSliderZoom = true;
        }

        private void ZoomSlider_ContactLeave(object sender, ContactEventArgs e)
        {
            manualSliderZoom = false;
        }

    }
}