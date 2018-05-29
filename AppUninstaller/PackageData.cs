using System.ComponentModel;

namespace AppUninstaller
{
    class PackageData
    {
        public bool IsActive = true;

        //public PackageData(string name, string path, string versionName, int versionCode)
        //{
        //    this.Name = name; this.Path = path; this.VersionName = versionName; this.VersionCode = versionCode;
        //}

        public PackageData(string name, string path)
        {
            this.Name = name; this.Path = path;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        //public string VersionName { get; private set; }
        //public int VersionCode { get; private set; }


        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
