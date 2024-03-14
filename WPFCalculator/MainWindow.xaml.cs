using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFCalculator.ViewModels;

namespace WPFCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public CalculatorViewModel ViewModel => (CalculatorViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var dig = (string)((Button)sender).Content;
            ViewModel.PressDigit(dig);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
           ViewModel.PressClear();
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            var op = (string)((Button)sender).Content;
            ViewModel.PressOperator(op);
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEquals();
        }
    }
}