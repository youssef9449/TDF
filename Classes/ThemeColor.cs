using System;
using System.Collections.Generic;
using System.Drawing;

namespace TDF.Classes
{
    public static class ThemeColor
    {
        static Random random;
        static int tempIndex;

        public static Color PrimaryColor { get; set; }
        public static Color SecondaryColor { get; set; }
        public static Color LightColor { get; set; }
        public static List<string> ColorList = new List<string>()
        {
    "#3F51B5", // Blue Indigo
    "#9C27B0", // Purple
    "#2196F3", // Blue
    "#E41A4A", // Red Pink
    "#5978BB", // Light Blue
    "#018790", // Teal Green
    "#00B0AD", // Aqua
    "#721D47", // Dark Magenta
    "#A12059", // Deep Pink
    "#126881", // Dark Cyan
    "#0094BC", // Sky Blue
    "#E4126B", // Pinkish Red
    "#43B76E", // Green
    "#B71C46"  // Maroon Red
        };
        static ThemeColor()
        {
            random = new Random();
        }
        public static Color changeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;
            //If correction factor is less than 0, darken color.
            if (correctionFactor < 0)
            {
                correctionFactor++;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            //If correction factor is greater than zero, lighten color.
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
        public static Color SelectThemeColor()
        {
            int index = random.Next(ColorList.Count);

            while (tempIndex == index)
            {
                index = random.Next(ColorList.Count);
            }
            tempIndex = index;
            string color = ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
    }
}
