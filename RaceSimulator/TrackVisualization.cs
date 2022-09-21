using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace RaceSimulator
{
    internal static class TrackVisualization
    {
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

        private static readonly string[] _rightCornerVertical =
        {
            @"------\",
             "      |",
             "  # # |",
             "      |",
             "      |",
             "      |",
             "|     |",
        };

        private static readonly string[] _leftCornerVertical =
        {
             "|     |",
             "      |",
             "      |",
             "      |",
             "  # # |",
             "      |",
             "------/",
        };

        private static readonly string[] _rightCornerHorizontal =
            {
            "/------",
            "|      ",
            "|   #  ",
            "|      ",
            "|   #  ",
            "|      ",
            "|     -"
        };

        private static readonly string[] _leftCornerHorizontal =
        {
            "-------",
            "      |",
            "   #  |",
            "      |",
            "   #  |",
            "      |",
            "-      "

        };


        #endregion

        public static void drawTrack(Track track)
        {
            Console.WriteLine(track);  
        }
    }
}
