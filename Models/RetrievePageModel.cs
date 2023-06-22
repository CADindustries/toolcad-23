using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    internal class RetrievePageModel : BindableBase
    {
        private static readonly string imagePath = "pack://application:,,,/Resources/";

        private readonly ObservableCollection<BitmapImage> greenStandCubes1 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes1 { get; set; }
        private readonly ObservableCollection<BitmapImage> redStandCubes1 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes1 { get; set; }
        private readonly ObservableCollection<BitmapImage> greenStandCubes2 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes2 { get; set; }
        private readonly ObservableCollection<BitmapImage> redStandCubes2 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes2 { get; set; }
        private readonly ObservableCollection<BitmapImage> greenStandCubes3 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes3 { get; set; }
        private readonly ObservableCollection<BitmapImage> redStandCubes3 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes3 { get; set; }
        private readonly ObservableCollection<BitmapImage> greenStandCubes4 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes4 { get; set; }
        private readonly ObservableCollection<BitmapImage> redStandCubes4 = new ObservableCollection<BitmapImage>() { null, null, null };
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes4 { get; set; }

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

        public RetrievePageModel()
        {
            RandomizeCommand = new DelegateCommand(Randomize);
            SetDefaults();
            GreenStandCubes1 = new ReadOnlyObservableCollection<BitmapImage>(greenStandCubes1);
            GreenStandCubes2 = new ReadOnlyObservableCollection<BitmapImage>(greenStandCubes2);
            GreenStandCubes3 = new ReadOnlyObservableCollection<BitmapImage>(greenStandCubes3);
            GreenStandCubes4 = new ReadOnlyObservableCollection<BitmapImage>(greenStandCubes4);
            RedStandCubes1 = new ReadOnlyObservableCollection<BitmapImage>(redStandCubes1);
            RedStandCubes2 = new ReadOnlyObservableCollection<BitmapImage>(redStandCubes2);
            RedStandCubes3 = new ReadOnlyObservableCollection<BitmapImage>(redStandCubes3);
            RedStandCubes4 = new ReadOnlyObservableCollection<BitmapImage>(redStandCubes4);
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
            greenStandCubes1.Reset();
            greenStandCubes2.Reset();
            greenStandCubes3.Reset();
            greenStandCubes4.Reset();
            redStandCubes1.Reset();
            redStandCubes2.Reset();
            redStandCubes3.Reset();
            redStandCubes4.Reset();
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
                var greenStandCubes = GenerateRandomGreenDictionary();
                var redStandCubes = GenerateRandomRedDictionary();

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
            ObservableCollection<BitmapImage>[] greens = { greenStandCubes1, greenStandCubes2, greenStandCubes3, greenStandCubes4 };
            ObservableCollection<BitmapImage>[] reds = { redStandCubes1, redStandCubes1, redStandCubes2, redStandCubes3 };

            for (int i = 0; i < 4; i++)
            {
                ObservableCollection<BitmapImage> collection = isGreen ? greens[i] : reds[i];
                var currentRoomElements = standCubes.Where(x => x.Key.X == i).ToList();
                foreach (var currentElement in currentRoomElements)
                {
                    collection[currentElement.Key.Y] = GetImage(currentElement.Value);
                }
            }
        }
        

        internal Dictionary<Vector2Int, string> GenerateRandomGreenDictionary()
        {
            List<Vector2Int> allowedGreenList = RandomHelper.GenerateAllowedList(MaxGreenText);
            List<string> greenStandElements = RandomHelper.GenerateRandomElements(WhiteText, BlueText);
            Dictionary<Vector2Int, string> greenStandCubes = new Dictionary<Vector2Int, string>();

            while (greenStandElements.Count > 0)
            {
                greenStandCubes.Add(allowedGreenList.Pop(0), greenStandElements.Pop(0));
            }

            RandomHelper.LowDownCubes(greenStandCubes);

            return greenStandCubes;
        }

        internal Dictionary<Vector2Int, string> GenerateRandomRedDictionary()
        {
            List<Vector2Int> allowedRedList = RandomHelper.GenerateAllowedList(MaxRedText);
            List<string> redStandElements = Enumerable.Repeat(CubeTypeEnum.Yellow, YellowText).ToList();
            Dictionary<Vector2Int, string> redStandCubes = new Dictionary<Vector2Int, string>();

            while (redStandElements.Count > 0)
            {
                redStandCubes.Add(allowedRedList.Pop(0), redStandElements.Pop(0));
            }

            RandomHelper.LowDownCubes(redStandCubes);

            return redStandCubes;
        }

        private static BitmapImage GetImage(string cube)
        {
            return new BitmapImage(new Uri(imagePath + cube + "_cube.png"));
        }

        #endregion 
    }
}
