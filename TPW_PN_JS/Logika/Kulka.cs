using System.ComponentModel;

namespace TPW_PN_JS.Logika
{
    public class Kulka : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _predkosc;

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        public double Predkosc
        {
            get => _predkosc;
            set
            {
                _predkosc = value;
                OnPropertyChanged(nameof(Predkosc));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

