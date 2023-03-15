using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using toolcad23.Models.Classes;
using toolcad23.Models.Helpers;

namespace toolcad23.Models
{
    internal class RetrievePageModel
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/";

        internal static Dictionary<Vector2Int, string> GenerateRandomGreenDictionary(int maxGreen, int white, int blue)
        {
            List<Vector2Int> allowedGreenList = RandomHelper.GenerateAllowedList(maxGreen);

            List<string> greenStandElements = RandomHelper.GenerateRandomElements(white, blue);

            Dictionary<Vector2Int, string> greenStandCubes = new Dictionary<Vector2Int, string>();

            while (greenStandElements.Count > 0)
            {
                greenStandCubes.Add(allowedGreenList.Pop(0), greenStandElements.Pop(0));
            }

            RandomHelper.LowDownCubes(greenStandCubes);

            return greenStandCubes;
        }

        internal static Dictionary<Vector2Int, string> GenerateRandomRedDictionary(int maxRed, int yellow)
        {
            List<Vector2Int> allowedRedList = RandomHelper.GenerateAllowedList(maxRed);

            List<string> redStandElements = Enumerable.Repeat(CubeTypeEnum.Yellow, yellow).ToList();

            Dictionary<Vector2Int, string> redStandCubes = new Dictionary<Vector2Int, string>();

            while (redStandElements.Count > 0)
            {
                redStandCubes.Add(allowedRedList.Pop(0), redStandElements.Pop(0));
            }

            RandomHelper.LowDownCubes(redStandCubes);

            return redStandCubes;
        }

        internal static BitmapImage GetImage(string cube)
        {
            return new BitmapImage(new Uri(imagePath + cube + "_cube.png"));
        }
    }
}
