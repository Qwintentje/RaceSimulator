using Controller;
using Model;
using RaceSimulator;

namespace Main
{   public class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            TrackVisualization.drawTrack(new Track("zandvoort", new Section[1]));

            for (; ; )
            {
                Thread.Sleep(100);
            }


        }
    }
}