using Model;
using System.Security.AccessControl;
using System.Timers;
using static Model.Section;
using Timer = System.Timers.Timer;

namespace Controller
{
    public class Race
    {
        public const int SectionLength = 300;
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random = new Random();
        private Timer _timer;
        public static Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private int interval = 500;
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _timer = new Timer(interval);
            _timer.Elapsed += OnTimedEvent;

            StartTimer();
            Initialize();
        }

        public void StartTimer()
        {
            _timer.Start();
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
            Participants.Reverse();
            foreach (Driver participant in Participants)
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

        private void MoveParticipants()
        {
            foreach(Section section in Track.Sections)
            {
                SectionData sectionData = GetSectionData(section);

                foreach (IParticipant participant in Participants)
                {
                    int speed = participant.Equipment.Performance * participant.Equipment.Speed;
                    if(sectionData.Left != null)
                    {
                        sectionData.DistanceLeft += speed;
                        if (sectionData.DistanceLeft > SectionLength)
                        {
                            var next = Track.Sections.Find(section)?.Next;
                            if (Track.Sections.First != null)
                            {
                                Section nextSection;
                                if (next != null)
                                {
                                    nextSection = next.Value;
                                }
                                else
                                {
                                    nextSection = Track.Sections.First.Value;
                                }

                                SectionData nextSectionData = GetSectionData(nextSection);
                                if (nextSectionData.AddParticipantToSection(participant))
                                {
                                    nextSectionData.DistanceLeft = sectionData.DistanceLeft - SectionLength;
                                    sectionData.Left = null;
                                    sectionData.DistanceLeft = 0;
                                    
                                    if(section.SectionType == SectionTypes.Finish)
                                    {
                                       // Console.WriteLine($"{participant.Name} is over de finish gegaan");
                                    }
                                //    Console.WriteLine("naar volgende ggn");
                                }
                            }
                            
                        }
                    }

                    if(sectionData.Right != null)
                    {
                        sectionData.DistanceRight += speed;
                        if (sectionData.DistanceRight > SectionLength)
                        {
                            var next = Track.Sections.Find(section)?.Next;
                            if (Track.Sections.First != null)
                            {
                                Section nextSection;
                                if (next != null)
                                {
                                    nextSection = next.Value;
                                } else
                                {
                                    nextSection = Track.Sections.First.Value;
                                }
                                SectionData nextSectionData = GetSectionData(nextSection);
                                if (nextSectionData.AddParticipantToSection(participant))
                                {
                                    nextSectionData.DistanceRight = sectionData.DistanceRight - SectionLength;
                                    sectionData.Right = null;
                                    sectionData.DistanceRight = 0;

                                    if (section.SectionType == SectionTypes.Finish)
                                    {
                                        //   Console.WriteLine($"{participant.Name} is over de finish gegaan");
                                    }
                                    //    Console.WriteLine("naar volgende ggn");
                                } 
                            }
                        } 
                    }
                }
            }
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
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = Convert.ToInt32(_random.Next(1, 10));
                participant.Equipment.Performance = Convert.ToInt32(_random.Next(1, 10));
             //   participant.Equipment.SectionSpeed = 100 / (participant.Equipment.Speed * participant.Equipment.Performance);
            }
        }

        #region Events
        public event EventHandler<DriversChangedEventArgs?> DriversChanged;

        private void OnTimedEvent(object? o, ElapsedEventArgs e)
        {
            MoveParticipants();
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
        }
        #endregion
    }
}