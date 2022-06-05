using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Diagnostics;

namespace LineReader
{
    class Program
    {
        private static LineWidthFinder _lineWidthFinder;
        private static LinePointsFinder _linePointsFinder;
        private static Line _line;

        private static Stopwatch _stopwatch;
        private static long lastTime;

        private static void StartTime()
        {
            _stopwatch = new Stopwatch();
            lastTime = 0;
            _stopwatch.Start();
        }

        private static void ShowTime(string str = "")
        {
            Console.WriteLine($"{str} {_stopwatch.ElapsedMilliseconds - lastTime}ms");
            lastTime = _stopwatch.ElapsedMilliseconds;
        }

        private static void ShowTimeFromStart(string str = "")
        {
            Console.WriteLine($"{str} {_stopwatch.ElapsedMilliseconds/2}ms");
        }

        static void Main(string[] args)
        {
            for (int i = 1; i < 8; i++)
            {
                _line = new Line();
                StartTime();
                var imageFromFile = SixLabors.ImageSharp.Image.Load<Rgba32>($"C:/Users/Алёша/Downloads/test{i}.png");
                var imageFromFile2 = imageFromFile;
                Console.WriteLine();
                ShowTime($"test{i}.png loaded for");                
                ExtentedImage exImage = new ExtentedImage(imageFromFile);
                ExtentedImage exImage2 = new ExtentedImage(imageFromFile2);
                _lineWidthFinder = new LineWidthFinder(_line);
                _linePointsFinder = new LinePointsFinder(_line);
                _lineWidthFinder.Find(exImage);
                ShowTime("Find line width with SixLabors for");
                exImage.Scale(1.0f / (_line.Width - 2));
                ShowTime("Scaled to 0.1 with SixLabors for");
                _linePointsFinder.Find(exImage);
                ShowTime("Points found with SixLabors for");
                _line.DrawOnImage(exImage2);
                ShowTime("Line drawn for");
                _line.DrawOnImage(exImage);
                exImage.SaveAsPNG($"C:/Users/Алёша/Downloads/pixelImage{i}.png");
                ShowTime("Saved to png for");
                exImage2.SaveAsPNG($"C:/Users/Алёша/Downloads/imageWithLine{i}.png");
                ShowTime("Saved to png for");
                ShowTimeFromStart("Result for");
            }
        }
    }
}
