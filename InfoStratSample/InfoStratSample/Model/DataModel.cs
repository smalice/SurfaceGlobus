using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;

namespace InfoStratSample.Model
{
    public class DataModel : INotifyPropertyChanged
    {
        public static DataModel Instance;
        public Location SelectedLocation;

        private ObservableCollection<Location> _locations;
        
        public ObservableCollection<Location> Locations
        {
            get
            {
                return _locations;
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

        public DataModel()
        {
            Instance = this;
            ObservableCollection<Location> newLocations = new ObservableCollection<Location>();
            //newLocations.Add(new Location() { Name="stv"});

            XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Location>));
            TextReader textReader = new StreamReader("Resources\\CapgeminiLocations.xml");
            newLocations = (ObservableCollection<Location>)deserializer.Deserialize(textReader);
            textReader.Close();

            //XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Location>));
            //TextWriter textWriter = new StreamWriter("Resources\\CapgeminiLocations.xml");
            //serializer.Serialize(textWriter, newLocations);
            //textWriter.Close();

            _locations = newLocations;
            NotifyPropertyChanged("Locations");
        }
    }
}
