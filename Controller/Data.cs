using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ControllerTest
{
    public static class Data
    {
        public static Competition competition { get; set; }
        public static void Initialize(Competition c)
        {
            competition = new Competition();
            addParticipant();
            addParticipant();
            addParticipant();

        }
        public static void addParticipant()
        {
            Driver driver = new Driver();
            competition.Participants.Add(driver);
        }

        public static void addTracks()
        {
            Track track = new Track("test", new Section[1]);
            competition.Tracks.Enqueue(track);
        }
       
           
    }
}
