using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace toolcad23.ViewModels.OtherViewModels
{
    internal class MyMessageBoxViewModel : BindableBase
    {
        private string messageText;
        public string MessageText
        {
            get { return messageText; }
            set { SetProperty(ref messageText, value); }
        }

        #region Commands
        public ICommand CloseWindowCommand { get; set; }
        #endregion

        internal MyMessageBoxViewModel() : this("") { }

        internal MyMessageBoxViewModel(string msg)
        {
            CloseWindowCommand = new DelegateCommand<object>(OnCloseWindowCommand);
            MessageText = msg;
        }

        private void OnCloseWindowCommand(object paramenter)
        {
            (paramenter as Window).Close();
        }
    }
}
