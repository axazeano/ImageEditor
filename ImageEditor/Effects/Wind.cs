using System;
using System.Drawing;

namespace ImageEditor.Effects
{
    class Wind: BaseEffect
    {
        private Direction direction;
        private int size;
        private Random randomShift;
        public Wind(Bitmap sourceImage, Direction direction, int size): base(sourceImage)
        {
            name = "Wind";
            this.direction = direction;
            this.size = size;
            randomShift = new Random(size);
        }

        private void WindUp()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color cl = lockedSourceImage.GetPixel(x, y);
                    int shift = y - randomShift.Next(0, size);

                    for (int i = shift; (i <= y && i >= 0); i++)
                    {
                        lockedResultImage.SetPixel(x, i, cl);
                    }
                }
            }
        }

        private void WindDown()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = height - 1; y >= 0; y--)
                {
                    Color cl = lockedSourceImage.GetPixel(x, y);
                    int shift = y + randomShift.Next(0, size);

                    for (int i = y; (i <= shift && i < height); i++)
                    {
                        lockedResultImage.SetPixel(x, i, cl);
                    }
                }
            }
        }

        private void WindLeft()
        {
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    Color cl = lockedSourceImage.GetPixel(x, y);
                    int shift = x - randomShift.Next(0, size);

                    for (int i = shift; (i <= x && i >= 0); i++)
                    {
                        lockedResultImage.SetPixel(i, y, cl);
                    }
                }
            }
        }

        private void WindRight()
        {
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    Color cl = lockedSourceImage.GetPixel(x, y);
                    int shift = x + randomShift.Next(0, size);

                    for (int i = x; (i <= shift && i < width); i++)
                    {
                        lockedResultImage.SetPixel(i, y, cl);
                    }
                }
            }

        }
        protected override void ProcceedEffect()
        {
            switch (direction)
            {
                case Direction.Left:
                    WindLeft();
                    break;

                case Direction.Right:
                    WindRight();
                    break;

                case Direction.Down:
                    WindDown();
                    break;

                case Direction.Up:
                    WindUp();
                    break;
            }
        }
    }
}
