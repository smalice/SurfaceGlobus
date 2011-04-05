using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InfoStratSample.Model
{
    public class Location : INotifyPropertyChanged
    {
        private string country;

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                NotifyPropertyChanged("Country");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set 
            { 
                longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }
        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set 
            { 
                latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }
        private double altitude;

        public double Altitude
        {
            get { return altitude; }
            set 
            { 
                altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set 
            { 
                address = value;
                NotifyPropertyChanged("Address");
            }
        }
        private string tlf;

        public string Tlf
        {
            get { return tlf; }
            set 
            { 
                tlf = value;
                NotifyPropertyChanged("Tlf");
            }
        }

        private string countryTlf;

        public string CountryTlf
        {
            get { return countryTlf; }
            set 
            { 
                countryTlf = value;
                NotifyPropertyChanged("CountryTlf");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        #endregion

        
    }
}
