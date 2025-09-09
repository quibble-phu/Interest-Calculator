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
    /// Interaction logic for InvestCalculatorWindow.xaml
    /// </summary>
    public partial class InvestCalculatorWindow : Window
    {
        public InvestCalculatorWindow()
        {
            InitializeComponent();
        }
        // คุณสามารถสร้าง Class นี้ไว้ในไฟล์เดียวกันหรือแยกไฟล์ก็ได้
        public class YearlyBreakdown
        {
            public int Year { get; set; }
            public double StartBalance { get; set; }
            public double TotalContribution { get; set; }
            public double InterestEarned { get; set; }
            public double EndBalance { get; set; }
        }

        /// <summary>
        /// คำนวณและแสดงรายละเอียดการเติบโตของพอร์ตแบบปีต่อปี
        /// </summary>
        /// <returns>รายการข้อมูลการเติบโตรายปี</returns>
        public List<YearlyBreakdown> CalculateInvestmentWithYearlyBreakdown(double principal, double monthlyContribution, double annualRate, int years)
        {
            // สร้าง List ว่างๆ เพื่อรอเก็บผลลัพธ์
            var breakdownList = new List<YearlyBreakdown>();

            double monthlyRate = (annualRate / 100) / 12;
            int totalMonths = years * 12;
            double currentBalance = principal;

            // วนลูปคำนวณทีละเดือน
            for (int month = 1; month <= totalMonths; month++)
            {
                // เพิ่มเงินลงทุนประจำเดือน
                currentBalance += monthlyContribution;

                // คำนวณดอกเบี้ยและทบต้น
                currentBalance += currentBalance * monthlyRate;

                // เมื่อครบทุก 12 เดือน (ครบปี) ให้บันทึกข้อมูลสรุปของปีนั้น
                if (month % 12 == 0)
                {
                    int currentYear = month / 12;

                    // คำนวณข้อมูลสรุปของปีนี้
                    double startBalanceOfYear = breakdownList.LastOrDefault()?.EndBalance ?? principal;
                    double totalContributionThisYear = monthlyContribution * 12;
                    double endBalanceOfYear = currentBalance;
                    double interestEarnedThisYear = endBalanceOfYear - startBalanceOfYear - totalContributionThisYear;

                    // สร้าง Object แล้วเพิ่มลงใน List
                    breakdownList.Add(new YearlyBreakdown
                    {
                        Year = currentYear,
                        StartBalance = startBalanceOfYear,
                        TotalContribution = totalContributionThisYear,
                        InterestEarned = interestEarnedThisYear,
                        EndBalance = endBalanceOfYear
                    });
                }
            }

            return breakdownList;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. ดึงข้อมูลจาก TextBox และแปลงค่า (ส่วนนี้เหมือนเดิม)
            // ... (ใส่โค้ดดึงข้อมูลและ TryParse ของคุณที่นี่) ...
            if (!double.TryParse(PrincipalTextBox.Text, out double principal) ||
                !double.TryParse(MonthlyContributionTextBox.Text, out double monthlyContribution) ||
                !double.TryParse(AnnualRateTextBox.Text, out double annualRate) ||
                !int.TryParse(YearsTextBox.Text, out int years))
            {
                // แสดงข้อความเตือนถ้าผู้ใช้กรอกข้อมูลไม่ถูกต้อง
                MessageBox.Show("กรุณากรอกข้อมูลเป็นตัวเลขให้ถูกต้อง", "ข้อมูลผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (principal <= 0 || monthlyContribution <= 0 || annualRate <= 0 || years <= 0)
            {
                MessageBox.Show("ข้อมูลต้องเป็นจำนวนบวก", "ข้อมูลผิดพลาด", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 2. เรียกใช้ฟังก์ชันคำนวณ (ส่วนนี้เหมือนเดิม)
            List<YearlyBreakdown> results = CalculateInvestmentWithYearlyBreakdown(principal, monthlyContribution, annualRate, years);

            // 3. ✨ สร้างและเปิดหน้าต่างใหม่ พร้อม "ส่ง" ผลลัพธ์เข้าไป ✨
            if (results != null)
            {
                // สร้าง instance ของหน้าต่างแสดงผล โดยส่ง list ผลลัพธ์เข้าไปใน Constructor
                InvestmentResultWindow resultWindow = new InvestmentResultWindow(results);

                // แสดงหน้าต่างใหม่แบบ Dialog
                resultWindow.ShowDialog();
            }
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
