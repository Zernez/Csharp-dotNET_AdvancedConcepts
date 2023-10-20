using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;
using static System.Console;



string imagesFolder = Path.Combine(Environment.CurrentDirectory, "Images");
IEnumerable<string> images = Directory.EnumerateFiles(imagesFolder);
foreach (string imagePath in images)
{
    string thumbnailPath = Path.Combine(Environment.CurrentDirectory, "Images",
        Path.GetFileNameWithoutExtension(imagePath) + "-thumbnail" +
        Path.GetExtension(imagePath));

    using (Image image = Image.Load(imagePath))
    {
        image.Mutate(x => x.Resize(image.Width / 10, image.Height / 10));
        image.Mutate(x => x.Grayscale()); image.Save(thumbnailPath);
    }
}
WriteLine("Image processing complete. View the images folder.");

