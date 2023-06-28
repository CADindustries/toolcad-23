using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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
        private static readonly Random rand = new Random((int)DateTime.Now.Ticks);
        public ReadOnlyObservableCollection<BitmapImage> RedStandCubesCollection
        {
            get { return (ReadOnlyObservableCollection<BitmapImage>)GetValue(RedStandCubesCollectionProperty); }
            set { SetValue(RedStandCubesCollectionProperty, value); }
        }

        public static readonly DependencyProperty RedStandCubesCollectionProperty =
            DependencyProperty.Register("RedStandCubesCollection", typeof(ReadOnlyObservableCollection<BitmapImage>), 
                typeof(RetrieveControlView), new PropertyMetadata(StandCubesCollectionPropertyChanged));

        public ReadOnlyObservableCollection<BitmapImage> GreenStandCubesCollection
        {
            get { return (ReadOnlyObservableCollection<BitmapImage>)GetValue(GreenStandCubesCollectionProperty); }
            set { SetValue(GreenStandCubesCollectionProperty, value); }
        }

        public static readonly DependencyProperty GreenStandCubesCollectionProperty =
            DependencyProperty.Register("GreenStandCubesCollection", typeof(ReadOnlyObservableCollection<BitmapImage>), 
                typeof(RetrieveControlView), new PropertyMetadata(StandCubesCollectionPropertyChanged));

        public RetrieveControlView()
        {
            InitializeComponent();
        }

        static void StandCubesCollectionPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is RetrieveControlView bc)
            {
                ((INotifyCollectionChanged)(e.NewValue as ReadOnlyObservableCollection<BitmapImage>)).CollectionChanged += (s, a) => bc.Randomize();
            }
        }

        // there is no need to make this by mvvm way
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SetBack();
        }

        private void SetBack()
        {
            Grid.SetColumn(RedStandGrid, 0);
            Grid.SetColumn(GreenStandGrid, 1);
        }

        private void Randomize()
        {
            if ((bool)SwapToggle.IsChecked)
            {
                var val = rand.Next(0, 2);
                Grid.SetColumn(RedStandGrid, val);
                Grid.SetColumn(GreenStandGrid, Math.Abs(val - 1));
            }
        }
    }
}
