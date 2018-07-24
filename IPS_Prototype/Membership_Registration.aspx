<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Membership_Registration.aspx.cs" Inherits="IPS_Prototype.Membership_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var now = new Date();
        var nextyr = now.getFullYear() + 1;
        now.setFullYear(nextyr);

        $(document).ready(function () {
            $("#ContentPlaceHolder1_datetime").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());
            $("#ContentPlaceHolder1_hiddentext").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());

            if ($('#ContentPlaceHolder1_MemebershipDDL').val() === "Friend of IPS" || $('#ContentPlaceHolder1_MemebershipDDL').val() === "") {
                $('#ContentPlaceHolder1_datetime')
                    .prop({ "disabled": false })
                    .val($.datepicker.formatDate("dd/mm/yy", now));
                 document.getElementById('ContentPlaceHolder1_hiddentext').value = document.getElementById('ContentPlaceHolder1_datetime').value;

            } else {
                console.log("set to readonly");
                $('#ContentPlaceHolder1_datetime')
                    .prop({ "disabled": true })
                    .val("NA");
                $('#ContentPlaceHolder1_hiddentext').val("NA");
            }

        });

        function populateHTxtBX() {
        document.getElementById('ContentPlaceHolder1_hiddentext').value = document.getElementById('ContentPlaceHolder1_datetime').value;

        };






    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="title" id="title" runat="server">Membership > Member Registration</label>

    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Add Member</div>
    <div class="SubHeader">

        <label for="MembershipType">Membership Type:</label>
        <div class="form-group">
            <div class="form-check form-check-inline">
                <label class="radio-inline">
                    <input type="radio" name="optradio" id="CA" runat="server" value="Coporate Associate" checked="true">Coporate Associate</label>

            </div>
            <div class="form-check form-check-inline">
                <label class="radio-inline">
                    <input type="radio" name="optradio" id="IA" runat="server" value="Individual Associate">Individual Associate</label>

            </div>

            <div class="form-group">
                <label for="Donor Tier">Donor Tier:</label>
                <div class="dropdownlist">
                    <asp:DropDownList ID="MemebershipDDL" Class="ddlStyle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSelected" Style="border: 1px solid #CCC; border-radius: 3px; width: 250px; height: 35px; padding-left: 5px;">
                        <asp:ListItem Text="Friend of IPS" Value="Friend of IPS"></asp:ListItem>
                        <asp:ListItem Text="Lifetime Friend of IPS" Value="Lifetime Friend of IPS"></asp:ListItem>
                        <asp:ListItem Text="Lifetime Benefactor of IPS" Value="Lifetime Benefactor of IPS"></asp:ListItem>
                        <asp:ListItem Text="Lifetime Patron of IPS" Value="Lifetime Patron of IPS"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>


            <div class="form-group">

                <label for="Member Expiry">Member Expiry:</label>
                <div class="form-group">
                    <input type="text" id="datetime" runat="server" onchange="populateHTxtBX();" autocomplete="off" />
                    <input type="text" id="hiddentext" runat="server" class="none"  />

                </div>

            </div>


            <div class="form-group">
                <button type="button" runat="server" onserverclick="Button_click1" class="btn btn-primary" style="width: 150px; height: 40px;">
                    NEXT
                    <i class="fas fa-arrow-right"></i>
                </button>

            </div>

        </div>
    </div>
</asp:Content>
