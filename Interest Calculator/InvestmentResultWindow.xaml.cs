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
    /// Interaction logic for InvestmentResultWindow.xaml
    /// </summary>
    public partial class InvestmentResultWindow : Window
    {
        // ⭐ Constructor ที่แก้ไขแล้ว: รับ List ของผลลัพธ์เข้ามา
        public InvestmentResultWindow(List<InvestCalculatorWindow.YearlyBreakdown> calculationResults)
        {
            InitializeComponent();

            // เรียกฟังก์ชันเพื่อนำข้อมูลที่ "ได้รับมา" ไปแสดงผล
            DisplayResults(calculationResults);
        }

        // เมธอด DisplayResults ของคุณดีอยู่แล้ว ไม่ต้องแก้ไขครับ
        private void DisplayResults(List<InvestCalculatorWindow.YearlyBreakdown> results)
        {
            if (results != null && results.Any())
            {
                // นำข้อมูลสรุป (ยอดสุดท้าย) มาแสดงใน TextBlock ด้านบน
                double finalValue = results.Last().EndBalance;
                FinalValueTextBlock.Text = $"มูลค่ารวมในอนาคต: {finalValue:N2} บาท";

                // นำรายการทั้งหมดไปใส่ใน DataGrid
                ResultsDataGrid.ItemsSource = results;
            }
            else
            {
                FinalValueTextBlock.Text = "ไม่พบข้อมูลการคำนวณ";
            }
        }
    }
}
