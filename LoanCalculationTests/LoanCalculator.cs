using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalculationTests
{
    public class LoanCalculator
    {
        public decimal CalculateMonthlyPayment(decimal creditAmount, int term, decimal rate)
        {
            decimal monthlyRate = rate / (12 * 100);
            decimal monthlyPayment = (creditAmount * monthlyRate) / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -term));
            return Math.Round(monthlyPayment, 2);
        }

        public decimal CalculateTotalOverpayment(decimal creditAmount, int term, decimal rate, decimal monthlyPayment)
        {
            decimal totalOverpayment = (monthlyPayment * term) - creditAmount;
            return Math.Round(totalOverpayment, 2);
        }
    }
}
