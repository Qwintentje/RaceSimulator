using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();

        public Race(Track track, List<IParticipant> participants)
        {
            _random = new Random(DateTime.Now.Millisecond);
            Track = track;
            Participants = participants;
            Initialize();
        }

        public void Initialize()
        {
            AddParticipantsToTrack(Track, Participants);
            RandomizeEquipment();
        } 

        public SectionData GetSectionData(Section section)
        {
            if (_positions.ContainsKey(section))
            {
                return _positions[section];
            }
            else
            {
                _positions.Add(section, new SectionData());
                return _positions[section];
            }
        }

        public Stack<Section> GetStartGrid(Track track)
        {
            var startGrid = new Stack<Section>();
            foreach (var section in track.Sections)
            {
                if (Section.SectionTypes.StartGrid == section.SectionType)
                {
                    startGrid.Push(section);
                }
            }
            return startGrid;
        }



        public void RandomizeEquipment()
        {
            foreach (IParticipant participent in Participants)
            {
                Random rNum = new Random(DateTime.Now.Millisecond);
                participent.Equipment.Performance = Convert.ToInt32(rNum.Next(0, 100));
                participent.Equipment.Quality = Convert.ToInt32(rNum.Next(0, 100));
            }
        }
        public void AddParticipantsToTrack(Track track, List<IParticipant> Participants)
        {
            int currentDriver = 0;
            int driversRemaining = Participants.Count;


            if (driversRemaining < 3) return;
            //Console.WriteLine("Huidige track: " + track.Name);
            Stack<Section> startGrid = GetStartGrid(track);
            PlaceParticipantsOnTrack(startGrid, currentDriver, driversRemaining, Participants);
        }

        public void PlaceParticipantsOnTrack(Stack<Section> startGrid, int currentDriver, int driversRemaining, List<IParticipant> participants)
        {
            //Zeker weten dat startGrid groter dan 0 is voordat we beginnen met toevoegen
            while (startGrid.Count > 0)
            {
                //krijg eerste van de stack, oftewel de start.
                var startSection = startGrid.Pop();
                var sectionData = GetSectionData(startSection);

                //Voeg driver toe aan de linkerkant van de track
                if (driversRemaining > 0)
                {
                    sectionData.Left = participants[currentDriver];
                    sectionData.StartTimeLeft = StartTime;
                    driversRemaining--;
                    currentDriver++;
                }

                //Voeg driver toe aan de rechter kant van de track
                if (driversRemaining > 0)
                {
                    sectionData.Right = participants[currentDriver];
                    sectionData.StartTimeRight = StartTime;
                    driversRemaining--;
                    currentDriver++;
                }
            }
        }



    }
}
