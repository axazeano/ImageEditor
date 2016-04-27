using System.Drawing;

namespace ImageEditor.Effects
{
    class GrayWorld: BaseEffect
    {
        protected int redSum, greenSum, blueSum;
        protected double redGlobal, greenGlobal, blueGlobal;
        protected double redDiv, greenDiv, blueDiv;
        protected int redNew, greenNew, blueNew;
        protected double avg;

        public GrayWorld(Bitmap sourceImage, Selection selection): base(sourceImage, selection)
        {
            calculateDivValues();
            name = "Gray world";
        }

        public GrayWorld(Bitmap sourceImage) : base(sourceImage)
        {
            calculateDivValues();
            name = "Gray world";
        }


        protected override void ProcceedEffect()
        {
            for (int y = startY; y < height; y++)
            {
                for (int x = startX; x < width; x++)
                {
                    Color color = lockedSourceImage.GetPixel(x, y);
                    redNew = (int)(color.R * redDiv);
                    greenNew = (int)(color.G * greenDiv);
                    blueNew = (int)(color.B * blueDiv);
                    lockedResultImage.SetPixel(x, y, Color.FromArgb(safeColor(redNew), safeColor(greenNew), safeColor(blueNew)));
                }
            }
        }


        private void calculateSumValues()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = lockedSourceImage.GetPixel(x, y);
                    redSum += color.R;
                    greenSum += color.G;
                    blueSum += color.B;
                }
            }
        }

        private void calculateGlobalValues()
        {
            calculateSumValues();

            int size = height * width;
            redGlobal = 1f / size * redSum;
            greenGlobal = 1f / size * greenSum;
            blueGlobal = 1f / size * blueSum;
            avg = (redGlobal + greenGlobal + blueGlobal) / 3;
        }

        private void calculateDivValues()
        {
            calculateGlobalValues();

            redDiv = avg / redGlobal;
            greenDiv = avg / greenGlobal;
            blueDiv = avg / blueGlobal;
        }

        private int safeColor(int value)
        {
            if (value < 256)
            {
                return value;
            } else
            {
                return 255;
            }
        }

    }
}
