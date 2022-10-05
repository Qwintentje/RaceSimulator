using System;
using Controller;
using Model;
using RaceSimulator;
using static Model.Section;

namespace Main
{   public class Program
    {

        static void Main(string[] args)
        {
            var competition = new Competition();
            competition = Data.Initialize(competition);
            Data.NextRace();

            TrackVisualization.Initialize(Data.CurrentRace);

            foreach (var item in Data.CurrentRace.Participants)
            {
                Console.WriteLine(item.Equipment.SectionSpeed);
            }

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}