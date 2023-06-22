using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    public partial class RetrieveControlView : UserControl
    {
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubesCollection
        {
            get { return (ReadOnlyObservableCollection<BitmapImage>)GetValue(RedStandCubesCollectionProperty); }
            set { SetValue(RedStandCubesCollectionProperty, value); }
        }

        public static readonly DependencyProperty RedStandCubesCollectionProperty =
            DependencyProperty.Register("RedStandCubesCollection", typeof(ReadOnlyObservableCollection<BitmapImage>), typeof(RetrieveControlView));

        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubesCollection
        {
            get { return (ReadOnlyObservableCollection<BitmapImage>)GetValue(GreenStandCubesCollectionProperty); }
            set { SetValue(GreenStandCubesCollectionProperty, value); }
        }

        public static readonly DependencyProperty GreenStandCubesCollectionProperty =
            DependencyProperty.Register("GreenStandCubesCollection", typeof(ReadOnlyObservableCollection<BitmapImage>), typeof(RetrieveControlView));

        public RetrieveControlView()
        {
            InitializeComponent();
        }
    }
}
