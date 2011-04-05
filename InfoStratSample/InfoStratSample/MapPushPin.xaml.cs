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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InfoStrat.VE.NUI;
using InfoStratSample.Model;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;


namespace InfoStratSample
{
    /// <summary>
    /// Interaction logic for MapPushPin.xaml
    /// </summary>
    public partial class MapPushPin : SurfaceVEPushPin
    {
        bool visible;

        public MapPushPin()
        {
            InitializeComponent();
        }

        private void SurfaceButton_Click(object sender, RoutedEventArgs e)
        {
            Model.DataModel.Instance.SelectedLocation = this.DataContext as Location;
            if (visible)
            {
                ItemInfo.Visibility = Visibility.Collapsed;
                visible = false;
                this.ZIndex = 1;
            }
            else 
            {
                ItemInfo.Visibility = Visibility.Visible;
                visible = true;
                this.ZIndex = 10;
            }
        }
    }
}
