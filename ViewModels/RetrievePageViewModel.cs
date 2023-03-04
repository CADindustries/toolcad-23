using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using toolcad23.Models;
using toolcad23.ViewModels.Commands;

namespace toolcad23.ViewModels
{
    internal class RetrievePageViewModel : BaseViewModel
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/";
        public ObservableCollection<BitmapImage> RedStandCubesR1 { get; set; }
        public ObservableCollection<BitmapImage> GreenStandCubesR1 { get; set; }
        public ObservableCollection<BitmapImage> RedStandCubesR2 { get; set; }
        public ObservableCollection<BitmapImage> GreenStandCubesR2 { get; set; }
        public ObservableCollection<BitmapImage> RedStandCubesR3 { get; set; }
        public ObservableCollection<BitmapImage> GreenStandCubesR3 { get; set; }
        public ObservableCollection<BitmapImage> RedStandCubesR4 { get; set; }
        public ObservableCollection<BitmapImage> GreenStandCubesR4 { get; set; }

        private string yellowText;
        public string YellowText
        {
            get { return yellowText; }
            set { yellowText = value; OnPropertyChanged(); }
        }

        private string whiteText;
        public string WhiteText
        {
            get { return whiteText; }
            set { whiteText = value; OnPropertyChanged(); }
        }

        private string blueText;
        public string BlueText
        {
            get { return blueText; }
            set { blueText = value; OnPropertyChanged(); }
        }

        private string maxRedText;
        public string MaxRedText
        {
            get { return maxRedText; }
            set { maxRedText = value; OnPropertyChanged(); }
        }

        private string maxGreenText;
        public string MaxGreenText
        {
            get { return maxGreenText; }
            set { maxGreenText = value; OnPropertyChanged(); }
        }

        #region Commands
        public ICommand RandomizeCommand { get; set; }
        #endregion

        internal RetrievePageViewModel()
        {
            RandomizeCommand = new DelegateCommand(Randomize);
            CleanUpAll();
            Test();
        }

        async private void Randomize(object parameter)
        {
            if (!int.TryParse(YellowText, out int parsedYellow))
            {
                MessageBox.Show("Неверный формат в \"Кол-во жёлтых\"");
                return;
            }
            if (!int.TryParse(WhiteText, out int parsedWhite))
            {
                MessageBox.Show("Неверный формат в \"Кол-во белых\"");
                return;
            }
            if (!int.TryParse(BlueText, out int parsedBlue))
            {
                MessageBox.Show("Неверный формат в \"Кол-во синих\"");
                return;
            }
            if (!int.TryParse(MaxRedText, out int parsedMaxRed))
            {
                MessageBox.Show("Неверный формат в \"MAX на красном\"");
                return;
            }
            if (!int.TryParse(MaxGreenText, out int parsedMaxGreen))
            {
                MessageBox.Show("Неверный формат в \"MAX на зеленом\"");
                return;
            }

            await Task.Run(() =>
            {

            });
        }

        private void CleanUpAll()
        {
            RedStandCubesR1 = new ObservableCollection<BitmapImage>() { null, null, null };
            GreenStandCubesR1 = new ObservableCollection<BitmapImage>() { null, null, null };
            RedStandCubesR2 = new ObservableCollection<BitmapImage>() { null, null, null };
            GreenStandCubesR2 = new ObservableCollection<BitmapImage>() { null, null, null };
            RedStandCubesR3 = new ObservableCollection<BitmapImage>() { null, null, null };
            GreenStandCubesR3 = new ObservableCollection<BitmapImage>() { null, null, null };
            RedStandCubesR4 = new ObservableCollection<BitmapImage>() { null, null, null };
            GreenStandCubesR4 = new ObservableCollection<BitmapImage>() { null, null, null };
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
