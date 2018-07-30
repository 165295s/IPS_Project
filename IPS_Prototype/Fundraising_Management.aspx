<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Fundraising_Management.aspx.cs" Inherits="IPS_Prototype.Fundraising_Management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Pagination.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="title">Fundraising Management</label>
    <br />
    <h3 style="font-family: Arial, Helvetica, sans-serif;font-size:25px;">Donors</h3>
    <hr />
        <div runat="server" id="fr_donors">
            <asp:GridView PagerStyle-HorizontalAlign="Right" CssClass="table table-hover BlueTable pagination-ys" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDonors_PageIndexChanging" ID="gvDonors" OnRowEditing="gvDonors_RowEditing" AutoGenerateColumns="false" AutoGenerateEditButton="false" runat="server" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
            <Columns>
                <asp:BoundField DataField="PERSON_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:BoundField DataField="ORG_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:BoundField DataField="FULLNAME_NAMETAGS" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:BoundField DataField="DONATION_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:BoundField DataField="PROSPECTIVE_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:TemplateField HeaderText="Donor Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("FULLNAME_NAMETAGS")%>'></asp:Label>
                        <asp:Label ID="lblPros" runat="server" Text='<%#Eval("Indicator")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DONATION_AMOUNT" HtmlEncode="False" DataFormatString="{0:c}" HeaderText="Donation Amount" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="DONATION_DT" HtmlEncode="False" DataFormatString="{0:d}" HeaderText="Donation Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="EVENT_NAME" HeaderText="Related to Events" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:CommandField EditText="Select" ShowEditButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
    