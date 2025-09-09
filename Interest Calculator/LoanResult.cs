using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interest_Calculator
{
    // Class สำหรับเก็บข้อมูลสรุป
    public class LoanResultSummary
    {
        public double MonthlyPayment { get; set; }
        public double TotalPayment { get; set; }
        public double TotalInterest { get; set; }
    }

    // Class สำหรับเก็บรายละเอียดการชำระเงินในแต่ละเดือน
    public class MonthlyPaymentDetail
    {
        public int MonthNumber { get; set; }
        public double PrincipalPaid { get; set; }
        public double InterestPaid { get; set; }
        public double RemainingBalance { get; set; }
    }
}
