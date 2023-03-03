using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace toolcad23.ViewModels
{
    internal class RetrievePageViewModel
    {
        public ObservableCollection<BitmapImage> RedStandCubesR1 { get; set; }
        public ObservableCollection<BitmapImage> GreenStandCubesR1 { get; set; }

        internal RetrievePageViewModel()
        {
            CleanUpAll();
        }

        private void CleanUpAll()
        {
            RedStandCubesR1 = new ObservableCollection<BitmapImage>() { null, null, null };
            GreenStandCubesR1 = new ObservableCollection<BitmapImage>() { null, null, null };
        }
    }
}
