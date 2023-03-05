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
using toolcad23.ViewModels.Commands;

namespace toolcad23.ViewModels
{
    internal class RetrievePageViewModel : BaseViewModel
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/";

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
                List<Vector2Int> allowedGreenList = GenerateAllowedList(parsedMaxGreen);
                List<Vector2Int> allowedRedList = GenerateAllowedList(parsedMaxRed);

                List<string> greenStandElements = GenerateRandomElements(parsedWhite, parsedBlue);
                List<string> yellowStandElements = Enumerable.Repeat(CubeTypeEnum.Yellow, parsedYellow).ToList();

                Dictionary<Vector2Int, string> greenStandCubes = new Dictionary<Vector2Int, string>();
                Dictionary<Vector2Int, string> redStandCubes = new Dictionary<Vector2Int, string>();

                while (greenStandElements.Count > 0)
                {
                    greenStandCubes.Add(allowedGreenList.Pop(0), greenStandElements.Pop(0));
                }

                while (yellowStandElements.Count > 0)
                {
                    redStandCubes.Add(allowedRedList.Pop(0), yellowStandElements.Pop(0));
                }

                LowDownCubes(greenStandCubes);
                LowDownCubes(redStandCubes);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    FillUpBitmapList(greenStandCubes, true);
                    FillUpBitmapList(redStandCubes, false);
                });
            });

            MainWindowModel.IsAllDone = true;
        }

        private void FillUpBitmapList(Dictionary<Vector2Int, string> standCubes, bool isGreen)
        {
            for (int i = 0; i < 4; i++)
            {
                ObservableCollection<BitmapImage> collection = isGreen ? GreenStandCubes[i] : RedStandCubes[i];
                var currentRoomElements = standCubes.Where(x => x.Key.X == i).ToList();
                foreach (var currentElement in currentRoomElements)
                {
                    collection[currentElement.Key.Y] = GetImage(currentElement.Value);
                }
            }
        }

        private void LowDownCubes(Dictionary<Vector2Int, string> standCubes)
        {
            for (int i = 0; i < 4; i++)
            {
                var currentRoomElements = standCubes.Select(x => x.Key).Where(x => x.X == i).ToList();
                if (currentRoomElements.Count > 0)
                {
                    // cringe logic to low down the cubes in List
                    int counter = 0;
                    while (counter < currentRoomElements.Count)
                    {
                        var vect = currentRoomElements.FirstOrDefault(x => x.Y == counter);
                        if (vect == null)
                        {
                            foreach (var currentRoomElement in currentRoomElements.Where(x => x.Y > counter))
                            {
                                currentRoomElement.Y -= 1;
                            }
                        }
                        vect = currentRoomElements.FirstOrDefault(x => x.Y == counter);
                        if (vect != null)
                        {
                            counter++;
                        }
                    }
                }
            }
        }

        private List<string> GenerateRandomElements(int white, int blue)
        {
            List<string> elements = new List<string>();
            for (int i = 0; i < white; i++)
            {
                elements.Add(CubeTypeEnum.White);
            }
            for (int i = 0; i < blue; i++)
            {
                elements.Add(CubeTypeEnum.Blue);
            }
            elements.Shuffle();
            return elements;
        }

        private List<Vector2Int> GenerateAllowedList(int max)
        {
            List<Vector2Int> places = new List<Vector2Int>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    places.Add(new Vector2Int(i, j));
                }
            }
            places.Shuffle();
            return places;
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

        private BitmapImage GetImage(string cube)
        {
            return new BitmapImage(new Uri(imagePath + cube));
        }        
    }
}
