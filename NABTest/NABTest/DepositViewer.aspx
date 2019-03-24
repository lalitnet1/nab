<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepositViewer.aspx.cs" Inherits="NABTest.DepositViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager runat="server">
                

            </asp:ScriptManager>
        <div>
            <asp:Timer ID="DepositTimer" runat="server" OnTick="DepositTimer_Tick" Interval="2000"></asp:Timer>
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        Total Maturity Amount:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="MaturityAmountLabel" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        Action:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ActionList" runat="server" OnSelectedIndexChanged="ActionList_SelectedIndexChanged">
                            <asp:ListItem Text="Hold" Value="0"/>
                            <asp:ListItem Text="Buy" Value="1"/>
                            <asp:ListItem Text="Sell" Value="2"/>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"
                ShowHeader="true" ShowFooter="true" GridLines="Both" CellPadding="4"
                ItemType="DepositBusiness.Deposit" SelectMethod="GridView1_GetData"
                CssClass="table table-striped table-bordered"></asp:GridView>
        </div>
    </form>
</body>
</html>
