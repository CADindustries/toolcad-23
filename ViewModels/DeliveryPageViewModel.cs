using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using toolcad23.Models;
using toolcad23.Models.Helpers;
using toolcad23.Models.Classes;
using toolcad23.ViewModels.Commands;

namespace toolcad23.ViewModels
{
    internal class DeliveryPageViewModel : BaseViewModel
    {
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
                MessageBoxFactory.Show("Неверный формат в \"Кол-во жёлтых\"");
                return;
            }
            if (!int.TryParse(WhiteText, out int parsedWhite))
            {
                MessageBoxFactory.Show("Неверный формат в \"Кол-во белых\"");
                return;
            }
            if (!int.TryParse(BlueText, out int parsedBlue))
            {
                MessageBoxFactory.Show("Неверный формат в \"Кол-во синих\"");
                return;
            }
            if (!int.TryParse(MaxRedText, out int parsedMaxRed))
            {
                MessageBoxFactory.Show("Неверный формат в \"MAX на красном\"");
                return;
            }
            if (!int.TryParse(MaxGreenText, out int parsedMaxGreen))
            {
                MessageBoxFactory.Show("Неверный формат в \"MAX на зеленом\"");
                return;
            }

            if (parsedMaxGreen > 3 || parsedMaxRed > 3)
            {
                MessageBoxFactory.Show("Максимально допустимое кол-во кубов в комнате не должно превосходить 3");
                return;
            }

            if (parsedMaxGreen < (parsedWhite + parsedBlue) / 4.0)
            {
                MessageBoxFactory.Show("Белых и синих кубов больше, чем максимально допустимое");
                return;
            }
            if (parsedMaxRed < parsedYellow / 4.0)
            {
                MessageBoxFactory.Show("Жёлтых кубов больше, чем максимально допустимое");
                return;
            }

            WaiterHelper.AddWaiter();

            Application.Current.Dispatcher.Invoke(() =>
            {
                CleanUpAll();
            });

            await Task.Run(() =>
            {
                List<List<string>> strings = DeliveryPageModel.GenerateRandomStrings(parsedMaxGreen, parsedMaxRed, parsedWhite, parsedBlue, parsedYellow);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    FillUpLists(strings);
                });
            });

            WaiterHelper.RemoveWaiter();
        }

        private void FillUpLists(List<List<string>> strings)
        {
            for (int i = 0; i < 4; i++)
            {
                QRCodeImages[i] = DeliveryPageModel.GetImage(strings[i]);
                QRCodeTexts[i] = DeliveryPageModel.GetText(strings[i]);
            }
        }
        #endregion

        internal DeliveryPageViewModel()
        {
            RandomizeCommand = new DelegateCommand(Randomize);
            SetUpAll();
            SetDefaults();
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

        private void CleanUpAll()
        {
            for (int i = 0; i < 4; i++)
            {
                QRCodeImages[i] = null;
                QRCodeTexts[i] = "";
            }
        }
    }
}
