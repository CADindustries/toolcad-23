using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toolcad23.Models.Classes
{
    internal class Vector2Int
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        internal Vector2Int(int x, int y)
        {
            X = x; 
            Y = y;
        }
    }
}
