using SixLabors.ImageSharp.PixelFormats;

namespace LineReader
{
    public class LinePointsFinder
    {
        private Line _line;
        private ExtentedImage _pixelImage;

        public LinePointsFinder(Line line)
        {
            _line = line;
        }

        public void Find(ExtentedImage pixelImage)
        {
            _pixelImage = pixelImage;
            _line.SetCoords(_pixelImage.Image.Width, _pixelImage.Image.Height);
            FindAndConvertToPointsBlackPixels();
            _line.FindAllNearestJoints();
            DestroyUselessJoints();
        }

        private void FindAndConvertToPointsBlackPixels()
        {
            for (int i = 0; i < _pixelImage.Image.Width; i++)
            {
                for (int j = 0; j < _pixelImage.Image.Height; j++)
                {
                    if (_pixelImage.Image[i, j].B == 0)
                    {
                        if (IsInLine(i, j))
                            _line.AddPoint(new System.Drawing.Point(i, j));
                    }
                    else
                    {
                        _pixelImage.Image[i, j] = Rgba32.ParseHex("FFFFFFFF");
                    }
                }
            }
        }

        private void DestroyUselessJoints()
        {
            var width = _line.Width;
            do
            {
                _line.DestroyUselessJoints();
                width += 3;
            } while (width < 10);
        }

        private bool IsInLine(int x, int y)
        {
            int blackPoints = GetBlackPixelCountAround(x, y);

            if (blackPoints >= 2)
                return true;

            return false;
        }

        private int GetBlackPixelCountAround(int x, int y)
        {
            int blackPoints = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) // center point
                        continue;
                    try
                    {
                        if (_pixelImage.Image[x + i, y + j].B == 0)
                            blackPoints++;
                    }
                    catch 
                    {
                        blackPoints++;
                    }
                }
            }
            return blackPoints;
        }
    }
}
