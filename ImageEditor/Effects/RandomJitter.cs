using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Effects
{
    class RandomJitter: BaseEffect
    {
        protected int newX, newY;
        protected int degree;
        protected int half;
        Random random;

        public RandomJitter(Bitmap sourceImage, int degree, Selection selection) : base(sourceImage, selection)
        {
            random = new Random();
            half = (int)Math.Floor(degree / 2.0);
        }

        public RandomJitter(Bitmap sourceImage, int degree) : base(sourceImage)
        {
            random = new Random();
            half = (int)Math.Floor(degree / 2.0);
        }

        protected override void ProcceedEffect()
        {
            //for (int x = 0; x < width; x++)
            //{
            //    for (int y = 0; y < height; y++)
            //    {
            //        lockedResultImage.SetPixel(getNewX(x), getNewY(y), lockedSourceImage.GetPixel(x, y));
            //    }
            //}

            int newX, newY;

            short nHalf = (short)Math.Floor(degree / 2.0);
            Random rnd = new Random();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    newX = rnd.Next(degree) - nHalf;

                    if (x + newX < 0 || x + newX >= width)
                    {
                        newX = 0;
                    }
                    else
                    {
                        newX = newX + x;
                    }

                    newY = rnd.Next(degree) - nHalf;

                    if (y + newY < 0 || y + newY >= height)
                    {
                        newY = 0;
                    }
                    else
                    {
                        newY = newY + y;
                    }

                    Color cl = lockedSourceImage.GetPixel(x, y);
                    lockedResultImage.SetPixel(newX, newY, cl);
                }
            }
        }

        protected int getNewX(int x) {
            int randomValue = random.Next(degree) - half;
            if (x + randomValue < 0 || x + randomValue >= width)
            {
                return 0;
            }
            else
            {
                return randomValue + x;
            }
        }


        protected int getNewY(int y)
        {
            int randomValue = random.Next(degree) - half;
            if (y + randomValue < 0 || y + randomValue >= height)
            {
                return 0;
            }
            else
            {
                return randomValue + y;
            }

        }

    }
}
