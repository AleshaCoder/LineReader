using System;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;
using System.IO;

namespace LineReader
{
    public class ExtentedImage
    {
        private SixLabors.ImageSharp.Image<Rgba32> _image;

        public SixLabors.ImageSharp.Image<Rgba32> Image => _image;

        public ExtentedImage(SixLabors.ImageSharp.Image<Rgba32> image)
        {
            _image = image;
        }

        /// <summary>
        /// Scale with nereast neighbor
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(float scale)
        {            
            int newWigth = (int)(Image.Width * scale);
            int newHeight = (int)(Image.Height * scale);

            SixLabors.ImageSharp.Image<Rgba32> newImage = new SixLabors.ImageSharp.Image<Rgba32>(newWigth, newHeight);

            for (int i = 0; i < newWigth; i++)
            {
                for (int j = 0; j < newHeight; j++)
                {
                    int x = (int)(i / scale);
                    int y = (int)(j / scale);
                    newImage[i, j] = _image[x, y];
                    if (newImage[i, j].R == 0)
                        newImage[i, j] = Rgba32.ParseHex("FF0000");
                }
            }
            _image = newImage;
        }

        public void SaveAsPNG(string path = "C:/Users/Алёша/Downloads/endtesttt2.png")
        {
            using (Stream newStrem = new FileStream(path, FileMode.OpenOrCreate))
                _image.Save(newStrem, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
        }
    }
}
