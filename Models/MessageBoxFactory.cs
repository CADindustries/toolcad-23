using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using toolcad23.ViewModels.OtherViewModels;
using toolcad23.Views.OtherViews;

namespace toolcad23.Models
{
    internal class MessageBoxFactory
    {
        internal static void Show(string msg)
        {
            MyMessageBox mb = new MyMessageBox();
            // not mvvm way
            mb.Owner = System.Windows.Application.Current.MainWindow;
            mb.DataContext = new MyMessageBoxViewModel(msg);
            mb.ShowDialog();
        }
    }
}
