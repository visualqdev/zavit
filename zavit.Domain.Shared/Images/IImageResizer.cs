using System.IO;

namespace zavit.Domain.Shared.Images
{
    public interface IImageResizer
    {
        byte[] ResizeImageToMinimum(Stream imageStream, int targetMinWidth, int targetMinHeight);
    }
}