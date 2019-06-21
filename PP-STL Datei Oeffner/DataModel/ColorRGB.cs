using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ColorRGB
    {
        public float R { get; } // Red
        public float G { get; } // Green 
        public float B { get; } // Blue


        public ColorRGB(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Used to create a background color object.
        /// </summary>
        /// <param name="r">Red value [float]</param>
        /// <param name="g">Green value [float]</param>
        /// <param name="b">Blue value [float]</param>
        /// <returns>The defined background color.</returns>
        public static ColorRGB CreateBackgroundColor(float r, float g, float b)
        {
            var backgroundColor = new ColorRGB(r, g, b);

            return backgroundColor;
        }

        /// <summary>
        /// Used to create a foreground color object.
        /// </summary>
        /// <param name="r">Red value [float]</param>
        /// <param name="g">Green value [float]</param>
        /// <param name="b">Blue value [float]</param>
        /// <returns>The defined foreground color.</returns>
        public static ColorRGB CreateForegroundColor(float r, float g, float b)
        {
            var foregroundColor = new ColorRGB(r, g, b);

            return foregroundColor;
        }
    }
}
