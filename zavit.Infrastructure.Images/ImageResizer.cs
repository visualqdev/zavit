using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using zavit.Domain.Shared.Images;

namespace zavit.Infrastructure.Images
{
    public class ImageResizer : IImageResizer
    {
        public Stream ResizeImageToMinimum(Stream imageStream, int targetMinWidth, int targetMinHeight)
        {
            using (var image = Image.FromStream(imageStream))
            {
                var ratioX = (double)targetMinWidth / image.Width;
                var ratioY = (double)targetMinHeight / image.Height;
                var ratio = Math.Max(ratioX, ratioY);

                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                using (var stream = new MemoryStream())
                {
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                    newImage.Save(stream, ImageFormat.Png);

                    var byteArray = stream.ToArray();
                    return new MemoryStream(byteArray);
                }
            }
        }
    }
}