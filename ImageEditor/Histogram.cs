using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    class Histogram
    {
        private Bitmap sourceImage;
        private LockBitmap lockedSourceImage;
        public Statistic statistic;
        public delegate void Complete();
        private int height;
        private int width;

        public struct Statistic
        {
            public int[] Red;
            public int[] Green;
            public int[] Blue;
        }


        public Histogram(Bitmap sourceImage)
        {
            this.sourceImage = sourceImage;
            lockedSourceImage = new LockBitmap(this.sourceImage);
            lockedSourceImage.LockBits();
            height = lockedSourceImage.Height;
            width = lockedSourceImage.Width;
        }

        public void analyzeImage(Complete complete)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = lockedSourceImage.GetPixel(x, y);
                    statistic.Red[color.R] += 1;
                    statistic.Green[color.G] += 1;
                    statistic.Blue[color.B] += 1;
                }
            }

            complete();
        }
        


    }
}
