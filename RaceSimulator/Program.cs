using System;
using Controller;
using Model;
using RaceSimulator;
using static Model.Section;

namespace Main
{
    public class Program
    {

        static void Main(string[] args)
        {
            var competition = new Competition();
            competition = Data.Initialize(competition);
            Data.NextRace();

            TrackVisualization.Initialize(Data.CurrentRace);

            //Data.CurrentRace.DriversChanged += TrackVisualization.OnDriversChanged;


            for (; ; )
            {
                Thread.Sleep(100);
            }

        }

        public static void RaceEndedEventHandler(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Next race");
            Data.NextRace();
            TrackVisualization.Initialize(Data.CurrentRace);
        }
    }
}