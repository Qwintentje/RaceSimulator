using Model;
using static Model.IParticipant;
using static Model.Section;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }
        public static event EventHandler<NextRaceEventArgs?> NextRaceEvent;

        public static Competition Initialize()
        {
            /*            if (competition != null)
                        {
                            competition = new Competition();
                        }*/
            Competition = new Competition();

            addParticipant(Competition);
            addTracks(Competition);

            return Competition;
        }

        public static void addParticipant(Competition competition)
        {
            competition.Participants.Add(new Driver("A", 0, new Car(), TeamColors.Red));
            competition.Participants.Add(new Driver("B", 0, new Car(), TeamColors.Blue));
            competition.Participants.Add(new Driver("C", 0, new Car(), TeamColors.Green));
            competition.Participants.Add(new Driver("D", 0, new Car(), TeamColors.Green));
        }

        public static void addTracks(Competition competition)
        {
            competition.Tracks.Enqueue(new Track("Felipe", new SectionTypes[]
            {
                    SectionTypes.Finish,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
            }));

            competition.Tracks.Enqueue(new Track("Constantijn", new SectionTypes[]
            {
                SectionTypes.Finish,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,

            }));
        }

        /*        public static void NextRace()
                {
                    if (CurrentRace != null)
                    {
                        CurrentRace.Dispose();
                    }

                    Track nextTrack = Competition.NextTrack();
                    if (nextTrack == null || Competition.Participants == null) return;

                    CurrentRace = new Race(nextTrack, Competition.Participants);
                }*/
        public static void NextRace()
        {
            if (CurrentRace != null) { CurrentRace.Dispose(); }
            Track track = Competition.NextTrack();

            if (track == null)
            {
                // Console.WriteLine("No more tracks");

                throw new Exception("No more tracks left.");
            }

            CurrentRace = new Race(track, Competition.Participants);
        }
    }
}
