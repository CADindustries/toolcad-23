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
    internal class DeliveryPageViewModel : BaseViewModel
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/QRCodes/";

        public ObservableCollection<BitmapImage> QRCodeImages { get; set; }
        public ObservableCollection<string> QRCodeTexts { get; set; }

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

        #region Randomizing
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

            if (parsedMaxGreen > 3 || parsedMaxRed > 3)
            {
                MessageBox.Show("Максимально допустимое кол-во кубов в комнате не должно превосходить 3");
                return;
            }

            if (parsedMaxGreen < (parsedWhite + parsedBlue) / 4)
            {
                MessageBox.Show("Белых и синих кубов больше, чем максимально допустимое");
                return;
            }
            if (parsedMaxRed < parsedYellow / 4)
            {
                MessageBox.Show("Жёлтых кубов больше, чем максимально допустимое");
                return;
            }

            MainWindowModel.IsAllDone = false;

            Application.Current.Dispatcher.Invoke(() =>
            {
                CleanUpAll();
            });

            await Task.Run(() =>
            {
                
            });

            MainWindowModel.IsAllDone = true;
        }
        #endregion

        internal DeliveryPageViewModel()
        {
            RandomizeCommand = new DelegateCommand(Randomize);
            SetUpAll();
            SetDefaults();
            Test();
        }

        private void SetDefaults()
        {
            YellowText = "2";
            WhiteText = "3";
            BlueText = "3";
            MaxGreenText = "2";
            MaxRedText = "1";
        }

        private void SetUpAll()
        {
            QRCodeImages = new ObservableCollection<BitmapImage>() { null, null, null, null };
            QRCodeTexts = new ObservableCollection<string>() { "", "", "", "" };
        }

        private void Test()
        {
            QRCodeImages[0] = GetImage(new List<string>() { "white" });
            QRCodeTexts[0] = "test, test";
        }

        private void CleanUpAll()
        {
            for (int i = 0; i < 4; i++)
            {
                QRCodeImages[i] = null;
                QRCodeTexts[i] = "";
            }
        }

        private BitmapImage GetImage(List<string> cubes)
        {
            if (cubes.Count > 0)
            {
                string joined = string.Join("_", cubes.ToArray());
                return new BitmapImage(new Uri(imagePath + joined + ".png"));
            }
            return new BitmapImage(new Uri(imagePath + "empty_qr.png"));
        }
    }
}
