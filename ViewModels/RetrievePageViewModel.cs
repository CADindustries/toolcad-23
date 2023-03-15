using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using toolcad23.Models;
using toolcad23.Models.Classes;
using toolcad23.Models.Helpers;
using toolcad23.ViewModels.Commands;

namespace toolcad23.ViewModels
{
    internal class RetrievePageViewModel : BaseViewModel
    {
        public ObservableCollection<ObservableCollection<BitmapImage>> GreenStandCubes { get; set; }
        public ObservableCollection<ObservableCollection<BitmapImage>> RedStandCubes { get; set; }

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
            SetUpAll();
            SetDefaults();
        }

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
                var greenStandCubes = RetrievePageModel.GenerateRandomGreenDictionary(parsedMaxGreen, parsedWhite, parsedBlue);
                var redStandCubes = RetrievePageModel.GenerateRandomRedDictionary(parsedMaxRed, parsedYellow);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    FillUpBitmapList(greenStandCubes, true);
                    FillUpBitmapList(redStandCubes, false);
                });
            });

            WaiterHelper.RemoveWaiter();
        }

        private void FillUpBitmapList(Dictionary<Vector2Int, string> standCubes, bool isGreen)
        {
            for (int i = 0; i < 4; i++)
            {
                ObservableCollection<BitmapImage> collection = isGreen ? GreenStandCubes[i] : RedStandCubes[i];
                var currentRoomElements = standCubes.Where(x => x.Key.X == i).ToList();
                foreach (var currentElement in currentRoomElements)
                {
                    collection[currentElement.Key.Y] = RetrievePageModel.GetImage(currentElement.Value);
                }
            }
        }
        #endregion

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
            GreenStandCubes = new ObservableCollection<ObservableCollection<BitmapImage>>()
            {
                new ObservableCollection<BitmapImage>() { null, null, null },
                new ObservableCollection<BitmapImage>() { null, null, null },
                new ObservableCollection<BitmapImage>() { null, null, null },
                new ObservableCollection<BitmapImage>() { null, null, null },
            };

            RedStandCubes = new ObservableCollection<ObservableCollection<BitmapImage>>()
            {
                new ObservableCollection<BitmapImage>() { null, null, null },
                new ObservableCollection<BitmapImage>() { null, null, null },
                new ObservableCollection<BitmapImage>() { null, null, null },
                new ObservableCollection<BitmapImage>() { null, null, null },
            };
        }

        private void CleanUpAll()
        {
            foreach (var stand in GreenStandCubes)
            {
                for (int i = 0; i < 3; i++)
                {
                    stand[i] = null;
                }
            }
            foreach (var stand in RedStandCubes)
            {
                for (int i = 0; i < 3; i++)
                {
                    stand[i] = null;
                }
            }
        }       
    }
}
