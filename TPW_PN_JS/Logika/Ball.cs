using System.ComponentModel;
using TPW_PN_JS.Dane;

namespace TPW_PN_JS.Logika
{
    public class Ball : IBall
    {
        private double _x;
        private double _y;
        private double _speedX;
        private double _speedY;
        private double _mass;
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

        public double SpeedX
        {
            get => _speedX;
            set
            {
                _speedX = value;
                OnPropertyChanged(nameof(SpeedX));
            }
        }

        public double Mass
        {
            get => _mass;
            set
            {
                _mass = value;
            }
        }

        public double SpeedY
        {
            get => _speedY;
            set
            {
                _speedY = value;
                OnPropertyChanged(nameof(SpeedY));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

