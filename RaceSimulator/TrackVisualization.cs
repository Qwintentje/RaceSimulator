using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace RaceSimulator
{

    public static class TrackVisualization
    {
        public static int origRow;
        public static int origCol;



        #region graphics



        private static readonly string[] _finishHorizontal =
            {
            "-------",
            "   1   ",
            "       ",
            "   2   ",
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

        public static void WriteAt(string[] s, int x, int y)
        {
            try
            {

                foreach (string line in s)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(origCol + x, origRow + y);
                    Console.WriteLine(line);                  
                    y += +1;

                }

            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        public static void drawTrack(Track track)
        {
            WriteAt(_startHorizontal, 20, 0);
            WriteAt(_straigthHorizontal, 27, 0);
            WriteAt(_rightCornerVerticalDown, 34, 0);
            WriteAt(_straigthVerticalDown, 34, 7);
            WriteAt(_leftCornerHorizontalDown, 34, 14);
            WriteAt(_straigthHorizontal, 41, 14);
            WriteAt(_straigthHorizontal, 48, 14);
            WriteAt(_rightCornerVerticalDown, 55, 14);
            WriteAt(_rightCornerHorizontalDown, 55, 21);
            WriteAt(_straigthHorizontal, 48, 21);
            WriteAt(_straigthHorizontal, 41, 21);
            WriteAt(_leftCornerVerticalDown, 34, 21);
            WriteAt(_rightCornerHorizontalDown, 34, 28);
            WriteAt(_straigthHorizontal, 27, 28);
            WriteAt(_rightCornerVerticalUp, 20, 28);
            WriteAt(_straigthVerticalUp, 20, 21);
            WriteAt(_leftCornerHorizontalUp, 20, 14);
            WriteAt(_straigthHorizontal, 13, 14);
            WriteAt(_straigthHorizontal, 6, 14);
            WriteAt(_rightCornerVerticalUp, 0, 14);
            WriteAt(_straigthVerticalUp, 0, 7);
            WriteAt(_rightCornerHorizontalUp, 0, 0);
            WriteAt(_straigthHorizontal, 7, 0);
            WriteAt(_straigthHorizontal, 14, 0);
        }


        public static string VisualizeParticipantsOnTrack(IParticipant p1, IParticipant p2, string[] s)
        {
            p1.Name.Substring(0, 1);
            string p1Name = p1.Name;
            string p2Name = p2.Name;
            var p2Char = p2Name[0];
            var p1Char = p1Name[0];
            //create array of all arrays in region and loop through them
            string[][] allArrays = new string[][] { _startHorizontal, _straigthHorizontal, _rightCornerVerticalDown, _straigthVerticalDown, _leftCornerHorizontalDown, _straigthHorizontal, _straigthHorizontal, _rightCornerVerticalDown, _rightCornerHorizontalDown, _straigthHorizontal, _straigthHorizontal, _leftCornerVerticalDown, _rightCornerHorizontalDown, _straigthHorizontal, _rightCornerVerticalUp, _straigthVerticalUp, _leftCornerHorizontalUp, _straigthHorizontal, _straigthHorizontal, _rightCornerVerticalUp, _straigthVerticalUp, _rightCornerHorizontalUp, _straigthHorizontal, _straigthHorizontal };
            foreach (string[] array in allArrays)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Contains("1"))
                    {
                        array[i] = array[i].Replace("1", p1Char.ToString());
                    }
                    if (array[i].Contains("2"))
                    {
                        array[i] = array[i].Replace("2", p2Char.ToString());
                    }
                }
            }
            return " ";
        }
    }
}
