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
        internal static BitmapImage GetLogoImage()
        {
            return new BitmapImage(new Uri("pack://application:,,,/Resources/logo_tc23.png"));
        }
    }
}
