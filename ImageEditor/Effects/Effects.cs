using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    public struct FloatPoint
    {
        public double X;
        public double Y;
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public static class DirectionExtention
    {
        public static Orientation orientation(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Orientation.Horizontal;
                case Direction.Right:
                    return Orientation.Horizontal;
                case Direction.Up:
                    return Orientation.Vertical;
                case Direction.Down:
                    return Orientation.Vertical;
                default:
                    return Orientation.Horizontal;
            }
        }
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }


    class Effect
    {

        public static Bitmap OffsetFilterAbs(Bitmap image, int xOffset, int yOffset)
        {
            LockBitmap lockImage = new LockBitmap(image);
            lockImage.LockBits();

            Bitmap result = new Bitmap(width: image.Width, height: image.Height);
            LockBitmap lockResult = new LockBitmap(result);
            lockResult.LockBits();

            for (int y = 0; y < lockImage.Height; y++)
            {
                for (int x = 0; y < lockImage.Width; x++)
                {
                    int newY = y + yOffset;
                    int newX = x + xOffset;

                    if (newY < lockImage.Height && newX < lockImage.Width)
                    {
                        Color cl = lockImage.GetPixel(x, y);
                        lockResult.SetPixel(newX, newY, cl);
                    }
                }
            }

            lockImage.UnlockBits();
            lockResult.UnlockBits();
            return result;
        }

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

        static private int strengthToPixels(int strength, Bitmap image, Orientation orientation)
        {
            int percent = 0;
            switch (orientation)
            {
                case Orientation.Horizontal:
                    percent = image.Width / 100;
                    break;
                case Orientation.Vertical:
                    percent = image.Height / 100;
                    break;
            }
            return percent * strength;
        }

        static public Bitmap wind(Bitmap image, int strength, Direction direction)
        {
            int pixelStrenght = strengthToPixels(strength, image, direction.orientation());
            Random randShift = new Random(pixelStrenght);
            Bitmap result = new Bitmap(width: image.Width, height: image.Height);
            LockBitmap lockResult = new LockBitmap(result);
            LockBitmap lockBitmap = new LockBitmap(image);
            lockBitmap.LockBits();
            lockResult.LockBits();

            switch (direction)
            {
                case Direction.Left:

                    for (int y = lockBitmap.Height - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < lockBitmap.Width; x++)
                        {
                            Color cl = lockBitmap.GetPixel(x, y);
                            int shift = x - randShift.Next(0, strength);

                            for (int i = shift; (i <= x && i >= 0); i++)
                            {
                                lockResult.SetPixel(i, y, cl);
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
                            int shift = x + randShift.Next(0, strength);

                            for (int i = x; (i <= shift && i < lockBitmap.Width); i++)
                            {
                                lockResult.SetPixel(i, y, cl);
                            }
                        }
                    }
                    break;

                case Direction.Down:
                    for (int x = 0; x < lockBitmap.Width; x++)
                    {
                        for (int y = lockBitmap.Height - 1; y >= 0; y--)
                        {
                            Color cl = lockBitmap.GetPixel(x, y);
                            int shift = y + randShift.Next(0, strength);

                            for (int i = y; (i <= shift && i < lockBitmap.Height); i++)
                            {
                                lockResult.SetPixel(x, i, cl);
                            }
                        }
                    }
                    break;

                case Direction.Up:
                    for (int x = 0; x < lockBitmap.Width; x++)
                    {
                        for (int y = 0; y < lockBitmap.Height; y++)
                        {
                            Color cl = lockBitmap.GetPixel(x, y);
                            int shift = y - randShift.Next(0, strength);

                            for (int i = shift; (i <= y && i >= 0); i++)
                            {
                                lockResult.SetPixel(x, i, cl);
                            }
                        }
                    }
                    break;
            }
            lockBitmap.UnlockBits();
            lockResult.UnlockBits();
            return result;
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

        public Bitmap waves(Bitmap image)
        {
            return null;
        }

        //public static bool Swirl(Bitmap b, double fDegree, bool bSmoothing /* default fDegree to .05 */)
        //{
        //    int nWidth = b.Width;
        //    int nHeight = b.Height;

        //    FloatPoint[,] fp = new FloatPoint[nWidth, nHeight];
        //    Point[,] pt = new Point[nWidth, nHeight];

        //    Point mid = new Point();
        //    mid.X = nWidth / 2;
        //    mid.Y = nHeight / 2;

        //    double theta, radius;
        //    double newX, newY;

        //    for (int x = 0; x < nWidth; ++x)
        //        for (int y = 0; y < nHeight; ++y)
        //        {
        //            int trueX = x - mid.X;
        //            int trueY = y - mid.Y;
        //            theta = Math.Atan2((trueY), (trueX));

        //            radius = Math.Sqrt(trueX * trueX + trueY * trueY);

        //            newX = mid.X + (radius * Math.Cos(theta + fDegree * radius));
        //            if (newX > 0 && newX < nWidth)
        //            {
        //                fp[x, y].X = newX;
        //                pt[x, y].X = (int)newX;
        //            }
        //            else
        //                fp[x, y].X = pt[x, y].X = x;

        //            newY = mid.Y + (radius * Math.Sin(theta + fDegree * radius));
        //            if (newY > 0 && newY < nHeight)
        //            {
        //                fp[x, y].Y = newY;
        //                pt[x, y].Y = (int)newY;
        //            }
        //            else
        //                fp[x, y].Y = pt[x, y].Y = y;
        //        }
        //}

        //if (bSmoothing)
        //    OffsetFilterAntiAlias(b, fp);
        //else
        //    OffsetFilterAbs(b, pt);


        public static Bitmap RandomJitter(Bitmap image, short nDegree)
        {
            LockBitmap lockImage = new LockBitmap(image);
            lockImage.LockBits();

            Bitmap result = (Bitmap)image.Clone();
            LockBitmap lockResult = new LockBitmap(result);
            lockResult.LockBits();

            int nWidth = lockImage.Width;
            int nHeight = lockImage.Height;

            int newX, newY;

            short nHalf = (short)Math.Floor(nDegree / 2.0);
            Random rnd = new Random();

            for (int x = 0; x < nWidth; x++)
            {
                for (int y = 0; y < nHeight; y++)
                {
                    newX = rnd.Next(nDegree) - nHalf;

                    if (x + newX < 0 || x + newX >= nWidth)
                    {
                        newX = 0;
                    }
                    else
                    {
                        newX = newX + x;
                    }

                    newY = rnd.Next(nDegree) - nHalf;

                    if (y + newY < 0 || y + newY >= nHeight)
                    {
                        newY = 0;
                    }
                    else
                    {
                        newY = newY + y;
                    }

                    Color cl = lockImage.GetPixel(x, y);
                    lockResult.SetPixel(newX, newY, cl);
                }
            }

            lockImage.UnlockBits();
            lockResult.UnlockBits();
            return result;
        }


        public static Bitmap GrayScale(Bitmap image)
        {
            LockBitmap lockImage = new LockBitmap(image);
            lockImage.LockBits();

            Bitmap result = (Bitmap)image.Clone();
            LockBitmap lockResult = new LockBitmap(result);
            lockResult.LockBits();

            int nWidth = lockImage.Width;
            int nHeight = lockImage.Height;

            for (int y=0; y < nHeight; y++)
            {
                for (int x=0; x < nWidth; x++)
                {
                    Color originalColor = lockImage.GetPixel(x, y);
                    int red = originalColor.R;
                    int green = originalColor.G;
                    int blue = originalColor.B;
                    int avg = (red + green + blue) / 3;
                    Color newColor = Color.FromArgb(red: avg, blue: avg, green: avg);
                    lockResult.SetPixel(x, y, newColor);
                    
                }
            }
            

            lockImage.UnlockBits();
            lockResult.UnlockBits();
            return result;
        }

        public static Bitmap GrayWorld(Bitmap image)
        {
            LockBitmap lockImage = new LockBitmap(image);
            lockImage.LockBits();

            Bitmap result = (Bitmap)image.Clone();
            LockBitmap lockResult = new LockBitmap(result);
            lockResult.LockBits();

            int nWidth = lockImage.Width;
            int nHeight = lockImage.Height;

            int redSum = 0, greenSum = 0, blueSum = 0;
            

            for (int y = 0; y < nHeight; y++)
            {
                for (int x = 0; x < nWidth; x++)
                {
                    Color color = lockImage.GetPixel(x, y);
                    redSum += color.R;
                    greenSum += color.G;
                    blueSum += color.B;
                }
            }

            int size = nWidth * nHeight;

            double redGlobal = 1f / size * redSum;
            double greenGlobal = 1f / size * greenSum;
            double blueGlobal = 1f / size * blueSum;
            double avg = (redGlobal + greenGlobal + blueGlobal) / 3;

            double redDiv = avg / redGlobal;
            double greenDiv = avg / greenGlobal;
            double blueDiv = avg / blueGlobal;

            for (int y = 0; y < nHeight; y++)
            {
                for (int x = 0; x < nWidth; x++)
                {
                    Color color = lockImage.GetPixel(x, y);
                    int newRed = (int)(color.R * redDiv);
                    int newGreen = (int)(color.G * greenDiv);
                    int newBlue = (int)(color.B * blueDiv);
                    lockResult.SetPixel(x, y, Color.FromArgb((newRed < 255) ? newRed: 254, (newGreen < 255) ? newGreen : 254, (newBlue < 255) ? newBlue : 254));
                }
            }
            lockImage.UnlockBits();
            lockResult.UnlockBits();
            return result;
        }

        /// <summary> Обрабатывает заданный растр. </summary>
        //public static Bitmap mainColor(Bitmap image, bool half)
        //{
        //    выберем опорным цветом серый и максимальные значеения
        //    Vector MainColor = Vector.White / 2;
        //    float Rmax = 0f;
        //    float Gmax = 0f;
        //    float Bmax = 0f;

        //    подсчитываем максимумы
        //        {
        //        for (int j = 0; j < image.Height; j++)
        //        {
        //            for (int i = 0; i < image.Height; i++)
        //            {
        //                if (source[i, j].R > Rmax) Rmax = source[i, j].R;
        //                if (source[i, j].G > Gmax) Gmax = source[i, j].G;
        //                if (source[i, j].B > Bmax) Bmax = source[i, j].B;
        //            }
        //        }

        //    }

        //    создаем результирующий растр
        //   Raster result = new Raster(source.Width, source.Height);

        //    определяем начальный столбец пикселей для обработки
        //        int start = half ? source.Width / 2 : 0;

        //    сохраняем время начала обработки
        //        DateTime startTime = DateTime.Now;

        //    обрабатываем исходный растр
        //        for (int j = 0; j < source.Height; j++)
        //    {
        //        первая половина растра не требует обработки
        //            for (int i = 0; i < start; i++)
        //            result[i, j] = source[i, j];

        //        для второй половины растра применяем фильтр
        //            for (int i = start; i < source.Width; i++)
        //        {
        //            result[i, j].R = source[i, j].R * MainColor.R / Rmax;
        //            result[i, j].G = source[i, j].G * MainColor.G / Gmax;
        //            result[i, j].B = source[i, j].B * MainColor.B / Bmax;
        //        }
        //        сообщаем текущий прогресс и время обработки
        //        }

        //    return result;
        //}
    }
    }
//}
