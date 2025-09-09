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

namespace Interest_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_loan(object sender, RoutedEventArgs e)
        {
         
            LoanCalculatorWindow loanWindow = new LoanCalculatorWindow();

          
            loanWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            loanWindow.Owner = this;
         
            loanWindow.ShowDialog();
        }

        private void Button_Click_exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Invest(object sender, RoutedEventArgs e)
        {
            InvestCalculatorWindow investWindow = new InvestCalculatorWindow();

         
            investWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            investWindow.Owner = this;

            investWindow.ShowDialog();
        }
    }
}
