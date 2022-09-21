using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;

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
//            Driver driver = new Driver();
            competition.Participants.Add(new Driver());
        }

        public static void addTracks()
        {
            Track track = new Track("Super coole track", new Section[1]);
            competition.Tracks.Enqueue(track);
        }
<<<<<<< Updated upstream
<<<<<<< Updated upstream

        public static void NextRace()
        {
            if (competition.NextTrack() != null)
            {
                CurrentRace = new Race(competition.NextTrack(), competition.Participants);

            }
        }
       
=======
=======
>>>>>>> Stashed changes
        
>>>>>>> Stashed changes
           
    }
}
