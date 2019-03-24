using DepositBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NABTest
{
    public partial class DepositViewer : System.Web.UI.Page
    {
        DepositDirection currentDirection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                currentDirection = DepositEngine.Instance.CurrentDirection;
                Dictionary<int, string> list = new Dictionary<int, string>();
                DepositEngine.Instance.CurrentDirection = (DepositDirection)currentDirection;

                ActionList.SelectedValue = Convert.ToInt32(currentDirection).ToString();

            }
            MaturityAmountLabel.Text = DepositLedger.Instance.LedgerAmount.ToString("n2");
        }

        public IEnumerable<DepositBusiness.Deposit> GridView1_GetData()
        {
            return DepositLedger.Instance.Deposits;
        }

        protected void ActionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentDirection != (DepositDirection)Convert.ToInt32(ActionList.SelectedItem.Value))
            {
                DepositEngine.Instance.CurrentDirection = (DepositDirection)Convert.ToInt32(ActionList.SelectedItem.Value);
            }
        }

        protected void DepositTimer_Tick(object sender, EventArgs e)
        {
            DepositEngine.Instance.TimerTicked();
            MaturityAmountLabel.Text = DepositLedger.Instance.LedgerAmount.ToString("n2");
        }
    }
}