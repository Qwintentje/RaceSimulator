using System.Timers;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random = new Random();
        public static Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        public System.Timers.Timer timer;
        private int _interval = 500;
        public int SectionLength { get; set; } = 100;
        public Race(Track track, List<IParticipant> participants)

            
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            timer = new System.Timers.Timer(_interval);
            timer.Elapsed += OnTimedEvent;
            
            StartTimer();
            Initialize();
        }
        public void StartTimer()
        {
            timer.Start();
        }
        public void Initialize()
        {
            RandomizeEquipment();
            AddParticipantsToTrack(Track, Participants);
        }

        public void AddParticipantsToTrack(Track track, List<IParticipant> Participants)
        {
            int driversRemaining = Participants.Count;

            if (driversRemaining < 3) return;
            Console.WriteLine("Huidige track: " + track.Name);
            Stack<Section> startGrid = GetStartGrid(track);
            PlaceParticipantsOnTrack();
        }

        public void PlaceParticipantsOnTrack()
        {
            if (Participants == null) return;

            foreach (var participant in Participants)
            {
                foreach (var section in Track.Sections)
                {
                    var sectionData = GetSectionData(section);
                    if (sectionData != null && sectionData.AddParticipantToSection(participant))
                    {
                        break;
                    }
                }
            }

            //Zeker weten dat startGrid groter dan 0 is voordat we beginnen met toevoegen
            /** while (startGrid.Count > 0)
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
            } **/
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

        public static Stack<Section> GetStartGrid(Track track)
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
            foreach (var participant in Participants)
            {
                participant.Equipment.Quality = Convert.ToInt32(_random.Next(1, 10));
                participant.Equipment.Performance = Convert.ToInt32(_random.Next(1, 10));
                participant.Equipment.SectionSpeed = SectionLength / (participant.Equipment.Performance * participant.Equipment.Speed);
            }
        }
        
        public event EventHandler<DriversChangedEventArgs?> DriversChanged;
        
        private void OnTimedEvent(object? src, ElapsedEventArgs e)
        {
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
        }

        
    }
}