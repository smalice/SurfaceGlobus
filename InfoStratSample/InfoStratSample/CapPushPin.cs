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

namespace InfoStratSample
{
    public class CapPushPin : SurfaceVEPushPin
    {
        #region Constructors

        static CapPushPin()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CapPushPin), new FrameworkPropertyMetadata(typeof(CapPushPin)));
        }

        public CapPushPin()
        {
        }

        #endregion

        protected override Point GetAnchorOffset()
        {
            return new Point(this.ActualWidth / 2, 0);
        }
    }

}
