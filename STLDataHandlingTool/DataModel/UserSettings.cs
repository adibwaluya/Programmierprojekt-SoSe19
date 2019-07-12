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
        public Color BackgroundColor
        {
            get { return BackgroundColor; }
            set { BackgroundColor = Color.FromRgb(10, 10, 10); }
        }
        public Color ForegroundColor
        {
            get { return ForegroundColor; }
            set { ForegroundColor = Color.FromRgb(255, 255, 255); }
        }
        public Color ErrorColor
        {
            get { return ErrorColor; }
            set { ErrorColor = Color.FromRgb(255, 0, 0); }
        }

        public UserSettings(Color backgroundColor, Color foregroundColor, Color errorColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            ErrorColor = errorColor;
        }
    }
}
