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

        abstract public Bitmap apply(Bitmap image);
        abstract public Bitmap reverse();

    }
}
