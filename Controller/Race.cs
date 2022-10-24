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
        private readonly Dictionary<IParticipant, int> _lapsDriven = new Dictionary<IParticipant, int>();
        private readonly List<IParticipant> _isFinished = new List<IParticipant>();

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            
            _random = new Random(DateTime.Now.Millisecond);
            _timer = new Timer(interval);
            _timer.Elapsed += OnTimedEvent;

         Dictionary<IParticipant, int> _lapsDriven = new Dictionary<IParticipant, int>();
         List<IParticipant> _isFinished = new List<IParticipant>();

        StartTimer();
        Initialize();
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
            DriversChanged = null;
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
            //Check if amount of isFinished list is equal with Participants.count
            if (_isFinished.Count == Participants.Count)
            {
                RaceEnded.Invoke(this, EventArgs.Empty);
            }
            for (int i = 0; i < _positions.Count; i++)
            {
                var CurrentSectionData = _positions.ElementAt(i); //Current Section
                KeyValuePair<Section, SectionData> NextSectionData; // Next Section
                if (i == _positions.Count - 1)
                {
                    NextSectionData = _positions.ElementAt(0);
                }
                else
                {
                    NextSectionData = _positions.ElementAt(i + 1);
                }

                var IsSectionFinish = NextSectionData.Key.SectionType == SectionTypes.Finish;

                //Set speed for DriverPair1 (Current Section)
                if (CurrentSectionData.Value.Left != null)
                {
                    int DriverSpeed;
                    if (CurrentSectionData.Value.Left.Equipment.IsBroken)
                    {
                         DriverSpeed = 0;
                    }
                    else
                    {
                        DriverSpeed = CurrentSectionData.Value.Left.Equipment.Speed * CurrentSectionData.Value.Left.Equipment.Performance;
                        CurrentSectionData.Value.DistanceLeft += DriverSpeed;
                    }

                    // If driver crossed Section size threshhold, move participant to next section.
                    // Else, add DriverSpeed to distance traveled.
                    if (CurrentSectionData.Value.DistanceLeft >= SectionLength && (NextSectionData.Value.Right == null || NextSectionData.Value.Left == null))
                    {
                        if (NextSectionData.Value.Right == null)
                        {
                            if (NextSectionData.Value != null)
                            {
                                NextSectionData.Value.Right = CurrentSectionData.Value.Left;
                                if (IsSectionFinish)
                                {
                                    if (_lapsDriven.ContainsKey(NextSectionData.Value.Right))
                                    {
                                        _lapsDriven[NextSectionData.Value.Right]++;
                                    }
                                    else
                                    {
                                        _lapsDriven.Add(NextSectionData.Value.Right, 1);
                                    }
                                    if (_lapsDriven[NextSectionData.Value.Right] == 3)
                                    {
                                        _isFinished.Add(NextSectionData.Value.Right);
                                        NextSectionData.Value.Right = null;
                                    }
                                }
                            }
                        }
                        else if (NextSectionData.Value.Left == null)
                        {
                            NextSectionData.Value.Left = CurrentSectionData.Value.Left;
                            if (IsSectionFinish)
                            {
                                if (_lapsDriven.ContainsKey(NextSectionData.Value.Left))
                                {
                                    _lapsDriven[NextSectionData.Value.Left]++;
                                }
                                else
                                {
                                    _lapsDriven.Add(NextSectionData.Value.Left, 1);
                                }
                                if (_lapsDriven[NextSectionData.Value.Left] == 3)
                                {
                                    _isFinished.Add(NextSectionData.Value.Left);
                                    NextSectionData.Value.Right = null;
                                }
                            }
                        }

                        //Delete driver info from previous section
                        CurrentSectionData.Value.DistanceLeft = CurrentSectionData.Value.DistanceLeft - SectionLength;
                        CurrentSectionData.Value.Left = null;
                    }
                    else
                    {
                        CurrentSectionData.Value.DistanceLeft += DriverSpeed;
                    }
                }

                if (CurrentSectionData.Value.Right != null)
                {
                    int DriverSpeed;
                    if (CurrentSectionData.Value.Right.Equipment.IsBroken)
                    {
                        DriverSpeed = 0;
                    }
                    else
                    {
                        DriverSpeed = CurrentSectionData.Value.Right.Equipment.Speed * CurrentSectionData.Value.Right.Equipment.Performance;
                        CurrentSectionData.Value.DistanceRight += DriverSpeed;
                    }

                    // If driver crossed Section size threshhold, move participant to next section.
                    // Else, add DriverSpeed to distance traveled.
                    if (CurrentSectionData.Value.DistanceRight >= SectionLength && (NextSectionData.Value.Right == null || NextSectionData.Value.Left == null))
                    {
                        if (NextSectionData.Value.Right == null)
                        {
                            if (NextSectionData.Value != null)
                            {
                                NextSectionData.Value.Right = CurrentSectionData.Value.Right;
                                if (IsSectionFinish)
                                {
                                    if (_lapsDriven.ContainsKey(NextSectionData.Value.Right))
                                    {
                                        _lapsDriven[NextSectionData.Value.Right]++;
                                    }
                                    else
                                    {
                                        _lapsDriven.Add(NextSectionData.Value.Right, 1);
                                    }
                                    if (_lapsDriven[NextSectionData.Value.Right] == 3)
                                    {
                                        _isFinished.Add(NextSectionData.Value.Right);
                                        NextSectionData.Value.Right = null;
                                    }
                                }
                            }
                        }
                        else if (NextSectionData.Value.Left == null)
                        {
                            NextSectionData.Value.Left = CurrentSectionData.Value.Right;
                            if (IsSectionFinish)
                            {
                                if (_lapsDriven.ContainsKey(NextSectionData.Value.Left))
                                {
                                    _lapsDriven[NextSectionData.Value.Left]++;
                                }
                                else
                                {
                                    _lapsDriven.Add(NextSectionData.Value.Left, 1);
                                }
                                if (_lapsDriven[NextSectionData.Value.Right] == 3)
                                {
                                    _isFinished.Add(NextSectionData.Value.Right);
                                    NextSectionData.Value.Right = null;
                                }
                            }
                        }

                        //Delete driver from previous section
                        CurrentSectionData.Value.DistanceRight = CurrentSectionData.Value.DistanceRight - SectionLength;
                        CurrentSectionData.Value.Right = null;
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
                participant.Equipment.Quality = Convert.ToInt32(_random.Next(7, 10));
                participant.Equipment.Performance = Convert.ToInt32(_random.Next(7, 10));
                //   participant.Equipment.SectionSpeed = 100 / (participant.Equipment.Speed * participant.Equipment.Performance);
            }
        }

        #region Events
        public event EventHandler<DriversChangedEventArgs?> DriversChanged;
        public event EventHandler<EventArgs> RaceEnded;

        private void OnTimedEvent(object? o, ElapsedEventArgs e)
        {
            MoveParticipants();
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
        }
        #endregion

        public void Dispose()
        {
            //DriversChanged = null;
            _positions.Clear();
            _timer.Stop();
        }
    }
}