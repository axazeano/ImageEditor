using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    class Effect
    {
        static public Bitmap rotate(Bitmap image, int angle)
        {
            LockBitmap lockImage = new LockBitmap(image);
            Bitmap result = new Bitmap(width: image.Width, height: image.Height);
            LockBitmap lockResult = new LockBitmap(result);

            lockImage.LockBits();
            lockResult.LockBits();

            // center of image
            int y0 = image.Height / 2;
            int x0 = image.Width / 2;

            double degree = Math.PI * angle / 180.0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int newX = Convert.ToInt32(Math.Cos(degree) * (x - x0) - Math.Sin(degree) * (y - y0) + x0);
                    if (newX >= image.Width || newX < 0)
                    {
                        continue;
                    }
                    int newY = Convert.ToInt32(Math.Sin(degree) * (x - x0) + Math.Cos(degree) * (y - y0) + y0);
                    if (newY >= image.Height || newY < 0)
                    {
                        continue;
                    }
                    Color color = lockImage.GetPixel(x, y);
                    lockResult.SetPixel(newX, newY, color);
                }

            }
            lockImage.UnlockBits();
            lockResult.UnlockBits();
            return result;
        }

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        static public void wind(Bitmap image, int value, Direction direction)
        {
            Random randShift = new Random(value);
            LockBitmap lockBitmap = new LockBitmap(image);
            lockBitmap.LockBits();

            switch (direction)
            {
                case Direction.Left:

                    for (int y = lockBitmap.Height - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < lockBitmap.Width; x++)
                        {
                            Color cl = lockBitmap.GetPixel(x, y);
                            int shift = x - randShift.Next(0, value);

                            for (int i = shift; (i <= x && i >= 0); i++)
                            {
                                lockBitmap.SetPixel(i, y, cl);
                            }
                        }
                    }
                    break;

                case Direction.Right:
                    for (int y = lockBitmap.Height - 1; y >= 0; y--)
                    {
                        for (int x = lockBitmap.Width - 1; x >= 0; x--)
                        {
                            Color cl = lockBitmap.GetPixel(x, y);
                            int shift = x + randShift.Next(0, value);

                            for (int i = x; (i <= shift && i < lockBitmap.Width); i++)
                            {
                                lockBitmap.SetPixel(i, y, cl);
                            }
                        }
                    }
                    break;

                case Direction.Down:
                case Direction.Up:
                default:
                    break;
            }

            for (int y = lockBitmap.Height - 1; y >= 0; y--)
            {
                for (int x = lockBitmap.Width - 1; x >= 0; x--)
                {
                    Color cl = lockBitmap.GetPixel(x, y);
                    int shift = x + randShift.Next(0, value);

                    for (int i = x; (i <= shift && i < lockBitmap.Width); i++)
                    {
                        lockBitmap.SetPixel(i, y, cl);
                    }
                }
            }
            lockBitmap.UnlockBits();
        }

        static public Bitmap bilinearInterpolation(Bitmap image)
        {

            Bitmap bTemp = (Bitmap)image.Clone();
            Bitmap result = new Bitmap(image.Width, image.Height);

            double nXFactor = 0.8;
            double nYFactor = 0.8;

            double fraction_x, fraction_y, one_minus_x, one_minus_y;
            int ceil_x, ceil_y, floor_x, floor_y;
            Color c1 = new Color();
            Color c2 = new Color();
            Color c3 = new Color();
            Color c4 = new Color();
            byte red, green, blue;

            byte b1, b2;

            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    // Setup

                    floor_x = (int)Math.Floor(x * nXFactor);
                    floor_y = (int)Math.Floor(y * nYFactor);
                    ceil_x = floor_x + 1;
                    if (ceil_x >= bTemp.Width) ceil_x = floor_x;
                    ceil_y = floor_y + 1;
                    if (ceil_y >= bTemp.Height) ceil_y = floor_y;
                    fraction_x = x * nXFactor - floor_x;
                    fraction_y = y * nYFactor - floor_y;
                    one_minus_x = 1.0 - fraction_x;
                    one_minus_y = 1.0 - fraction_y;

                    c1 = bTemp.GetPixel(floor_x, floor_y);
                    c2 = bTemp.GetPixel(ceil_x, floor_y);
                    c3 = bTemp.GetPixel(floor_x, ceil_y);
                    c4 = bTemp.GetPixel(ceil_x, ceil_y);

                    // Blue
                    b1 = (byte)(one_minus_x * c1.B + fraction_x * c2.B);

                    b2 = (byte)(one_minus_x * c3.B + fraction_x * c4.B);

                    blue = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                    // Green
                    b1 = (byte)(one_minus_x * c1.G + fraction_x * c2.G);

                    b2 = (byte)(one_minus_x * c3.G + fraction_x * c4.G);

                    green = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                    // Red
                    b1 = (byte)(one_minus_x * c1.R + fraction_x * c2.R);

                    b2 = (byte)(one_minus_x * c3.R + fraction_x * c4.R);

                    red = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                    result.SetPixel(x, y, System.Drawing.Color.FromArgb(255, red, green, blue));
                }
            }
            return result;
        }
    }
}
