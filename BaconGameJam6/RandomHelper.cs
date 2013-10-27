using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaconGameJam6
{
    public static class RandomHelper
    {
        public static Color[] Rainbow = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
        public static Color GetRandomColor(this Color[] colors)
        {
            Random r = new Random();
            int index = r.Next(0,colors.Length);
            return colors[index];
        }

        public static Color GetRandomColor()
        {
            Random r = new Random();
            int index = r.Next(0, Rainbow.Length);
            return Rainbow[index];
        }
    }
}
