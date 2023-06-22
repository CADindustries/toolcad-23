using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using toolcad23.Models.Classes;
using toolcad23.Models.Helpers;

namespace toolcad23.Models
{
    internal class DeliveryPageModel
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/QRCodes/";

        public DeliveryPageModel() 
        {
            
        }

        internal List<List<string>> GenerateRandomStrings(int maxGreen, int maxRed, int white, int blue, int yellow)
        {
            List<Vector2Int> allowedGreenList = RandomHelper.GenerateAllowedList(maxGreen);
            List<Vector2Int> allowedRedList = RandomHelper.GenerateAllowedList(maxRed);

            List<string> greenStandElements = RandomHelper.GenerateRandomElements(white, blue);
            List<string> redStandElements = Enumerable.Repeat(CubeTypeEnum.Yellow, yellow).ToList();

            Dictionary<Vector2Int, string> greenStandCubes = new Dictionary<Vector2Int, string>();
            Dictionary<Vector2Int, string> redStandCubes = new Dictionary<Vector2Int, string>();

            while (greenStandElements.Count > 0)
            {
                greenStandCubes.Add(allowedGreenList.Pop(0), greenStandElements.Pop(0));
            }

            while (redStandElements.Count > 0)
            {
                redStandCubes.Add(allowedRedList.Pop(0), redStandElements.Pop(0));
            }

            RandomHelper.LowDownCubes(greenStandCubes);
            RandomHelper.LowDownCubes(redStandCubes);

            return GenerateStringLists(greenStandCubes, redStandCubes);
        }

        private List<List<string>> GenerateStringLists(Dictionary<Vector2Int, string> green, Dictionary<Vector2Int, string> red)
        {
            List<List<string>> strings = new List<List<string>>()
            {
                new List<string>(),
                new List<string>(),
                new List<string>(),
                new List<string>(),
            };
            for (int i = 0; i < 4; i++)
            {
                foreach (string s in green.Where(x => x.Key.X == i).Select(x => x.Value))
                {
                    strings[i].Add(s);
                }
                foreach (string s in red.Where(x => x.Key.X == i).Select(x => x.Value))
                {
                    strings[i].RandomlyAdd(s);
                }
            }
            return strings;
        }

        internal BitmapImage GetImage(List<string> cubes)
        {
            if (cubes.Count > 0)
            {
                string joined = string.Join("_", cubes.ToArray());
                try
                {
                    return new BitmapImage(new Uri(imagePath + joined + ".png"));
                }
                catch (IOException)
                {
                    // usually this is because of many yellow cubes, that do not have qr_codes
                    return new BitmapImage(new Uri(imagePath + "no_qr.jpg"));
                }
            }
            return new BitmapImage(new Uri(imagePath + "empty_qr.png"));
        }

        internal string GetText(List<string> cubes)
        {
            if (cubes.Count > 0)
            {
                string joined = string.Join(", ", cubes.ToArray());
                return joined;
            }
            return "empty";
        }
    }
}
