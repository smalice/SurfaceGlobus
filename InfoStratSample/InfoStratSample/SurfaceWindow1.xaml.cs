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
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
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

        private void SurfaceRadioButton_Click1(object sender, RoutedEventArgs e)
        {
            if (SVEMap != null)
                SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Aerial;
        }

        private void SurfaceRadioButton_Click2(object sender, RoutedEventArgs e)
        {
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Hybrid;
        }

        private void SurfaceRadioButton_Click3(object sender, RoutedEventArgs e)
        {
            SVEMap.MapStyle = InfoStrat.VE.VEMapStyle.Road;
        }

        private void SurfaceButton_Click2(object sender, RoutedEventArgs e)
        {
            SVEMap.FlyTo(new InfoStrat.VE.VELatLong(58.9, 5.9), -80, 0,30000, null);
            //new EventHandler(MetodRotate)
        }

        void MetodRotate(object sender, EventArgs args)
        {
            SVEMap.Yaw = 0;
            SVEMap.Pitch = -90;
        }
    }
}