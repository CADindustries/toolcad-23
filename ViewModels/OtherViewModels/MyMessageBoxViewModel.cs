using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using toolcad23.ViewModels.Commands;

namespace toolcad23.ViewModels.OtherViewModels
{
    internal class MyMessageBoxViewModel : BaseViewModel
    {
        private string messageText;
        public string MessageText
        {
            get { return messageText; }
            set { messageText = value; OnPropertyChanged(); }
        }

        #region Commands
        public ICommand CloseWindowCommand { get; set; }
        #endregion

        internal MyMessageBoxViewModel() : this("") { }

        internal MyMessageBoxViewModel(string msg)
        {
            CloseWindowCommand = new DelegateCommand(OnCloseWindowCommand);
            MessageText = msg;
        }

        private void OnCloseWindowCommand(object paramenter)
        {
            (paramenter as Window).Close();
        }
    }
}
