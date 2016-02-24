using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    abstract class Effect
    {
        public String name;
        private Bitmap prevImage;
        private Bitmap newImage;

        public Effect(Bitmap image)
        {
            this.prevImage = image;
        }
        abstract public Bitmap apply();
        abstract public Bitmap reverse();

    }
}
