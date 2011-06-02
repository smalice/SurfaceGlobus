using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using Blake.NUI.WPF.Touch;
using InfoStrat.MotionFx;
using Microsoft.Surface.Presentation.Controls;
using Blake.NUI.WPF.Utility;
using Blake.NUI.WPF.SurfaceToolkit.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HandTesting
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(String info)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        #endregion

        #region Properties

        private ObservableCollection<ImageModel> _images = new ObservableCollection<ImageModel>();
        public ObservableCollection<ImageModel> Images
        {
            get
            {
                return _images;
            }
        }

        #region MotionTrackingClient

        /// <summary>
        /// The <see cref="MotionTrackingClient" /> property's name.
        /// </summary>
        public const string MotionTrackingClientPropertyName = "MotionTrackingClient";

        private MotionTrackingClient _motionTrackingClient = null;

        public MotionTrackingClient MotionTrackingClient
        {
            get
            {
                return _motionTrackingClient;
            }

            set
            {
                if (_motionTrackingClient == value)
                {
                    return;
                }

                var oldValue = _motionTrackingClient;
                _motionTrackingClient = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(MotionTrackingClientPropertyName);
            }
        }

        #endregion

        #endregion

        public SurfaceWindow1()
            : this(false)
        { }

        Point centerPoint;

        ImageModel desertImage;
        ImageModel jellyImage;
        ImageModel penguinImage;

        private bool isGravityOn;
        private readonly DispatcherTimer _gravityTimer;

        public SurfaceWindow1(bool isHorizontal)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(SurfaceWindow1_Loaded);

            MotionTrackingClient = new MotionTrackingClient(this);
            
            NativeTouchDevice.RegisterEvents(this);

            InitData();
            this.DataContext = this;

            _gravityTimer = new DispatcherTimer();
            _gravityTimer.Tick += GetGavityOn;
            _gravityTimer.Interval = TimeSpan.FromSeconds(1.0 / 5.0);
            
        }

        private void InitData()
        {
            desertImage = new ImageModel(new Uri(@"Images\capegemini_07.jpg", UriKind.Relative));
            jellyImage = new ImageModel(new Uri(@"Images\capegemini_12.jpg", UriKind.Relative));
            penguinImage = new ImageModel(new Uri(@"Images\temporaryLogo.png", UriKind.Relative));
            Images.Add(desertImage);
            Images.Add(jellyImage);
            Images.Add(penguinImage);
        }

        void SurfaceWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            centerPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
        }

        private void ButtonShutDown_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                w.Close();
            }
        }

        private void MotionToggleButtonDesert_Checked(object sender, RoutedEventArgs e)
        {
            var svi = scatterview.ItemContainerGenerator.ContainerFromItem(desertImage) as ScatterViewItem;
            SurfaceAnimateUtility.ThrowSVI(svi, centerPoint, 0, 0.0, 1.0);
            desertImage.IsVisible = true;
        }

        private void MotionToggleButtonDesert_Unchecked(object sender, RoutedEventArgs e)
        {
            var svi = scatterview.ItemContainerGenerator.ContainerFromItem(desertImage) as ScatterViewItem;
            SurfaceAnimateUtility.ThrowSVI(svi, new Point(-600, -450), -30, 0.0, 1.0);
            desertImage.IsVisible = false;
        }


        private void MotionToggleButtonJellyfish_Checked(object sender, RoutedEventArgs e)
        {
            var svi = scatterview.ItemContainerGenerator.ContainerFromItem(jellyImage) as ScatterViewItem;
            SurfaceAnimateUtility.ThrowSVI(svi, centerPoint, 0, 0.0, 1.0);
            jellyImage.IsVisible = true;
        }

        private void MotionToggleButtonJellyfish_Unchecked(object sender, RoutedEventArgs e)
        {
            var svi = scatterview.ItemContainerGenerator.ContainerFromItem(jellyImage) as ScatterViewItem;
            SurfaceAnimateUtility.ThrowSVI(svi, new Point(-600, -450), -30, 0.0, 1.0);
            jellyImage.IsVisible = false;
        }


        private void MotionToggleButtonPenguins_Checked(object sender, RoutedEventArgs e)
        {
            var svi = scatterview.ItemContainerGenerator.ContainerFromItem(penguinImage) as ScatterViewItem;
            SurfaceAnimateUtility.ThrowSVI(svi, centerPoint, 0, 0.0, 1.0);
            penguinImage.IsVisible = true;
        }

        private void MotionToggleButtonPenguins_Unchecked(object sender, RoutedEventArgs e)
        {
            var svi = scatterview.ItemContainerGenerator.ContainerFromItem(penguinImage) as ScatterViewItem;
            SurfaceAnimateUtility.ThrowSVI(svi, new Point(-600, -450), -30, 0.0, 1.0);
            penguinImage.IsVisible = false;
        }

        private void MotionToggleButtonGravity_Checked(object sender, RoutedEventArgs e)
        {
            isGravityOn = true;
            _gravityTimer.Start();
            
        }

        private void GetGavityOn(object o, EventArgs e)
        {
            foreach (var imageModel in Images)
            {
                if (imageModel.IsVisible)
                {
                    var svi = scatterview.ItemContainerGenerator.ContainerFromItem(imageModel) as ScatterViewItem;
                    if (svi.ActualCenter.Y < this.ActualHeight - 100)
                    {
                        imageModel.dY += 0.25;
                        SurfaceAnimateUtility.ThrowSVI(svi, new Point(svi.ActualCenter.X, svi.ActualCenter.Y + imageModel.dY),
                            svi.ActualOrientation, 0.0, 0.1);
                    }
                    else 
                    {
                        imageModel.dY = 0;
                    }
                    
                }
            }
        }

        private void MotionToggleButtonGravity_Unchecked(object sender, RoutedEventArgs e)
        {
            isGravityOn = false;
            _gravityTimer.Stop();
            foreach (var imageModel in Images)
            {
                imageModel.dY = 0;
            }
        }
    }
}