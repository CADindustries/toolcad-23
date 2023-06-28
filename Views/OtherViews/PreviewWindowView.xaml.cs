using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace toolcad23.Views.OtherViews
{
    /// <summary>
    /// Interaction logic for PreviewWindowView.xaml
    /// </summary>
    public partial class PreviewWindowView : Window
    {
        public PreviewWindowView()
        {
            InitializeComponent();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
                var hwndTarget = hwndSource.CompositionTarget;
                hwndTarget.RenderMode = RenderMode.SoftwareOnly;
            }
            catch
            {

            }
        }
    }
}
