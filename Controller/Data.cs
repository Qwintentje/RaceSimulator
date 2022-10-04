using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;
using static Model.Section;

namespace Controller
{
    public static class Data
    {
        public static Competition competition { get; set; }
        public static Race CurrentRace { get; set; }
       
        
        public static void Initialize()
        {
            competition = new Competition();
           
            for (int i = 0; i < 3; i++)
            {
                addParticipant();
                addTracks();
            }
            CurrentRace = new Race(competition.NextTrack(), competition.Participants);
        }
        public static void addParticipant()
        {
            Driver driver = new Driver("test");
            driver.Equipment = new Car(100, 100, 100, false);
            competition.Participants.Add(driver);
        }

        public static void addTracks()
        {
            Track track = new Track("Super coole track", new SectionTypes[] {SectionTypes.StartGrid});
            competition.Tracks.Enqueue(track);
        }


        public static void NextRace()
        {
            if (competition.NextTrack() != null)
            {
                CurrentRace = new Race(competition.NextTrack(), competition.Participants);

            }
        }
    }
}
