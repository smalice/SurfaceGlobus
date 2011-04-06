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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using System.Windows.Media.Animation;

namespace InfoStratSample
{
    /// <summary>
    /// Interaction logic for ZoomItem.xaml
    /// </summary>
    public partial class ZoomItem : SurfaceUserControl
    {
        public EventHandler<ContactEventArgs> ZoomIn;
        public EventHandler<ContactEventArgs> ZoomOut;

        public ZoomItem()
        {
            InitializeComponent();
        }

        public void StartAnimation()
        {
            Storyboard AnimStack = (Storyboard)FindResource("AniItem");
            AnimStack.Begin();
        }

        protected override void OnContactDown(ContactEventArgs e)
        {
            var b = true;
            var p = e.Contact.GetPosition(this);

            if (p.Y > 30 && p.Y < 50)
            {
                if (p.X > 30 && p.X < 45)
                {
                    ZoomOut(this, e);
                    this.Visibility = Visibility.Collapsed;
                }
                else if (p.X > 55 && p.X < 70)
                {
                    ZoomIn(this, e);
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
