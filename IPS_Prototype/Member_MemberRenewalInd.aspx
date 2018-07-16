<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Member_MemberRenewalInd.aspx.cs" Inherits="IPS_Prototype.Member_MemberRenewalInd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>       
        // checkbox checked
        function showControlsAfterPostBackChecked() {
            $("#ContentPlaceHolder1_grouping").css("display", "block");
            displaySuccess('Successfully Renewed!');
            var dropdownValue = $('#ContentPlaceHolder1_Donor_Tier').val();
            handleDropDownChange1(dropdownValuerefreshed);
        }

        function showControlsAfterPostBackCheckedFailure() {
            $("#ContentPlaceHolder1_grouping").css("display", "block");
            displayFailure();
            var dropdownValue = $('#ContentPlaceHolder1_Donor_Tier').val();
            handleDropDownChange1(dropdownValuerefreshed);
        }

        function showControlsAfterPostBackUnchecked() {
            displaySuccess('Successfully Renewed!');
            var dropdownValue = $('#ContentPlaceHolder1_Donor_Tier').val();
            handleDropDownChange1(dropdownValuerefreshed);
        }

        function handleDropDownChange1(dropdownValuerefreshed) {
            console.log("event called");
            // Filter ddl w/ date
            if (dropdownValuerefreshed === "Friend of IPS" || dropdownValuerefreshed === "") {
                $('#ContentPlaceHolder1_datetime')
                    .prop({ "disabled": false })
                    .val($.datepicker.formatDate("dd/mm/yy"));
            } else {
                console.log("set to readonly");
                $('#ContentPlaceHolder1_datetime')
                    .prop({ "disabled": true })
                    .val("NA");
            }

            // Change tb according to ddl
            if (dropdownValuerefreshed === "") {
                document.getElementById("ContentPlaceHolder1_memfee").value = ""
            }
            if (dropdownValuerefreshed === "Friend of IPS") {
                $('#ContentPlaceHolder1_memfee').val(10000);
            }
            if (dropdownValuerefreshed === "Lifetime Friend of IPS") {
                $('#ContentPlaceHolder1_memfee').val(100000);
            }
            if (dropdownValuerefreshed === "Lifetime Benefactor of IPS") {
                $('#ContentPlaceHolder1_memfee').val(500000);
            }
            if (dropdownValuerefreshed === "Lifetime Patron of IPS") {
                document.getElementById("ContentPlaceHolder1_memfee").value = 1000000;
            }
        }



        function handleDropDownChange(dropdownValue) {
            console.log("event called");
            // Filter ddl w/ date
            var now = new Date();
            var nextyr = now.getFullYear() + 1;
            now.setFullYear(nextyr);

            if (dropdownValue === "Friend of IPS" || dropdownValue === "") {
                $('#ContentPlaceHolder1_datetime')
                    .prop({ "disabled": false })
                    .val($.datepicker.formatDate("dd/mm/yy", now));
            } else {
                console.log("set to readonly");
                $('#ContentPlaceHolder1_datetime')
                    .prop({ "disabled": true })
                    .val("NA");
            }

            // Change tb according to ddl
            if (dropdownValue === "") {
                document.getElementById("ContentPlaceHolder1_memfee").value = ""
            }
            if (dropdownValue === "Friend of IPS") {
                $('#ContentPlaceHolder1_memfee').val(10000);
            }
            if (dropdownValue === "Lifetime Friend of IPS") {
                $('#ContentPlaceHolder1_memfee').val(100000);
            }
            if (dropdownValue === "Lifetime Benefactor of IPS") {
                $('#ContentPlaceHolder1_memfee').val(500000);
            }
            if (dropdownValue === "Lifetime Patron of IPS") {
                document.getElementById("ContentPlaceHolder1_memfee").value = 1000000;
            }
        }

        $(document).ready(function () {

            $('#ContentPlaceHolder1_cbInstallment').click(function () {
                 
            $('#dvInstallment').toggle();
        });

            var now = new Date();
            var nextyr = now.getFullYear() + 1;
            now.setFullYear(nextyr);

            $('#UserRenewalHeader > .fas').toggleClass('active');
            $('#UserRenewal').collapse();
            $('#ContentPlaceHolder1_datetime').datepicker({
                dateFormat: "dd/mm/yy", now
            });
            $('#ContentPlaceHolder1_paymentreceiveddate').datepicker({
                dateFormat: "dd/mm/yy"
            });

            $('#ContentPlaceHolder1_memfee').blur(function () {
                // when there's a blur we need to switch based of selected Donor tier
                var donorTier = $('#ContentPlaceHolder1_Donor_Tier').val()
                var v = parseInt($(this).val());
                console.log('donorTier: ', donorTier, '\nmemFee: ', v);
                switch (donorTier) {
                    case "Friend of IPS": {
                        if (v > 99999) {
                            $(this).val('99999')
                        }
                        break;
                    }
                    case "Lifetime Friend of IPS": {
                        if (v > 499999) {
                            $(this).val('499999');
                        }
                        if (v < 100000) {
                            $(this).val("100000");
                        }
                        break;
                    }
                    case "Lifetime Benefactor of IPS": {
                        if (v > 999999) {
                            $(this).val('999999');
                        }
                        else if (v < 500000) {
                            $(this).val("500000");
                        }
                        break;
                    }
                    case "Lifetime Patron of IPS": {
                        if (v < 1000000) {
                            $(this).val('1000000');
                        }
                        break;
                    }
                }
            });

            $('#ContentPlaceHolder1_Donor_Tier').change(function () {
                var dropdownValue = $(this).val();
                handleDropDownChange(dropdownValue);
            });

            // show attributes if check box is checked
            $('#ContentPlaceHolder1_cbpaid').click(function () {
                if ($("#ContentPlaceHolder1_cbpaid").is(':checked'))
                    $("#ContentPlaceHolder1_grouping").css("display", "block");  // checked
                else
                    $("#ContentPlaceHolder1_grouping").css("display", "none");  // unchecked
            });

        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="SuccessAlert" class="alert alert-success">
        <strong>Success!</strong>
        <label id="SuccessMsg"></label>
    </div>
    <div id="FailureAlert" class="alert alert-danger">
        <strong>Unsuccessful!</strong> Please check if you selected the correct fields or notify the Administrators.
    </div>
    <label class="title" id="title" runat="server">Membership > Member Renewal</label>
    <div class="CommonHeader" id="UserRenewalHeader" runat="server" style=""></div>
    <div class="collapse" id="UserRenewal">
        <div class="SubHeader">

            <!--Start form from here -->
            <asp:HiddenField ID="individualID" runat="server" />
            <asp:HiddenField ID="individualName" runat="server" />
            <asp:HiddenField ID="memdets" runat="server" />

            <div class="form-group">
                <label for="lblDonorTier">Donor Tier:</label>
                <select runat="server" class="form-control" id='Donor_Tier' style="width: 350px; left: -1px; top: -2px;">
                    <option value="">Select a tier</option>
                    <option value="Friend of IPS">Friend of IPS</option>
                    <option value="Lifetime Friend of IPS">Lifetime Friend of IPS</option>
                    <option value="Lifetime Benefactor of IPS">Lifetime Benefactor of IPS</option>
                    <option value="Lifetime Patron of IPS">Lifetime Patron of IPS</option>
                </select>
            </div>

            <div class="form-group">
                <label for="lblExpiryDate">New Expiry Date:</label>
                <input runat="server" type="text" class="form-control" style="width: 350px;" id="datetime" />
            </div>

            <div class="form-group">
                <label for="lblpaid">Paid:</label>
                <input type="checkbox" name="cbpaid" class="form-control" style="width: 17px; height: 17px; margin-top: -3px" id="cbpaid" runat="server" value="checkboxpaid">
            </div>

            <div id="grouping" runat="server" style="display: none; margin-top: -12px;">

                <div class="form-group" style="width: 350px;">
                    <label style="margin-bottom: -10px; margin-top: 10px" for="lblMembershipFee">Membership Fee:</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text">$</span>
                        </div>
                        <input class="form-control" type="number" id="memfee" runat="server">
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblInstallment">Installment:</label>
                    <input type="checkbox" name="cbInstallment" class="form-control" style="width: 17px; height: 17px; margin-top: -3px" id="cbInstallment" runat="server"  value="cbInstallment">
                </div>

   <%--              <div class="input-group" style="display:none;width: 350px;" id="dvInstallment">
                        <label style="margin-bottom: -10px; margin-top: 10px" for="lblMembershipFee">Installment Amount:</label>               
                        <div class="input-group-prepend mb-3">
                            <span class="input-group-text">$</span>
                        </div>
                        <input class="form-control" type="number" id="txtInstallment" runat="server">
                    </div>--%>

                <div class="input-group" style="display:none;width: 350px;" id="dvInstallment">
                       <div class="form-group" style="width: 350px;">
                    <label style="margin-bottom: -10px; margin-top: 10px" for="lblMembershipFee">Installment Amount:</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text">$</span>
                        </div>
                        <input class="form-control" type="number" id="txtInstallment" runat="server">
                    </div>
                </div>
                </div>

                <div class="form-group">
                    <label for="lblPaymentMode">Payment Mode:</label>
                    <select class="form-control" id='PaymentMode' runat="server" style="width: 350px; left: -1px; top: -2px;">
                        <option value="">Select payment mode</option>
                        <option value="Cheque">Cheque</option>
                        <option value="Credit Card">Credit Card</option>
                        <option value="InterBank">InterBank</option>
                    </select>
                </div>

                <div class="form-group">
                    <label for="lblPaymentReceivedDate">Payment Received Date:</label>
                    <input runat="server" type="text" class="form-control" style="width: 350px;" id="paymentreceiveddate" />
                </div>

                <div class="form-group">
                    <label for="lblremarks">Remarks:</label>
                    <textarea id="remarks" class="form-control" name="remarks" runat="server" cols="20" rows="4"></textarea>
                </div>

            </div>


            <div class="form-group">
                <%-- type="button"--%>
                <button type="button" id="UserSubmit" class="btn btn-primary" runat="server" onserverclick="Submit_Renewal">RENEW</button>
            </div>


        </div>
    </div>

</asp:Content>
