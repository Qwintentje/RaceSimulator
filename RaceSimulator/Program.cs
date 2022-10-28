using Controller;
using RaceSimulator;

namespace Main
{
    public class Program
    {

        static void Main(string[] args)
        {
            /*            var competition = new Competition();
                        competition = Data.Initialize(competition);*/
            Data.Initialize();
            Data.NextRace();

            TrackVisualization.Initialize(Data.CurrentRace);



            for (; ; )
            {
                Thread.Sleep(100);
            }

        }


    }
}