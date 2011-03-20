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

namespace InfoStratSample
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class WorldSurfaceWindow : SurfaceWindow
    {
        private bool isCollapsed;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WorldSurfaceWindow()
        {
            InitializeComponent();

            // Add handlers for Application activation events
            AddActivationHandlers();
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Aerial;
        }


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
    }
}