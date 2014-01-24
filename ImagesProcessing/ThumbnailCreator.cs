using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImagesProcessing
{
    public static class ThumbnailCreator
    {
        public static void CreateThumbnail(string imagePath, string outputPath, int newPixelHeight)
        {
            using (Image originalImage = Image.FromFile(imagePath))
            {
                int newWidth = (originalImage.Width*newPixelHeight)/originalImage.Height;
                using (Bitmap blankCanvas = new Bitmap(newWidth, newPixelHeight))
                {
                    using (Graphics graphics = Graphics.FromImage(blankCanvas))
                    {
                        graphics.DrawImage(originalImage, 0, 0, blankCanvas.Width, blankCanvas.Height);
                        blankCanvas.Save(outputPath);
                    }
                }
            }
        }
    }
}
