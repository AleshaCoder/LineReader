using System.Collections.Generic;

namespace LineReader
{
    class LineWidthFinder
    {
        private const float _widthCoef = 0.9f;
        private Line _line;

        public LineWidthFinder(Line line)
        {
            _line = line;
        }

        public void Find(ExtentedImage image)
        {
            List<int> averageMaxs = GetAveragesMaxsOfBlackPixels(image);
            _line.Width = GetAverage(averageMaxs);
            _line.Width = (int)(_line.Width * _widthCoef);
        }

        private List<int> GetAveragesMaxsOfBlackPixels(ExtentedImage image)
        {
            List<int> averageMaxs = new List<int>();
            for (int i = 0; i < image.Image.Width; i += 2)
            {
                int max1 = 0;
                for (int j = 0; j < image.Image.Height; j += 2)
                {
                    if (image.Image[i, j].R == 0)
                        max1 += 2;
                    else
                    {
                        if (max1 > 2)
                            averageMaxs.Add(max1);
                        max1 = 0;
                    }
                }
            }

            return averageMaxs;
        }

        private int GetAverage(List<int> numbers)
        {
            int sum = 0;
            foreach (var item in numbers)
                sum += item;

            return sum / numbers.Count;
        }
    }
}
