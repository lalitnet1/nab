using System;
using System.Collections.Generic;
using System.Text;

namespace DepositBusiness
{
    public class DepositLedger
    {
        private List<Deposit> _depositList;

        public double LedgerAmount { get; private set; }

        public IReadOnlyCollection<Deposit> Deposits
        {
            get { return _depositList.AsReadOnly(); }
        }

        #region Singleton

        private static readonly Lazy<DepositLedger> _instance = new Lazy<DepositLedger>(() => new DepositLedger());

        public static DepositLedger Instance
        {
            get { return _instance.Value; }
        }

        #endregion

        private DepositLedger()
        {
            _depositList = new List<Deposit>();
            LedgerAmount = 0;
        }

        public void AddDeposit(double principle, DateTime startDate, DateTime endDate, double interestRate)
        {
            var deposit = new Deposit(principle, startDate, endDate, interestRate);
            AddDeposit(deposit);
        }

        public void AddDeposit(Deposit deposit)
        {
            _depositList.Add(deposit);
            LedgerAmount += deposit.MaturityAmount;
        }

        public bool Remove(Deposit dep)
        {
            var isRemoved = _depositList.Remove(dep);
            LedgerAmount -= isRemoved ? dep.MaturityAmount : 0;

            return isRemoved;
        }

        public bool TryRemoveAt(int index)
        {
            if(_depositList.Count > index)
            {
                var deposit = _depositList[index];
                return _depositList.Remove(deposit);
            }

            return false;
        }
    }
}
