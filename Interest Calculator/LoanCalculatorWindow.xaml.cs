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
using System.Windows.Shapes;

namespace Interest_Calculator
{
    public partial class LoanCalculatorWindow : Window
    {
        public LoanCalculatorWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // ตรวจสอบและดึงข้อมูล 
            if (!double.TryParse(LoanAmountTextBox.Text, out double loanAmount) ||
                !double.TryParse(InterestRateTextBox.Text, out double interestRate) ||
                !int.TryParse(LoanTermTextBox.Text, out int loanTerm))
            {
                MessageBox.Show("กรุณากรอกข้อมูลเป็นตัวเลขให้ถูกต้อง", "ข้อมูลผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (loanAmount <= 0 || interestRate <= 0 || loanTerm <= 0)
            {
                MessageBox.Show("ข้อมูลต้องเป็นจำนวนบวก", "ข้อมูลผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // แปลงหน่วยให้เป็น "รายปี" (สำหรับดอกเบี้ย) และ "จำนวนเดือนทั้งหมด" (สำหรับระยะเวลา) 
            double annualRate;
            if (InterestRateUnitComboBox.SelectedIndex == 1) // ถ้าดอกเบี้ยเป็น "ต่อเดือน"
            {
                annualRate = interestRate * 12; // แปลงเป็นดอกเบี้ยต่อปี
            }
            else { annualRate = interestRate; }

            int totalTermInMonths;
            if (LoanTermUnitComboBox.SelectedIndex == 0) // ถ้าระยะเวลาเป็น "ปี"
            {
                totalTermInMonths = loanTerm * 12; // แปลงปีเป็นเดือน
            }
            else // ถ้าระยะเวลาเป็น "เดือน"
            {
                totalTermInMonths = loanTerm; // ใช้ค่าเดือนได้เลย
            }

            // เรียกใช้ Logic คำนวณ โดยส่ง totalTermInMonths เข้าไป
            var calculator = new LoanCalculatorLogic();
            var (summary, details) = calculator.Calculate(loanAmount, annualRate, totalTermInMonths);

            // เปิดหน้าต่างแสดงผล
            if (summary.MonthlyPayment > 0)
            {
                LoanResultWindow resultWindow = new LoanResultWindow(summary, details);
                resultWindow.Owner = this;
                resultWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("ไม่สามารถคำนวณได้ กรุณาตรวจสอบข้อมูล", "ข้อผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}

