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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace toolcad23.Views.OtherViews
{
    /// <summary>
    /// Interaction logic for RightPanelControlView.xaml
    /// </summary>
    public partial class RightPanelControlView : UserControl
    {
        public string YellowText
        {
            get { return (string)GetValue(YellowTextProperty); }
            set { SetValue(YellowTextProperty, value); }
        }
        public static readonly DependencyProperty YellowTextProperty =
            DependencyProperty.Register("YellowText", typeof(string), typeof(RightPanelControlView));

        public string WhiteText
        {
            get { return (string)GetValue(WhiteTextProperty); }
            set { SetValue(WhiteTextProperty, value); }
        }
        public static readonly DependencyProperty WhiteTextProperty =
            DependencyProperty.Register("WhiteText", typeof(string), typeof(RightPanelControlView));

        public string BlueText
        {
            get { return (string)GetValue(BlueTextProperty); }
            set { SetValue(BlueTextProperty, value); }
        }
        public static readonly DependencyProperty BlueTextProperty =
            DependencyProperty.Register("BlueText", typeof(string), typeof(RightPanelControlView));

        public string MaxRedText
        {
            get { return (string)GetValue(MaxRedTextProperty); }
            set { SetValue(MaxRedTextProperty, value); }
        }
        public static readonly DependencyProperty MaxRedTextProperty =
            DependencyProperty.Register("MaxRedText", typeof(string), typeof(RightPanelControlView));

        public string MaxGreenText
        {
            get { return (string)GetValue(MaxGreenTextProperty); }
            set { SetValue(MaxGreenTextProperty, value); }
        }
        public static readonly DependencyProperty MaxGreenTextProperty =
            DependencyProperty.Register("MaxGreenText", typeof(string), typeof(RightPanelControlView));

        public ICommand RandomizeCommand
        {
            get { return (ICommand)GetValue(RandomizeCommandProperty); }
            set { SetValue(RandomizeCommandProperty, value); }
        }
        public static readonly DependencyProperty RandomizeCommandProperty =
            DependencyProperty.Register("RandomizeCommand", typeof(ICommand), typeof(RightPanelControlView));

        public RightPanelControlView()
        {
            InitializeComponent();
        }
    }
}
