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
        private int _startX;
        private int _originalStartX;
        public int startX
        {
            get { return _startX; }
            private set
            {
                _startX = (int)(value * scaleX);
                _originalStartX = value;
            }
        }
        private int _startY;
        private int _originalStartY;
        public int startY
        {
            get { return _startY; }
            private set
            {
                _startY = (int)(value * scaleY);
                _originalStartY = value;
            }
        }

        private int _originalEndX;
        private int _endX;
        public int endX
        {
            get { return _endX; }
            private set
            {
                _endX = (int)(value * scaleX);
                _originalEndX = value;
            }
        }

        private int _originalEndY;
        private int _endY;
        public int endY
        {
            get { return _endY; }
            private set {
                endY = (int)(value * scaleY);
                _originalEndY = value;
            }
        }
        public int originalWidth { get; private set; }
        public int originalHeight { get; private set; }
        public int viewWidth { get; private set; }
        public int viewHeight { get; private set; }
        private float scaleX;
        private float scaleY;

        public Selection(int startX, int startY)
        {
            this.startX = startX;
            this.startY = startY;
        }

        public Selection(int originalWidth, int originalHeight, int viewWidth, int viewHeight)
        {
            this.originalWidth = originalWidth;
            this.originalHeight = originalHeight;
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            scaleX = originalWidth / viewWidth;
            scaleY = originalHeight / viewHeight;
        }

        public void startSelection(int startX, int startY)
        {
            this.startX = endX;
            this.startY = endY;
        }

        public void update(int endX, int endY)
        {
            this.endX = endX;
            this.endY = endY;
        }

        public void clean()
        {
            startX = 0;
            startY = 0;
            endX = 0;
            endY = 0;
        }

        public Bitmap drawSelection(Bitmap image)
        {
            Bitmap result = image;
            return result;
        }
    }
}
