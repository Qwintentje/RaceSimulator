using System.Drawing;
using System.Windows.Media.Imaging;
using Controller;
using Model;
namespace WPF_Applicatie
{
    public class WPFVisualization
    {
        private static Race _currentRace;

        #region Graphics

        private const string Broken = @".\\Images\\Broken.png";

        private const string CarBlue = @".\\Images\\CarBlue.png";
        private const string CarGreen = @".\\Images\\CarGreen.png";
        private const string CarRed = @".\\Images\\CarRed.png";
        private const string CarYellow = @".\\Images\\CarYellow.png";

        private const string FinishLine = @"./Images/Finish.png";
        private const string StartGrid = @"./Images/StartGrid.png";
        private const string StraightHorizontal = @"./Images/StraightHorizontal.png";
        private const string StraightVertical = @"./Images/StraightVertical.png";

        private const string Turn0 = @"./Images/Turn0.png";
        private const string Turn2 = @"./Images/Turn2.png";
        private const string Turn3 = @"./Images/Turn3.png";
        private const string Turn4 = @"./Images/Turn4.png";

        #endregion

        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap bitmap = ImageClass.CreateEmptyBitmap(100, 100);
            var graphics = Graphics.FromImage(bitmap);
            return ImageClass.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        public static void Initialize(Race race)
        {
            _currentRace = race;
        }
    }
}
