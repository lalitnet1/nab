using System;

namespace DepositBusiness
{
    public class Deposit
    {
        public double Principle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double InterestRate { get; set; }
        public int Term { get; private set; }
        public double MaturityAmount { get; private set; }

        public Deposit(double maturityAmount, DateTime startDate, DateTime endDate, double interestRate)
        {
            this.MaturityAmount = maturityAmount;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.InterestRate = interestRate;

            if (EndDate > startDate)
            {
                TimeSpan difference = EndDate - StartDate;
                this.Term = difference.Days / 365;
                Principle = MaturityAmount / Math.Pow((1 + InterestRate), Term);
            }
        }
    }
}
