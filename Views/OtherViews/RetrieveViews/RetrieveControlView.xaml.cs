using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using toolcad23.Models;

namespace toolcad23.Views.OtherViews.RetrieveViews
{
    /// <summary>
    /// Interaction logic for RetrieveControlView.xaml
    /// </summary>
    public partial class RetrieveControlView : UserControl
    {
        public ObservableCollection<BitmapImage> RedStandCubesCollection
        {
            get { return (ObservableCollection<BitmapImage>)GetValue(RedStandCubesCollectionProperty); }
            set { SetValue(RedStandCubesCollectionProperty, value); }
        }

        public static readonly DependencyProperty RedStandCubesCollectionProperty =
            DependencyProperty.Register("RedStandCubesCollection", typeof(ObservableCollection<BitmapImage>), typeof(RetrieveControlView));

        public ObservableCollection<BitmapImage> GreenStandCubesCollection
        {
            get { return (ObservableCollection<BitmapImage>)GetValue(GreenStandCubesCollectionProperty); }
            set { SetValue(GreenStandCubesCollectionProperty, value); }
        }

        public static readonly DependencyProperty GreenStandCubesCollectionProperty =
            DependencyProperty.Register("GreenStandCubesCollection", typeof(ObservableCollection<BitmapImage>), typeof(RetrieveControlView));

        public RetrieveControlView()
        {
            InitializeComponent();
        }
    }
}
