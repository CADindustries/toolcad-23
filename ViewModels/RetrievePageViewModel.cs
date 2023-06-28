using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using toolcad23.Models;
using toolcad23.Models.Helpers;

namespace toolcad23.ViewModels
{
    internal class RetrievePageViewModel : BindableBase
    {
        private readonly RetrievePageModel model;
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes1 => model.GreenStandCubes1;
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes1 => model.RedStandCubes1;
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes2 => model.GreenStandCubes2;
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes2 => model.RedStandCubes2;
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes3 => model.GreenStandCubes3;
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes3 => model.RedStandCubes3;
        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubes4 => model.GreenStandCubes4;
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubes4 => model.RedStandCubes4;

        public int YellowText
        {
            get { return model.YellowText; }
            set { model.YellowText = value; }
        }

        public int WhiteText
        {
            get { return model.WhiteText; }
            set { model.WhiteText = value; }
        }

        public int BlueText
        {
            get { return model.BlueText; }
            set { model.BlueText = value; }
        }

        public int MaxRedText
        {
            get { return model.MaxRedText; }
            set { model.MaxRedText = value; }
        }

        public int MaxGreenText
        {
            get { return model.MaxGreenText; }
            set { model.MaxGreenText = value; }
        }

        #region Commands
        public ICommand RandomizeCommand => model.RandomizeCommand;
        public ICommand SavePictureCommand { get; }
        #endregion

        internal RetrievePageViewModel()
        {
            model = new RetrievePageModel();
            model.PropertyChanged += (s, a) => { RaisePropertyChanged(a.PropertyName); };
            model.ProblemRaised += (s, a) => 
            {
                MessageBoxFactory.Show(a);
            };

            SavePictureCommand = new DelegateCommand<object>((obj) =>
            {
                Functions.CreateBitmapFromVisualAndCopyToClipboard(obj as Visual);
            });
        }
    }
}
