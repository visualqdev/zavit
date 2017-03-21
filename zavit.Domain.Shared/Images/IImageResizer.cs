using System.IO;

namespace zavit.Domain.Shared.Images
{
    public interface IImageResizer
    {
        Stream ResizeImageToMinimum(Stream imageStream, int targetMinWidth, int targetMinHeight);
    }
}