using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DataModel
{
    public class UserSettings
    {
        public Color BackgroundColor { get; set; } = Color.FromRgb(10, 10, 10);
        public Color ForegroundColor { get; set; } = Color.FromRgb(255, 255, 255);
        public Color ErrorColor { get; set; } = Color.FromRgb(255, 0, 0);

        public UserSettings(Color backgroundColor, Color foregroundColor, Color errorColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            ErrorColor = errorColor;
        }
    }
}
