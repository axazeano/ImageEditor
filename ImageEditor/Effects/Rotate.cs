using System;
using System.Drawing;

namespace ImageEditor.Effects
{
    class Rotate: BaseEffect
    {
        public int centerY { get;}
        public int centerX { get;}
        protected double degrees;

    
        public Rotate(Bitmap sourceImage, int angle) : base(sourceImage)
        {
            name = "Rotate";
            centerX = width / 2;
            centerY = height / 2;
            degrees = Math.PI * angle / 180.0;

        }

        protected override void ProcceedEffect()
        {
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int newX = Convert.ToInt32(Math.Cos(degrees) * (x - centerX) - Math.Sin(degrees) * (y - centerY) + centerX);
                    if (newX >= width || newX < 0) { continue; }
                    int newY = Convert.ToInt32(Math.Sin(degrees) * (x - centerX) + Math.Cos(degrees) * (y - centerY) + centerY);
                    if (newY >= height || newY < 0) { continue; }

                    lockedResultImage.SetPixel(newX, newY, lockedSourceImage.GetPixel(x, y));

                }
            }
        }
    }
}