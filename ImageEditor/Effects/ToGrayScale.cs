using System.Drawing;

namespace ImageEditor.Effects
{
    class ToGrayScale: BaseEffect
    {
        public ToGrayScale(Bitmap sourceImage): base(sourceImage)
        {
            name = "To grayscale";
        }

        protected override void ProcceedEffect()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color originalColor = lockedSourceImage.GetPixel(x, y);
                    short newColorValue = (short)((originalColor.R + originalColor.G + originalColor.B) / 3);
                    Color newColor = Color.FromArgb(red: newColorValue, blue: newColorValue, green: newColorValue);

                    lockedResultImage.SetPixel(x, y, newColor);

                }
            }
        }
    }
}
