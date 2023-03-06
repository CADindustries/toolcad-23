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
using toolcad23.Views.OtherViews.RetrieveViews;

namespace toolcad23.Views.OtherViews.DeliveryViews
{
    public partial class DeliveryControlView : UserControl
    {
        public BitmapImage QRCodeImage
        {
            get { return (BitmapImage)GetValue(QRCodeImageProperty); }
            set { SetValue(QRCodeImageProperty, value); }
        }

        public static readonly DependencyProperty QRCodeImageProperty =
            DependencyProperty.Register("QRCodeImage", typeof(BitmapImage), typeof(DeliveryControlView));

        public string QRCodeText
        {
            get { return (string)GetValue(QRCodeTextProperty); }
            set { SetValue(QRCodeTextProperty, value); }
        }

        public static readonly DependencyProperty QRCodeTextProperty =
            DependencyProperty.Register("QRCodeText", typeof(string), typeof(DeliveryControlView));

        public DeliveryControlView()
        {
            InitializeComponent();
        }
    }
}
