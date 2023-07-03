using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LoanCalculationTests
{
    [TestClass]
    public class LoanCalculatorTests
    {
        [TestMethod]
        public void TestCalculateMonthlyPayment()
        {
            // Arrange
            decimal creditAmount = 10000;
            int term = 12;
            decimal rate = 10;
            decimal expectedMonthlyPayment = 879.16m;
            LoanCalculator calculator = new LoanCalculator();

            // Act
            decimal actualMonthlyPayment = calculator.CalculateMonthlyPayment(creditAmount, term, rate);

            // Assert
            Assert.AreEqual(expectedMonthlyPayment, actualMonthlyPayment);
        }

        [TestMethod]
        public void TestCalculateTotalOverpayment()
        {
            // Arrange
            decimal creditAmount = 10000;
            int term = 12;
            decimal rate = 10;
            decimal monthlyPayment = 879.16m;
            decimal expectedTotalOverpayment = 549.92m;
            LoanCalculator calculator = new LoanCalculator();

            // Act
            decimal actualTotalOverpayment = calculator.CalculateTotalOverpayment(creditAmount, term, rate, monthlyPayment);

            // Assert
            Assert.AreEqual(expectedTotalOverpayment, actualTotalOverpayment);
        }
    }
}
