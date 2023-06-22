using Prism.Commands;
using Prism.Mvvm;
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
using toolcad23.Models.Helpers;

namespace toolcad23.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private WindowState currentWindowState;
        public WindowState CurrentWindowState
        {
            get { return currentWindowState; }
            set { SetProperty(ref currentWindowState, value); OnStateChanged(currentWindowState); }
        }

        private Visibility maximizeButtonVisibility;
        public Visibility MaximizeButtonVisibility
        {
            get { return maximizeButtonVisibility; }
            set { SetProperty(ref maximizeButtonVisibility, value); }
        }

        private Visibility restoreButtonVisibility;
        public Visibility RestoreButtonVisibility
        {
            get { return restoreButtonVisibility; }
            set { SetProperty(ref restoreButtonVisibility, value); }
        }

        private Visibility progressBarVisibility;
        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set { SetProperty(ref progressBarVisibility, value); }
        }

        private Visibility checkAllDoneVisibility;
        public Visibility CheckAllDoneVisibility
        {
            get { return checkAllDoneVisibility; }
            set { SetProperty(ref checkAllDoneVisibility, value); }
        }

        private Page mainFrameSource;
        public Page MainFrameSource
        {
            get { return mainFrameSource; }
            set { SetProperty(ref mainFrameSource, value); }
        }
        
        private int selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set { SetProperty(ref selectedTabIndex, value); TabSelectionChanged(selectedTabIndex); }
        }

        #region Commands
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand RestoreWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        #endregion

        public MainWindowViewModel() 
        {
            MinimizeWindowCommand = new DelegateCommand<object>(OnMinimizeWindowCommand);
            MaximizeWindowCommand = new DelegateCommand<object>(OnMaximizeWindowCommand);
            RestoreWindowCommand = new DelegateCommand<object>(OnRestoreWindowCommand);
            CloseWindowCommand = new DelegateCommand<object>(OnCloseWindowCommand);

            WaiterHelper.CollectionChanged += OnStaticAllDoneChanged;
            WaiterHelper.AddWaiter();

            OnStateChanged(WindowState.Normal);
            OnActionChanged(false);

            GoToPage(UIFactory.GetInfoPageView());

            WaiterHelper.RemoveWaiter();
        }

        private void OnStaticAllDoneChanged(object sender, EventArgs e)
        {
            OnActionChanged(WaiterHelper.GetWaiterStatus());
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
            ProgressBarVisibility = done ? Visibility.Collapsed : Visibility.Visible;
            CheckAllDoneVisibility = done ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OnStateChanged(WindowState state)
        {
            MaximizeButtonVisibility = state == WindowState.Maximized ? Visibility.Collapsed : Visibility.Visible;
            RestoreButtonVisibility = state == WindowState.Maximized ? Visibility.Visible : Visibility.Collapsed;
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
