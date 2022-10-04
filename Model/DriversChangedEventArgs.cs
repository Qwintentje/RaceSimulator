using System;
namespace Model
{
    public class DriversChangedEventArgs : EventArgs
    {
        public Track Track { get; set; }
        //constructor
        public DriversChangedEventArgs(Track? t)
        {
            Track = t;
        }
    }

    
    
}
