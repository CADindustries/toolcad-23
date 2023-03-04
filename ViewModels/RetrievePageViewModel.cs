using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using toolcad23.Models;

namespace toolcad23.ViewModels
{
    internal class RetrievePageViewModel
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/";
        public ObservableCollection<BitmapImage> RedStandCubesR1 { get; set; }
        public ObservableCollection<BitmapImage> GreenStandCubesR1 { get; set; }

        internal RetrievePageViewModel()
        {
            CleanUpAll();
            Test();
        }

        private void CleanUpAll()
        {
            RedStandCubesR1 = new ObservableCollection<BitmapImage>() { null, null, null };
            GreenStandCubesR1 = new ObservableCollection<BitmapImage>() { null, null, null };
        }

        private void Test()
        {
            GreenStandCubesR1 = new ObservableCollection<BitmapImage>() { GetImage(CubeTypeEnum.White), null, null };
        }

        private BitmapImage GetImage(string cube)
        {
            return new BitmapImage(new Uri(imagePath + cube));
        }
    }
}
