using System.Timers;
using Model;
using static Model.Section;
using Timer = System.Timers.Timer;

namespace Controller
{
    public class Race
    {
        public const int SectionLength = 150;
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random = new Random();
        private Timer _timer;
        public static Dictionary<Section, SectionData> _positions;
        private int interval = 500;
        public static Dictionary<IParticipant, int> _lapsDriven;
        private static List<IParticipant> _isFinished;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;

            _random = new Random(DateTime.Now.Millisecond);
            _timer = new Timer(interval);
            _timer.Elapsed += OnTimedEvent;
            _positions = new Dictionary<Section, SectionData>();

            _lapsDriven = new Dictionary<IParticipant, int>();
            _isFinished = new List<IParticipant>();

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
        }

        private void MoveParticipants()
        {
            if (_isFinished.Count == Participants.Count)
            {
                RaceEnded?.Invoke(this, EventArgs.Empty);
            };

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
                                        if (_lapsDriven[NextSectionData.Value.Right] == 2)
                                        {
                                            _isFinished.Add(NextSectionData.Value.Right);
                                            NextSectionData.Value.Right = null;
                                            NextSectionData.Value.DistanceRight = 0;
                                        }
                                        else
                                        {
                                            _lapsDriven[NextSectionData.Value.Right]++;
                                        }
                                    }
                                    else
                                    {
                                        _lapsDriven.Add(NextSectionData.Value.Right, 1);
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
                                    if (_lapsDriven[NextSectionData.Value.Left] == 2)
                                    {
                                        _isFinished.Add(NextSectionData.Value.Left);
                                        NextSectionData.Value.Left = null;
                                        NextSectionData.Value.DistanceLeft = 0;
                                    }
                                    else
                                    {
                                        _lapsDriven[NextSectionData.Value.Left]++;
                                    }
                                }
                                else
                                {
                                    _lapsDriven.Add(NextSectionData.Value.Left, 1);
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
                                        if (_lapsDriven[NextSectionData.Value.Right] == 2)
                                        {
                                            _isFinished.Add(NextSectionData.Value.Right);
                                            NextSectionData.Value.Right = null;
                                            NextSectionData.Value.DistanceRight = 0;
                                        }
                                        else
                                        {
                                            _lapsDriven[NextSectionData.Value.Right]++;
                                        }
                                    }
                                    else
                                    {
                                        _lapsDriven.Add(NextSectionData.Value.Right, 1);
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
                                    if (_lapsDriven[NextSectionData.Value.Left] == 2)
                                    {
                                        _isFinished.Add(NextSectionData.Value.Left);
                                        NextSectionData.Value.Left = null;
                                        NextSectionData.Value.DistanceLeft = 0;
                                    }
                                    else
                                    {
                                        _lapsDriven[NextSectionData.Value.Left]++;
                                    }
                                }
                                else
                                {
                                    _lapsDriven.Add(NextSectionData.Value.Left, 1);
                                }
                            }
                        }

                        //Delete driver from previous section
                        CurrentSectionData.Value.DistanceRight = CurrentSectionData.Value.DistanceRight - SectionLength;
                        CurrentSectionData.Value.Right = null;
                    }
                    else
                    {
                        CurrentSectionData.Value.DistanceRight += DriverSpeed;
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
                participant.Equipment.Quality = Convert.ToInt32(_random.Next(1, 5));
                participant.Equipment.Performance = Convert.ToInt32(_random.Next(5, 10));
            }
        }

        public void updateEquipment()
        {
            foreach (var participant in Participants)
            {
                if (!participant.Equipment.IsBroken)
                {
                    if ((_random.Next(0, 100) * participant.Equipment.Quality) < 5)
                    {
                        participant.Equipment.IsBroken = true;
                    }
                    else
                    {
                        // participant.Equipment.IsBroken = false;
                    }
                }
                else
                {
                    if (_random.Next(0, 100) < 20)
                    {
                        participant.Equipment.IsBroken = false;
                    }
                    else
                    {
                        //participant.Equipment.IsBroken = true;
                    }
                }
            }
        }
        #region Events
        public event EventHandler<DriversChangedEventArgs?> DriversChanged;
        public event EventHandler<EventArgs> RaceEnded;
        private void OnTimedEvent(object? o, ElapsedEventArgs e)
        {
            MoveParticipants();
            updateEquipment();
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
        }

        #endregion

        public void Dispose()
        {
            DriversChanged = null;
            RaceEnded = null;
            _positions.Clear();
            _timer.Stop();
        }

        public bool IsFinished()
        {
            if (_isFinished.Count == Participants.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}