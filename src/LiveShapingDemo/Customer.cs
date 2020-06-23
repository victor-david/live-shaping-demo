using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiveShapingDemo
{
    public class Customer : INotifyPropertyChanged
    {
        #region Private
        private int id;
        private string name;
        private int balance;
        #endregion

        public const int MinBalance = 0;
        public const int MaxBalance = 500000;

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #region Public properties
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
       
        public int Balance
        {
            get => balance;
            set => SetProperty(ref balance, value);
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}