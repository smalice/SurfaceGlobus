using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandTesting
{
    public class ImageModel
    {
        public bool IsVisible;

        public double dY;

        public Uri ImageSource { get; set; }

        public ImageModel(Uri source)
        {
            this.ImageSource = source;
        }
    }
}
