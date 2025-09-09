using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interest_Calculator
{
    public class LoanCalculatorLogic
    {
        public (LoanResultSummary, List<MonthlyPaymentDetail>) Calculate(double principal, double annualRate, int totalMonths)
        {
            var summary = new LoanResultSummary();
            var details = new List<MonthlyPaymentDetail>();

            // ตรวจสอบข้อมูลเบื้องต้น
            if (principal <= 0 || annualRate < 0 || totalMonths <= 0)
                return (summary, details);

            double monthlyRate = (annualRate / 100) / 12;
            double remainingBalance = principal;

            // คำนวณค่างวดต่อเดือน
            if (monthlyRate > 0)
            {
                var ratePower = Math.Pow(1 + monthlyRate, totalMonths);
                summary.MonthlyPayment = principal * (monthlyRate * ratePower) / (ratePower - 1);
            }
            else
            {
                summary.MonthlyPayment = principal / totalMonths;
            }

            for (int i = 1; i <= totalMonths; i++)
            {
                double interestPaid = remainingBalance * monthlyRate;
                double principalPaid = summary.MonthlyPayment - interestPaid;
                remainingBalance -= principalPaid;

                if (i == totalMonths && remainingBalance > -1 && remainingBalance < 1)
                {
                    principalPaid += remainingBalance;
                    remainingBalance = 0;
                }

                details.Add(new MonthlyPaymentDetail
                {
                    MonthNumber = i,
                    InterestPaid = interestPaid,
                    PrincipalPaid = principalPaid,
                    RemainingBalance = remainingBalance
                });
            }

            summary.TotalPayment = summary.MonthlyPayment * totalMonths;
            summary.TotalInterest = summary.TotalPayment - principal;

            return (summary, details);
        }
    }
}
