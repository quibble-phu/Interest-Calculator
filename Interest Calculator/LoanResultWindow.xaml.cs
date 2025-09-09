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
    /// <summary>
    /// Interaction logic for LoanResultWindow.xaml
    /// </summary>
    public partial class LoanResultWindow : Window
    {
        // Constructor ที่รับข้อมูลเข้ามา
        public LoanResultWindow(LoanResultSummary summary, List<MonthlyPaymentDetail> details)
        {
            InitializeComponent();
            DisplayResults(summary, details);
        }

        private void DisplayResults(LoanResultSummary summary, List<MonthlyPaymentDetail> details)
        {
            // แสดงข้อมูลสรุป
            MonthlyPaymentTextBlock.Text = $"ค่างวดต่อเดือน: {summary.MonthlyPayment:N2} บาท";
            TotalPaymentTextBlock.Text = $"ยอดชำระทั้งหมด: {summary.TotalPayment:N2} บาท";
            TotalInterestTextBlock.Text = $"ดอกเบี้ยทั้งหมด: {summary.TotalInterest:N2} บาท";

            // แสดงตารางการผ่อนชำระ
            AmortizationDataGrid.ItemsSource = details;
        }
    }
}
