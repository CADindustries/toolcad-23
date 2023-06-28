using Prism.Commands;
using Prism.Mvvm;
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
using toolcad23.Models.Classes;
using toolcad23.Models.Helpers;

namespace toolcad23.Models
{
    internal class DeliveryPageModel : BindableBase
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/QRCodes/";

        private readonly ObservableCollection<BitmapImage> qrCodeImages = new ObservableCollection<BitmapImage>() { null, null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> QRCodeImages { get; set; }
        private readonly ObservableCollection<string> qrCodeTexts = new ObservableCollection<string>() { "", "", "", "" };
        public ReadOnlyObservableCollection<string> QRCodeTexts { get; set; }

        private int yellowText;
        public int YellowText
        {
            get { return yellowText; }
            set { SetProperty(ref yellowText, value); }
        }

        private int whiteText;
        public int WhiteText
        {
            get { return whiteText; }
            set { SetProperty(ref whiteText, value); }
        }

        private int blueText;
        public int BlueText
        {
            get { return blueText; }
            set { SetProperty(ref blueText, value); }
        }

        private int maxRedText;
        public int MaxRedText
        {
            get { return maxRedText; }
            set { SetProperty(ref maxRedText, value); }
        }

        private int maxGreenText;
        public int MaxGreenText
        {
            get { return maxGreenText; }
            set { SetProperty(ref maxGreenText, value); }
        }

        internal event EventHandler<string> ProblemRaised;

        #region Commands
        public ICommand RandomizeCommand { get; set; }
        #endregion

        public DeliveryPageModel() 
        {
            RandomizeCommand = new DelegateCommand(Randomize);
            SetDefaults();
            QRCodeImages = new ReadOnlyObservableCollection<BitmapImage>(qrCodeImages);
            QRCodeTexts = new ReadOnlyObservableCollection<string>(qrCodeTexts);
        }

        private void SetDefaults()
        {
            YellowText = 2;
            WhiteText = 3;
            BlueText = 3;
            MaxGreenText = 2;
            MaxRedText = 1;
        }

        private void CleanUpAll()
        {
            qrCodeImages.Reset();
            qrCodeTexts.Reset();
        }

        #region Randomizing
        async private void Randomize()
        {
            if (MaxGreenText > 3 || MaxRedText > 3)
            {
                ProblemRaised?.Invoke(this, "Максимально допустимое кол-во кубов в комнате не должно превосходить 3");
                return;
            }

            if (MaxGreenText < (WhiteText + BlueText) / 4.0)
            {
                ProblemRaised?.Invoke(this, "Белых и синих кубов больше, чем максимально допустимое");
                return;
            }
            if (MaxRedText < YellowText / 4.0)
            {
                ProblemRaised?.Invoke(this, "Жёлтых кубов больше, чем максимально допустимое");
                return;
            }

            WaiterHelper.AddWaiter();

            Application.Current.Dispatcher.Invoke(() =>
            {
                CleanUpAll();
            });

            await Task.Run(() =>
            {
                List<List<string>> strings = GenerateRandomStrings();

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
                qrCodeImages[i] = GetImage(strings[i]);
                qrCodeTexts[i] = GetText(strings[i]);
            }
        }

        internal List<List<string>> GenerateRandomStrings()
        {
            List<Vector2Int> allowedGreenList = RandomHelper.GenerateAllowedList(MaxGreenText);
            List<Vector2Int> allowedRedList = RandomHelper.GenerateAllowedList(MaxRedText);

            List<string> greenStandElements = RandomHelper.GenerateRandomElements(WhiteText, BlueText);
            List<string> redStandElements = Enumerable.Repeat(CubeTypeEnum.Yellow, YellowText).ToList();

            Dictionary<Vector2Int, string> greenStandCubes = new Dictionary<Vector2Int, string>();
            Dictionary<Vector2Int, string> redStandCubes = new Dictionary<Vector2Int, string>();

            while (greenStandElements.Count > 0)
            {
                greenStandCubes.Add(allowedGreenList.Pop(0), greenStandElements.Pop(0));
            }

            while (redStandElements.Count > 0)
            {
                redStandCubes.Add(allowedRedList.Pop(0), redStandElements.Pop(0));
            }

            RandomHelper.LowDownCubes(greenStandCubes);
            RandomHelper.LowDownCubes(redStandCubes);

            return GenerateStringLists(greenStandCubes, redStandCubes);
        }

        private List<List<string>> GenerateStringLists(Dictionary<Vector2Int, string> green, Dictionary<Vector2Int, string> red)
        {
            List<List<string>> strings = new List<List<string>>()
            {
                new List<string>(),
                new List<string>(),
                new List<string>(),
                new List<string>(),
            };
            for (int i = 0; i < 4; i++)
            {
                foreach (string s in green.Where(x => x.Key.X == i).Select(x => x.Value))
                {
                    strings[i].Add(s);
                }
                foreach (string s in red.Where(x => x.Key.X == i).Select(x => x.Value))
                {
                    strings[i].RandomlyAdd(s);
                }
            }
            return strings;
        }

        private static BitmapImage GetImage(List<string> cubes)
        {
            if (cubes.Count > 0)
            {
                string joined = string.Join("_", cubes.ToArray());
                try
                {
                    return new BitmapImage(new Uri(imagePath + joined + ".png"));
                }
                catch (IOException)
                {
                    // usually this is because of many yellow cubes, that do not have qr_codes
                    return new BitmapImage(new Uri(imagePath + "no_qr.jpg"));
                }
            }
            return new BitmapImage(new Uri(imagePath + "empty_qr.png"));
        }

        private static string GetText(List<string> cubes)
        {
            if (cubes.Count > 0)
            {
                string joined = string.Join(", ", cubes.ToArray());
                return joined;
            }
            return "empty";
        }

        #endregion
    }
}
