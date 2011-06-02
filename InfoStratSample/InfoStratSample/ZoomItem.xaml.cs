using System;
using System.Windows;
using Microsoft.Surface.Presentation;
using System.Windows.Media.Animation;

namespace InfoStratSample
{

    public partial class ZoomItem
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
            var p = e.Contact.GetPosition(this);

            if (p.Y > 30 && p.Y < 50)
            {
                if (p.X > 30 && p.X < 45)
                {
                    ZoomOut(this, e);
                    Visibility = Visibility.Collapsed;
                }
                else if (p.X > 55 && p.X < 70)
                {
                    ZoomIn(this, e);
                    Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
