using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LineReader
{
    public class Line
    {
        private int _width;

        private List<Joint> _points;

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
            _points = new List<Joint>();
        }

        public void SetCoords(int x, int y)
        {
            _maxXCoord = x;
            _maxYCoord = y;
        }

        public void AddPoint(Point point)
        {
            Joint joint = new Joint(point);

            if (_points.Count == 0)
                joint.SetFirst();

            if (_points.Contains(joint) == false)
                _points.Add(joint);
        }

        public void FindAllNearestJoints()
        {
            foreach (var joint in _points)
                joint.SetNearestJoints(_points);
        }

        public void DestroyUselessJoints()
        {
            for (int i = 0; i < _points.Count; i++)
            {
                if (HasNearestPoints(_points[0]) == false)
                {
                    _points.Remove(_points[i]);
                    i--;
                    continue;
                }

                foreach (var nearest in _points[i].NearestJoints)
                {
                    if (nearest.First)
                        continue;

                    if (_points[i].ConsistAllJoints(nearest))
                    {
                        _points[i].DestroyNearestJoints(nearest);
                        _points.Remove(nearest);
                        i--;
                        break;
                    }
                }
            }
        }

        private bool HasNearestPoints(Joint joint)
        {
            return joint.NearestJoints.Count == 0;
        }

        public void DrawOnImage(ExtentedImage image)
        {
            int scale = image.Image.Width / _maxXCoord;
            foreach (var point in _points)
                DrawPoint(image, new Point(point.Point.X * scale, point.Point.Y * scale), 1);
        }

        private void DrawPoint(ExtentedImage image, Point point, int width)
        {
            image.Image[point.X, point.Y] = Rgba32.ParseHex("0000FFFF");
            for (int i = -width/2; i < width / 2 + (width % 2); i++)
            {
                for (int j = -width / 2; j < width / 2 + (width % 2); j++)
                {
                    if (i == 0 && j == 0) // center point
                        continue;
                    try
                    {
                        image.Image[point.X + i, point.Y + j] = Rgba32.ParseHex("0000FFFF");
                    }
                    catch { }
                }
            }
        }
    }
}
