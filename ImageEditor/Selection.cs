using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    class Selection
    {
        private static Selection _instance;

        private readonly Selection selection;
        /// <summary>
        /// Singleton of history
        /// </summary>
        public static Selection Instance => _instance ?? (_instance = new Selection());

        public int x, y, width, height;

        public void updateSelection(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }


    }
}
