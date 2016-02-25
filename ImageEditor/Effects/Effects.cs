using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    class Effect
    {
        static public Bitmap rotate(Bitmap image, int angle)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            // center of image
            int y0 = image.Height / 2;
            int x0 = image.Width / 2;

            double degree = Math.PI * angle / 180.0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int newX = Convert.ToInt32(Math.Cos(degree) * (x - x0) - Math.Sin(degree) * (y - y0) + x0);
                    if (newX > result.Width - 1 || newX < 0)
                    {
                        continue;
                    }
                    int newY = Convert.ToInt32(Math.Sin(degree) * (x - x0) + Math.Cos(degree) * (y - y0) + y0);
                    if (newY > result.Height - 1 || newY < 0)
                    {
                        continue;
                    }
                    Color color = image.GetPixel(x, y);
                    result.SetPixel(newX, newY, color);
                }
            }
            return result;
        }
    }
}
