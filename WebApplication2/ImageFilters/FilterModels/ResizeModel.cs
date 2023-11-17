using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using WebApplication2.ImageFilters.Interfaces;

namespace WebApplication2.ImageFilters.FilterModels
{
    public class ResizeModel : IFilter
    {
        private readonly Size _size;

        public ResizeModel(Size size) 
        {
            _size = size;
        }

        private Image ResizeImage(Image image)
        {
            
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)_size.Width / (float)sourceWidth);
            nPercentH = ((float)_size.Height / (float)sourceHeight);
            nPercent = Math.Min(nPercentW, nPercentH);

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap bitmap = new Bitmap(destWidth, destHeight);
            Graphics graphic = Graphics.FromImage((System.Drawing.Image)bitmap);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graphic.DrawImage(image, 0, 0, destWidth, destHeight);
            graphic.Dispose();
            return (Image)bitmap;
        }

        public Image Apply(Image img)
        {
            return ResizeImage(img);
        }

    }
}
