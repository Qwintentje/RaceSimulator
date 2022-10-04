using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;
using static Model.Section;
using static Model.IParticipant;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }


        public static Competition Initialize(Competition competition)
        {
            if (competition != null)
            {
                competition = new Competition();
            }
            Competition = competition;
         
            addParticipant(competition);
            addTracks(competition);

            return competition;
        }

        public static void addParticipant(Competition competition)
        {
            competition.Participants.Add(new Driver("Johan", 0, new Car( 100, false), TeamColors.Red));
            competition.Participants.Add(new Driver("Geral", 0, new Car( 100, false), TeamColors.Blue));
            competition.Participants.Add(new Driver("Bastiaan", 0, new Car( 100, false), TeamColors.Green));
            competition.Participants.Add(new Driver("Jan Willem", 0, new Car( 100, false), TeamColors.Yellow));
            competition.Participants.Add(new Driver("Valentijn", 0, new Car( 100, false), TeamColors.Blue));
        }

        public static void addTracks(Competition competition)
        {
            competition.Tracks.Enqueue(new Track("Felipe", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight
            }));

            competition.Tracks.Enqueue(new Track("Constantijn", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight
            }));
        }


        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();
            if (nextTrack == null || Competition.Participants == null) return;

            CurrentRace = new Race(nextTrack, Competition.Participants);
        }
   }
}
