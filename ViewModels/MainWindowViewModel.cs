using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using toolcad23.Models;
using toolcad23.ViewModels.Commands;

namespace toolcad23.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private BitmapImage logoImage;
        public BitmapImage LogoImage
        {
            get { return logoImage; }
            set { logoImage = value; OnPropertyChanged(); }
        }

        private WindowState currentWindowState;
        public WindowState CurrentWindowState
        {
            get { return currentWindowState; }
            set { currentWindowState = value; OnPropertyChanged(); OnStateChanged(currentWindowState); }
        }

        private Visibility maximizeButtonVisibility;
        public Visibility MaximizeButtonVisibility
        {
            get { return maximizeButtonVisibility; }
            set { maximizeButtonVisibility = value; OnPropertyChanged(); }
        }

        private Visibility restoreButtonVisibility;
        public Visibility RestoreButtonVisibility
        {
            get { return restoreButtonVisibility; }
            set { restoreButtonVisibility = value; OnPropertyChanged(); }
        }

        private Visibility progressBarVisibility;
        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set { progressBarVisibility = value; OnPropertyChanged(); }
        }

        private Visibility checkAllDoneVisibility;
        public Visibility CheckAllDoneVisibility
        {
            get { return checkAllDoneVisibility; }
            set { checkAllDoneVisibility = value; OnPropertyChanged(); }
        }

        private Page mainFrameSource;
        public Page MainFrameSource
        {
            get { return mainFrameSource; }
            set { mainFrameSource = value; OnPropertyChanged(); }
        }
        
        private int selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set { selectedTabIndex = value; OnPropertyChanged(); TabSelectionChanged(selectedTabIndex); }
        }

        #region Commands
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand RestoreWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        #endregion

        public MainWindowViewModel() 
        {
            MinimizeWindowCommand = new DelegateCommand(OnMinimizeWindowCommand);
            MaximizeWindowCommand = new DelegateCommand(OnMaximizeWindowCommand);
            RestoreWindowCommand = new DelegateCommand(OnRestoreWindowCommand);
            CloseWindowCommand = new DelegateCommand(OnCloseWindowCommand);

            OnStateChanged(WindowState.Normal);
            OnActionChanged(false);
            SetLogoImage();

            GoToPage(UIFactory.GetInfoPageView());
        }

        private void SetLogoImage()
        {
            LogoImage = MainWindowModel.GetLogoImage();
        }

        private void GoToPage(Page page)
        {
            if (MainFrameSource != page)
            {
                MainFrameSource = page;
            }
        }

        private void TabSelectionChanged(int index)
        {
            // maybe cringe, idk
            switch (index)
            {
                case 0:
                    {
                        GoToPage(UIFactory.GetInfoPageView());
                        break;
                    }
                case 1:
                    {
                        GoToPage(UIFactory.GetRetrievePageView());
                        break;
                    }
                case 2:
                    {
                        GoToPage(UIFactory.GetDeliveryPageView());
                        break;
                    }
            }
        }

        private void OnActionChanged(bool done)
        {
            if (done)
            {
                ProgressBarVisibility = Visibility.Collapsed;
                CheckAllDoneVisibility = Visibility.Visible;
            }
            else
            {
                ProgressBarVisibility = Visibility.Visible;
                CheckAllDoneVisibility = Visibility.Collapsed;
            }
        }

        private void OnStateChanged(WindowState state)
        {
            if (state == WindowState.Maximized)
            {
                MaximizeButtonVisibility = Visibility.Collapsed;
                RestoreButtonVisibility = Visibility.Visible;
            }
            else
            {
                MaximizeButtonVisibility = Visibility.Visible;
                RestoreButtonVisibility = Visibility.Collapsed;
            }
        }

        private void OnMinimizeWindowCommand(object paramenter)
        {
            (paramenter as Window).WindowState = WindowState.Minimized;
        }

        private void OnMaximizeWindowCommand(object paramenter)
        {
            (paramenter as Window).WindowState = WindowState.Maximized;
        }

        private void OnRestoreWindowCommand(object paramenter)
        {
            (paramenter as Window).WindowState = WindowState.Normal;
        }

        private void OnCloseWindowCommand(object paramenter)
        {
            (paramenter as Window).Close();
        }
    }
}
