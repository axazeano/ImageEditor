using System.Drawing;

namespace ImageEditor.Effects
{
    class BaseEffect
    {
        public string name;
        public Bitmap sourceImage { get; protected set; }
        public Bitmap resultImage { get; protected set; }
        protected int startY;
        protected int startX;
        protected int height;
        protected int width;

        public delegate void Complete(Bitmap image);

        protected LockBitmap lockedSourceImage;
        protected LockBitmap lockedResultImage;

        public BaseEffect(Bitmap sourceImage, Selection selection)
        {
            this.sourceImage = sourceImage;

            startX = selection.x;
            startY = selection.y;
            height = selection.height;
            width = selection.width;

            resultImage = new Bitmap(height: height, width: width);
            lockedSourceImage = new LockBitmap(sourceImage);
            lockedResultImage = new LockBitmap(resultImage);

            lockedSourceImage.LockBits();
            lockedResultImage.LockBits();
        }

        public BaseEffect(Bitmap sourceImage)
        {
            this.sourceImage = sourceImage;

            startX = 0;
            startY = 0;
            height = this.sourceImage.Height;
            width = this.sourceImage.Width;

            resultImage = new Bitmap(height: height, width: width);
            lockedSourceImage = new LockBitmap(sourceImage);
            lockedResultImage = new LockBitmap(resultImage);

            lockedSourceImage.LockBits();
            lockedResultImage.LockBits();
        }

        protected virtual void ProcceedEffect()
        {
            // Effect's code 
        }

        public void ApplyEffect(Complete callback)
        {
            ProcceedEffect();
            FreeBits();
            callback(resultImage);
        }

        private void FreeBits()
        {
            lockedSourceImage.UnlockBits();
            lockedResultImage.UnlockBits();
        }
    }
}
