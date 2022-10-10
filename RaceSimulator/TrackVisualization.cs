using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;
using static Model.Section;
using static Controller.Race;

namespace RaceSimulator
{

    public static class TrackVisualization
    {
        private static Track _track;
        private static Race _currentRace;
        private static Direction _currentDirection = Direction.East;
        private static int _posX;
        private static int _posY;

        #region graphics

        private static readonly string[] _finishHorizontal =
            {
            "-------",
            "       ",
            "   1   ",
            "       ",
            "   2   ",
            "       ",
            "-------"
        };

        private static readonly string[] _finishVertical =
            {
            "|     |",
            "|     |",
            "|     |",
            "| 1 2 |",
            "|     |",
            "|     |",
            "|     |",
        };

        private static readonly string[] _startHorizontal =
        {
            "-------",
            "       ",
            "  1    ",
            "       ",
            "    2  ",
            "       ",
            "-------"
        };

        private static readonly string[] _startVertical =
        {
            "|     |",
            "| 1   |",
            "|     |",
            "|     |",
            "|     |",
            "|   2 |",
            "|     |",
        };

        private static readonly string[] _straigthHorizontal =
        {
            "-------",
            "       ",
            "   1   ",
            "       ",
            "   2   ",
            "       ",
            "-------"
        };

        private static readonly string[] _straigthVerticalDown =
        {
            "|     |",
            "|     |",
            "|     |",
            "| 2 1 |",
            "|     |",
            "|     |",
            "|     |",
        };

        private static readonly string[] _straigthVerticalUp =
{
            "|     |",
            "|     |",
            "|     |",
            "| 1 2 |",
            "|     |",
            "|     |",
            "|     |",
        };

        private static readonly string[] _rightCornerVerticalDown =
        {
            @"------\",
             "      |",
             "   1  |",
             "      |",
             "  2   |",
             "      |",
            @"\     |",
        };
        private static readonly string[] _rightCornerVerticalUp =
{
            @"|     \",
             "|      ",
             "|  1   ",
             "|      ",
             "|   2  ",
             "|      ",
            @"\------",
        };





        private static readonly string[] _rightCornerHorizontalUp =
            {
            "/------",
            "|      ",
            "|  1   ",
            "|      ",
            "|   2  ",
            "|      ",
            "|     /"
        };

        private static readonly string[] _rightCornerHorizontalDown =
        {
            "/     |",
            "      |",
            "  1   |",
            "      |",
            "   2  |",
            "      |",
            "------/"
        };



        private static readonly string[] _leftCornerVerticalUp =
        {
             "/     |",
             "      |",
             "   2  |",
             "      |",
             "  1   |",
             "      |",
             "------/",
        };


        private static readonly string[] _leftCornerVerticalDown =
        {
            @"/------",
             "|      ",
             "|  1   ",
             "|      ",
             "|   2  ",
             "|      ",
             "|      /",
        };



        private static readonly string[] _leftCornerHorizontalDown =
{
           @"|     \",
            "|      ",
            "|    1 ",
            "|      ",
            "|  2   ",
            "|      ",
           @"\------"

        };

        private static readonly string[] _leftCornerHorizontalUp =
{
           @"------\",
            "      |",
            "  1   |",
            "      |",
            "   2  |",
            "      |",
           @"\     |"

        };


        #endregion

        public static void Initialize(Race race)
        {
            _currentRace = race;
            _track = _currentRace.Track;

            InitializeConsole();
            DrawTrack(_track);
            _currentRace.DriversChanged += OnDriversChanged;
        }

        public static void InitializeConsole()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
        
        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            DrawTrack(e.Track);
            

        }

        public static void DrawTrack(Track track)
        {
            _posX = 50; 
            _posY = 0;

            Console.SetCursorPosition(_posX, _posY);
            int i = 1;
            foreach (Section section in track.Sections)
            {
                DrawSection(section);
            }
        }

