using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace NetduinoControl.Phone
{
    public class Settings : INotifyPropertyChanged
    {
        private const string FileName = "Settings.xml";

        private static readonly Settings Default = new Settings { IPAddress = "192.168.0.109" };

        private string _ipAddress;
        public string IPAddress
        {
            get { return _ipAddress; }
            set
            {
                if (_ipAddress != value)
                {
                    _ipAddress = value;
                    OnPropertyChanged("IPAddress");
                }
            }
        }

        public async static Task<Settings> LoadAsync()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file;
            try
            {
                file = await folder.GetFileAsync(FileName);
            }
            catch (Exception)
            {
                return Settings.Default;
            }

            Stream stream = await file.OpenStreamForReadAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            return (Settings)serializer.Deserialize(stream);
        }

        public async static Task SaveAsync(Settings obj)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

            Stream stream = await file.OpenStreamForWriteAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            serializer.Serialize(stream, obj);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
