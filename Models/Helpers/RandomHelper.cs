using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toolcad23.Models.Classes;

namespace toolcad23.Models.Helpers
{
    internal class RandomHelper
    {
        public static void LowDownCubes(Dictionary<Vector2Int, string> standCubes)
        {
            for (int i = 0; i < 4; i++)
            {
                var currentRoomElements = standCubes.Select(x => x.Key).Where(x => x.X == i).ToList();
                if (currentRoomElements.Count > 0)
                {
                    // cringe logic to low down the cubes in List
                    int counter = 0;
                    while (counter < currentRoomElements.Count)
                    {
                        var vect = currentRoomElements.FirstOrDefault(x => x.Y == counter);
                        if (vect == null)
                        {
                            foreach (var currentRoomElement in currentRoomElements.Where(x => x.Y > counter))
                            {
                                currentRoomElement.Y -= 1;
                            }
                        }
                        vect = currentRoomElements.FirstOrDefault(x => x.Y == counter);
                        if (vect != null)
                        {
                            counter++;
                        }
                    }
                }
            }
        }

        public static List<string> GenerateRandomElements(int white, int blue)
        {
            List<string> elements = new List<string>();
            for (int i = 0; i < white; i++)
            {
                elements.Add(CubeTypeEnum.White);
            }
            for (int i = 0; i < blue; i++)
            {
                elements.Add(CubeTypeEnum.Blue);
            }
            elements.Shuffle();
            return elements;
        }

        public static List<Vector2Int> GenerateAllowedList(int max)
        {
            List<Vector2Int> places = new List<Vector2Int>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    places.Add(new Vector2Int(i, j));
                }
            }
            places.Shuffle();
            return places;
        }
    }
}
