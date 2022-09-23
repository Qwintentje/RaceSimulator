using Controller;
using Model;
using RaceSimulator;
using static Model.Section;

namespace Main
{   public class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            new Race(new Track("Yolo", new SectionTypes[] { SectionTypes.StartGrid }), Data.competition.Participants);
            TrackVisualization.drawTrack(new Track("Yolo", new SectionTypes[] { SectionTypes.StartGrid }));

            for (; ; )
            {
                Thread.Sleep(100);
            }


        }
    }
}