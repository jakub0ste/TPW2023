﻿using System.ComponentModel;

namespace TPW_PN_JS.Dane
{
    public interface IBall : INotifyPropertyChanged
    {
        double X { get; set; }

        double Y { get; set; }

        double SpeedX { get; set; }

        double Mass { get; set; }

        double SpeedY { get; set; }

        //object Lock { get; set; }
    }
}
