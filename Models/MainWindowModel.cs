using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace toolcad23.Models
{
    internal class MainWindowModel
    {
        private static bool isAllDone;
        public static EventHandler IsAllDoneChanged;
        public static bool IsAllDone
        {
            get { return isAllDone; }
            set { isAllDone = value; IsAllDoneChanged?.Invoke(null, EventArgs.Empty); }
        }

        internal static BitmapImage GetLogoImage()
        {
            return new BitmapImage(new Uri("pack://application:,,,/Resources/logo_tc23.png"));
        }
    }
}
