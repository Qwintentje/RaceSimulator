using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Applicatie
{
    public static class ImageClass
    {
        private static Dictionary<string, Bitmap> _imageDictionary = new();

        public static Bitmap returnBitmap(string URL)
        {
            if (_imageDictionary.ContainsKey(URL))
            {
                return _imageDictionary[URL];
            }
            else
            {
                Bitmap bitmap = new Bitmap(URL);
                _imageDictionary.Add(URL, bitmap);
                return bitmap;
            }
        }

        public static Bitmap CreateEmptyBitmap(int een, int twee)
        {
            string key = "Empty";
            if (!_imageDictionary.ContainsKey(key))
            {
                Bitmap bitmap = new Bitmap(een, twee);
                _imageDictionary.Add(key, bitmap);
                var graphics = Graphics.FromImage(bitmap);
                graphics.Clear(System.Drawing.Color.LightBlue);
            }
            return (Bitmap)_imageDictionary[key].Clone();
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static void Dispose()
        {
            _imageDictionary.Clear();
        }
    }
}
