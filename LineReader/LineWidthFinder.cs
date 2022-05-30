using System.Collections.Generic;

namespace LineReader
{
    class LineWidthFinder
    {
        private Line _line;

        public LineWidthFinder(Line line)
        {
            _line = line;
        }

        public void Find(ExtentedImage image)
        {
            List<int> averageMaxs = new List<int>();
            for (int i = 0; i < image.Image.Width; i += 2)
            {
                int max1 = 0;
                for (int j = 0; j < image.Image.Height; j+=2)
                {
                    if (image.Image[i,j].R == 0)
                        max1+=2;
                    else
                    {
                        if (max1 > 2)
                            averageMaxs.Add(max1);
                        max1 = 0;
                    }
                }
            }
            int sum = 0;
            foreach (var item in averageMaxs)
            {
                sum += item;
            }
            _line.Width = sum / averageMaxs.Count;
            _line.Width = (int)(_line.Width * 0.9);
        }
    }
}
