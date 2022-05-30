using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LineReader
{
    public class Line
    {
        private int _width;

        private List<Point> _points;

        private int _maxXCoord, _maxYCoord;

        public int Width
        {
            get => _width;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Width should be more than 0");
                _width = value;
            }
        }

        public Line()
        {
            _width = 0;
            _points = new List<Point>();
        }

        public void SetCoords(int x, int y)
        {
            _maxXCoord = x;
            _maxYCoord = y;
        }

        public void AddPoint(Point point)
        {
            if (_points.Contains(point) == false)
            {
                _points.Add(point);
            }
        }

        public void DrawOnImage(ExtentedImage image)
        {
            int scale = image.Image.Width / _maxXCoord;
            foreach (var point in _points)
            {
                DrawPoint(image, new Point(point.X * scale, point.Y * scale), 3);
            }
        }

        private void DrawPoint(ExtentedImage image, Point point, int width)
        {
            image.Image[point.X, point.Y] = Rgba32.ParseHex("FF0000FF");
            for (int i = -width/2; i < width / 2 + (width % 2); i++)
            {
                for (int j = -width / 2; j < width / 2 + (width % 2); j++)
                {
                    if (i == 0 && j == 0) // center point
                        continue;
                    try
                    {
                        image.Image[point.X + i, point.Y + j] = Rgba32.ParseHex("FF0000FF");
                    }
                    catch { }
                }
            }
        }
    }
}
