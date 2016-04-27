using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Effects.Binarization
{
    class Threadhold: BaseEffect
    {
        public short threadholdValue { get; private set; }
        public Threadhold(Bitmap sourceImage, short value): base(sourceImage)
        {
            threadholdValue = value;
        }

        protected override void ProcceedEffect()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = lockedSourceImage.GetPixel(x, y);
                    if (color.R < threadholdValue)
                    {
                        lockedResultImage.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        lockedResultImage.SetPixel(x, y, color);
                    }
                }
            }
        }


    }
}