        public static void DrawSection(Section section)
        {
            string[] sectionToDraw = VisualizeParticipantsOnTrack(GetSectionVisualization(section.SectionType, _currentDirection), _currentRace.GetSectionData(section));
            int currentY = _posY;
            
            foreach (string s in sectionToDraw)
            {
                try
                {
                    Console.SetCursorPosition(_posX, currentY);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(s);
                currentY++;
            }

            UpdateDirection(section.SectionType);
            UpdateCursorPosition();
        }

        public static string[] VisualizeParticipantsOnTrack(string[] sectionRow, SectionData sectionData)
        {
            string[] retValue = new String[sectionRow.Length];
            string left = " ";
            string right = " ";

            if (sectionData.Left != null)
            {
                left = sectionData.Left.Name.Substring(0, 1);
            }
           

            if (sectionData.Right != null)
            {
                right = sectionData.Right.Name.Substring(0, 1);
            }
            

            for (int i = 0; i < sectionRow.Length; i++)
            {
                retValue[i] = sectionRow[i].Replace("1", left).Replace("2", right);
            }
            
            return retValue;
        }

        //Methode om de Direction te updaten wanneer een corner wordt bereikt
        public static void UpdateDirection(SectionTypes sectionType)
        {
            switch (sectionType)
            {
                case SectionTypes.LeftCorner:
                    if (_currentDirection == Direction.North)
                    {
                        _currentDirection = Direction.West;
                    }
                    else if (_currentDirection == Direction.East)
                    {
                        _currentDirection = Direction.North;
                    }
                    else if (_currentDirection == Direction.South)
                    {
                        _currentDirection = Direction.East;
                    }
                    else if (_currentDirection == Direction.West)
                    {
                        _currentDirection = Direction.South;
                    }
                    break;
                case SectionTypes.RightCorner:
                    if (_currentDirection == Direction.North)
                    {
                        _currentDirection = Direction.East;
                    }
                    else if (_currentDirection == Direction.East)
                    {
                        _currentDirection = Direction.South;
                    }
                    else if (_currentDirection == Direction.South)
                    {
                        _currentDirection = Direction.West;
                    }
                    else if (_currentDirection == Direction.West)
                    {
                        _currentDirection = Direction.North;
                    }
                    break;
            }
        }

        //Methode om de cursor position aan te passen 
        public static void UpdateCursorPosition()
        {
            switch (_currentDirection)
            {
                case Direction.North:
                    _posY -= 7;
                    break;
                case Direction.East:
                    _posX += 7;
                    break;
                case Direction.South:
                    _posY += 7;
                    break;
                case Direction.West:
                    _posX -= 7;
                    break;
            }
        }

        private static string[] GetSectionVisualization(SectionTypes sectionType, Direction direction)
        {
            return sectionType switch
            {
                SectionTypes.StartGrid => _startHorizontal,
                SectionTypes.Finish => direction switch
                {
                    Direction.North => _finishVertical,
                    Direction.East => _finishHorizontal,
                    Direction.South => _finishVertical,
                    Direction.West => _finishHorizontal,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.Straight => direction switch
                {
                    Direction.North => _straigthVerticalUp,
                    Direction.East => _straigthHorizontal,
                    Direction.South => _straigthVerticalDown,
                    Direction.West => _straigthHorizontal,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.RightCorner => direction switch
                {
                    Direction.North => _rightCornerHorizontalUp,
                    Direction.East => _rightCornerVerticalDown,
                    Direction.South => _rightCornerHorizontalDown,
                    Direction.West => _rightCornerVerticalUp,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.LeftCorner => direction switch
                {
                    Direction.North => _leftCornerHorizontalUp,
                    Direction.East => _leftCornerVerticalUp,
                    Direction.South => _leftCornerHorizontalDown,
                    Direction.West => _leftCornerVerticalDown,
                    _ => throw new NotImplementedException()
                },
                _ => throw new NotImplementedException()
            };
        }
    }
}
