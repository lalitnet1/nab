using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DepositBusiness
{
    public class DepositEngine
    {
        private readonly double MIN_MATURITY_AMOUNT;
        private readonly double MAX_MATURITY_AMOUNT;
        private readonly double MIN_INITIAL_MATURITY_AMOUNT;
        private readonly double MAX_INITIAL_MATURITY_AMOUNT;

        private readonly DateTime MIN_START_DATE;

        private DepositDirection _currentDirection;
        private Random _rand;

        public DepositDirection CurrentDirection
        {
            get { return _currentDirection; }
            set
            {
                _currentDirection = value;
            }
        }

        #region Singleton

        private static readonly Lazy<DepositEngine> _instance = new Lazy<DepositEngine>(() => new DepositEngine());

        public static DepositEngine Instance
        {
            get { return _instance.Value; }
        }

        #endregion

        private DepositEngine()
        {
            MIN_INITIAL_MATURITY_AMOUNT = 70000000;
            MAX_INITIAL_MATURITY_AMOUNT = 100000000;

            MIN_MATURITY_AMOUNT = 50000000;
            MAX_MATURITY_AMOUNT = 120000000;

            _currentDirection = DepositDirection.Hold;
            _rand = new Random();

            var today = DateTime.Today;
            MIN_START_DATE = new DateTime(today.Year - 5, today.Month, today.Day);

            Initialize(50);
        }

        private void Initialize(int initCount)
        {
            Deposit deposit;

            var maxLimitAmount = MAX_INITIAL_MATURITY_AMOUNT;
            var minLimitAmount = MIN_INITIAL_MATURITY_AMOUNT;

            for (int i = 0; i < initCount; i++)
            {
                deposit = GetNextDeposit(initCount, i);

                DepositLedger.Instance.AddDeposit(deposit);
            }
        }

        private Deposit GetNextDeposit(int initCount, int i)
        {
            Deposit deposit;
            var remainingAmount = MAX_INITIAL_MATURITY_AMOUNT - DepositLedger.Instance.LedgerAmount;
            var rangeStart = remainingAmount * 0.7 / (initCount - i);
            var rangeEnd = remainingAmount * 1.2 / (initCount - i);

            double nextMaturity = 0;
            do
            {
                nextMaturity = _rand.Next((int)rangeStart, (int)rangeEnd) + _rand.NextDouble();
            }
            while (DepositLedger.Instance.LedgerAmount + nextMaturity > MAX_INITIAL_MATURITY_AMOUNT);

            if (i == initCount - 1
                && DepositLedger.Instance.LedgerAmount + nextMaturity < MIN_INITIAL_MATURITY_AMOUNT)
            {
                nextMaturity = MIN_INITIAL_MATURITY_AMOUNT - DepositLedger.Instance.LedgerAmount;
            }

            deposit = new Deposit(
                nextMaturity,
                MIN_START_DATE.AddDays(_rand.Next(0, 365 * 5)),
                DateTime.Today.AddDays(_rand.Next(0, 365 * 5)),
                (double)_rand.Next(4, 6) + _rand.NextDouble());
            return deposit;
        }

        public void TimerTicked()
        {
            if (CurrentDirection == DepositDirection.Buy)
                BuyDeposit();
            else if (CurrentDirection == DepositDirection.Sell)
                SellDeposit();
        }

        private void BuyDeposit()
        {
            if(DepositLedger.Instance.LedgerAmount < MAX_MATURITY_AMOUNT)
            {
                var deposit = new Deposit(
                _rand.Next(1000000, 10000000) + _rand.NextDouble(),
                MIN_START_DATE.AddDays(_rand.Next(0, 365 * 5)),
                DateTime.Today.AddDays(_rand.Next(0, 365 * 5)),
                (double)_rand.Next(4, 6) + _rand.NextDouble());
            }
        }

        private void SellDeposit()
        {
            if(DepositLedger.Instance.LedgerAmount > MIN_MATURITY_AMOUNT)
            {
                do
                {
                    //Do Nothing
                }
                while (!DepositLedger.Instance.TryRemoveAt(_rand.Next(0, DepositLedger.Instance.Deposits.Count - 1)));
            }
        }
    }
}
