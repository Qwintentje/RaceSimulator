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
            "   #   ",
            "       ",
            "   #   ",
            "-------" 
        };

        private static readonly string[] _finishVertical =
            {
            "|     |",
            "|     |",
            "|     |",
            "| # # |",
            "|     |",
            "|     |",
            "|     |",
        };

        private static readonly string[] _startHorizontal =
        {
            "-------",
            "       ",
            "     # ",
            "       ",
            " #     ",
            "       ",
            "-------"
        };

        private static readonly string[] _startVertical =
        {
            "|     |",
            "| #   |",
            "|     |",
            "|     |",
            "|     |",
            "|   # |",
            "|     |",
        };

        private static readonly string[] _straigthHorizontal =
        {
            "-------",
            "       ",
            "    #  ",
            "       ",
            "    #  ",
            "       ",
            "-------"
        };

        private static readonly string[] _straigthVertical =
        {
            "|     |",
            "|     |",
            "|     |",
            "|     |",
            "| # # |",
            "|     |",
            "|     |",
        };

        private static readonly string[] _rightCornerVerticalDown =
        {
            @"------\",
             "      |",
             "  # # |",
             "      |",
             "      |",
             "      |",
             "|     |",
        };





        private static readonly string[] _rightCornerHorizontalUp =
            {
            "/------",
            "|      ",
            "|   #  ",
            "|      ",
            "|   #  ",
            "|      ",
            "|     /"
        };



        private static readonly string[] _leftCornerVerticalUp =
        {
             "/     |",
             "      |",
             "      |",
             "      |",
             "  # # |",
             "      |",
             "------/",
        };

                private static readonly string[] _rightCornerVerticalUp =
{
             @"|     \",
             "|      ",
             "|      ",
             "|      ",
             "|      ",
             "|      ",
            @"\------",
        };

        private static readonly string[] _leftCornerHorizontalDown =
{
           @"|     \",
            "|      ",
            "|  #   ",
            "|      ",
            "|  #   ",
            "|      ",
           @"\------"

        };

        private static readonly string[] _leftCornerHorizontalUp =
{
           @"------\",
            "      |",
            "   #  |",
            "      |",
            "   #  |",
            "      |",
           @"\      "

        };


        #endregion

        public static void WriteAt(string[] s, int x, int y)
        {
            try
            {
                
                foreach (string line in s)
                {
                    Console.SetCursorPosition(origCol + x, origRow + y);
                    Console.WriteLine(line);
                    //x += 1;
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



        }
    }
}
