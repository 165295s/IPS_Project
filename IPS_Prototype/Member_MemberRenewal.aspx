<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Member_MemberRenewal.aspx.cs" Inherits="IPS_Prototype.Member_MemberRenewal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script>   
        var check = 0;
            $(document).ready(function () {     

            $('[id^=detail-]').hide();
            $('.toggle').click(function () {
                $input = $(this);
                $target = $('#' + $input.attr('data-toggle'));
                $target.slideToggle();
            });

            $("#ContentPlaceHolder1_QuestionOptions").change(function () {
                if ($('#ContentPlaceHolder1_QuestionOptions option:selected').index() == 0) {
                    $('#ContentPlaceHolder1_person').css("display", "none");
                    $('#ContentPlaceHolder1_organisation').css("display", "block");
                    $("#ContentPlaceHolder1_grouping").css("display", "block");
                    check = 0;
                    $('#ContentPlaceHolder1_hidden').val("org");
                }
                else {
                    $('#ContentPlaceHolder1_person').css("display", "block");
                    $('#ContentPlaceHolder1_organisation').css("display", "none");
                    $("#ContentPlaceHolder1_grouping").css("display", "none");  // dont show ddl
                    check = 1;
                    $('#ContentPlaceHolder1_hidden').val("indi");
                }
                $("#period").change();
                $('#DonorTier').change();
                $("#SuccessAlert").css("display", "none");
            });

            $('#period').change(function () {
                // Declare variables 
                var input, filter, table, tr, td, i;
                input = document.getElementById("period");
                filter = input.innerText.toUpperCase();
                var donorTier = $('#DonorTier').val();
                table = document.getElementsByClassName("BlueTable");
                tr = $(".BlueTable").find('tr');
                // var check = 0;
                // Loop through all table rows, and hide those who don't match the search query
                if (check == 0) {
                    //CA
                    // Loop through all table rows, and hide those who don't match the search query
                    for (i = 0; i < tr.length; i++) {
                        td = tr[i].getElementsByTagName("td")[2];
                        if (td) {
                            // get the value of the date
                            console.log("check", td);
                            var value = td.innerText;
                            var date = value.substring(3, 5);

                            if (date != null) {
                                var index = $('#period option:selected').index();
                                var month = parseInt(date);
                                console.log("check month", month);
                                if ((index == 0 && month >= 1 && month <= 12) || (index == 0 && value == "NA")) {
                                    tr[i].classList.remove("none");
                                } else if (index == 1 && month >= 1 && month <= 3) {
                                    tr[i].classList.remove("none");
                                }
                                else if (index == 2 && month >= 4 && month <= 6) {
                                    tr[i].classList.remove("none");
                                } else if (index == 3 && month >= 7 && month <= 9) {
                                    tr[i].classList.remove("none");
                                } else if (index == 4 && month >= 10 && month <= 12) {
                                    tr[i].classList.remove("none");
                                } else {
                                    tr[i].classList.add("none");
                                }
                            }

                        }
                    }

                }
                else {
                    //Person
                    for (i = 0; i < tr.length; i++) {
                        td = tr[i].getElementsByTagName("td")[4];    
                        td2= tr[i].getElementsByTagName("td")[3];    
                        if (td && td2) {
                             // console.log("check", td);
                            // get the value of the date
                            var value = td.innerText;
                            var date = value.substring(3, 5);
                            if (date != null) {
                                var index = $('#period option:selected').index();
                                var month = parseInt(date);
                                console.log("date", date);
                                console.log("actual", td2.innerText);
                                 console.log("selected", donorTier);
                                var canShow = (td2.innerText.toUpperCase() == donorTier.toUpperCase()) || (donorTier== "ALL");
                                console.log("can show " + canShow);
                                if ((index == 0 && month >= 1 && month <= 12) || (index == 0 && value == "NA") && canShow) {
                                    tr[i].classList.remove("none");
                                } else if (index == 1 && month >= 1 && month <= 3 && canShow) {
                                    tr[i].classList.remove("none");
                                }
                                else if (index == 2 && month >= 4 && month <= 6 && canShow) {
                                    tr[i].classList.remove("none");
                                } else if (index == 3 && month >= 7 && month <= 9 && canShow) {
                                    tr[i].classList.remove("none");
                                } else if (index == 4 && month >= 10 && month <= 12 && canShow) {
                                    tr[i].classList.remove("none");
                                } else {
                                    tr[i].classList.add("none");
                                }
                            }
                        }
                    }
                }
            });

            $('#DonorTier').change(function () {
                // Declare variables 
                var input, filter, table, tr, td, i;
                input = document.getElementById("DonorTier");
                filter = input.innerText.toUpperCase();
                table = document.getElementsByClassName("BlueTable");
                tr = $(".BlueTable").find('tr');
                // var check = 0;
                // Loop through all table rows, and hide those who don't match the search query
                if (check == 0) {
                    //CA
                    // Loop through all table rows, and hide those who don't match the search query
                    for (i = 0; i < tr.length; i++) {
                        td = tr[i].getElementsByTagName("td")[1];
                        if (td) {
                            // get the value of the date
                            console.log("check", td);
                            var value = td.innerText;
                            console.log("check", value);

                            if (value != null) {
                                var index = $('#DonorTier option:selected').index();
                                if ((index == 0 && value != "")) {
                                    tr[i].classList.remove("none");
                                } else if (index == 1 && value == "Friend of IPS") {
                                    tr[i].classList.remove("none");
                                }
                                else if (index == 2 && value == "Lifetime Friend of IPS") {
                                    tr[i].classList.remove("none");
                                } else if (index == 3 && value == "Lifetime Benefactor of IPS") {
                                    tr[i].classList.remove("none");
                                } else if (index == 4 && value == "Lifetime Patron of IPS") {
                                    tr[i].classList.remove("none");
                                } else {
                                    tr[i].classList.add("none");
                                }
                            }

                        }
                    }

                }
                else {
                    //Person
                    for (i = 0; i < tr.length; i++) {
                        td = tr[i].getElementsByTagName("td")[3];
                        if (td) {
                            // get the value of the date
                            console.log("check", td);
                            var value = td.innerText;
                            console.log("check", value);

                            if (value != null) {
                                var index = $('#DonorTier option:selected').index();
                                if ((index == 0 && value != "")) {
                                    tr[i].classList.remove("none");
                                } else if (index == 1 && value == "Friend of IPS") {
                                    tr[i].classList.remove("none");
                                }
                                else if (index == 2 && value == "Lifetime Friend of IPS") {
                                    tr[i].classList.remove("none");
                                } else if (index == 3 && value == "Lifetime Benefactor of IPS") {
                                    tr[i].classList.remove("none");
                                } else if (index == 4 && value == "Lifetime Patron of IPS") {
                                    tr[i].classList.remove("none");
                                } else {
                                    tr[i].classList.add("none");
                                }
                            }

                        }
                    }
                  //  $("#period").change();
                }

            });
        });
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <label class="title">Membership Renewal</label>
    <br />
    <table>
        <tr>
            <td>
                <div style="margin-right: 5px" class="btn-group btn-group-inline">
                    <label style="margin-bottom:25px;" for="Type">Type:</label>
                    <select style="width: 200px; margin-top: -7px; margin-bottom:30px;" class="form-control" id="QuestionOptions" runat="server" name="D1">
                        <option value="organisation">Corporate Associate</option>
                        <option value="person">Individual Associate</option>
                    </select>
                </div>
            </td>
            <tr />
        <tr>
            <td>
                <div style="margin-right: 20px" class="btn-group btn-group-inline">
                    <label for="lblDonorTier">Donor Tier:</label>
                    <select class="form-control" name="D1" id='DonorTier' style="width: 241px; margin-top: -7px;">
                        <option value="ALL">All</option>
                        <option value="Friend of IPS">Friend of IPS</option>
                        <option value="Lifetime Friend of IPS">Lifetime Friend of IPS</option>
                        <option value="Lifetime Benefactor of IPS">Lifetime Benefactor of IPS</option>
                        <option value="Lifetime Patron of IPS">Lifetime Patron of IPS</option>
                    </select>
                </div>             
            </td>
            <td>
                   <div style="margin-right: 20px" class="btn-group btn-group-inline">
                    <label for="Periods">Period:</label>
                    <select style="width: 123px; margin-top: -7px" class="form-control" id="period" name="D1">
                        <option value="ALL">All</option>
                        <option value="JAN - MAR">Jan - Mar</option>
                        <option value="APR - JUN">Apr - Jun</option>
                        <option value="JUL - SEP">Jul - Sep</option>
                        <option value="OCT - DEC">Oct - Dec</option>
                    </select>
                </div>
            </td>
            <td>
                <div id="grouping" style="margin-left: 650px;" runat="server">                 
                    <div class="btn-group btn-group-inline">
                        <label for="lblOrg">Organisation:</label>  
                        <asp:TextBox ID="tbSearchOrg" Width="250px" style="margin-top: -7px;" CssClass="form-control" runat="server"></asp:TextBox>                    
                    </div>
                </div>
            </td>
        </tr>
    </table>

    <br />
    <br />
    <br />

    <div id="SuccessAlert" class="alert alert-success" >
        <strong>Success!</strong>
        <label id="SuccessMsg"></label>
    </div>
  
    <div runat="server" id="organisation">
        <asp:GridView CssClass="table table-hover BlueTable" ID="gvOrg" OnRowDataBound="gvOrg_RowDataBound" runat="server" OnRowEditing="gvOrg_RowEditing" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="CA" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="DONOR_TIER" HeaderText="Donor Tier" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="EXPIRY_DT" HtmlEncode="False" HeaderText="Expiry Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="ORG_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:BoundField DataField="CONTRIBUTION_STATUS" HtmlEncode="False" HeaderText="Status" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="Amount_Paid" HtmlEncode="False" HeaderText="Amount Paid" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="Times_Paid" HtmlEncode="False" HeaderText="Times Paid" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:TemplateField HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Button ID="btnTEROrg" OnClick="btnTEROrg_Click" Style="display: block; text-align: center; cursor: pointer; width: 60px !important; height: 30px !important; border: none; padding-top: 3px; background-color: #007bff; color: white; border-radius: 3px;" runat="server" Text="TER"
                            Visible='<%#bool.Parse( HasMemberPaidOrg((int)(Eval("ORG_ID")))) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField EditText="Renew" ShowEditButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
            </Columns>
        </asp:GridView>
    </div>

    <div runat="server" id="person" style="display: none;">
        <asp:GridView CssClass="table table-hover BlueTable personTable" OnRowDataBound="gvPerson_RowDataBound" Style="max-width: 100%" ID="gvPerson" class="table table-striped" DataKeyNames="PERSON_ID" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" OnRowEditing="gvPerson_RowEditing" runat="server" AutoGenerateColumns="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
            <Columns>
                <asp:BoundField DataField="PERSON_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  />
                <asp:BoundField DataField="FULLNAME" HeaderText="Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="EMAIL_ADDR" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="DONOR_TIER" HeaderText="Donor Tier" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="EXPIRY_DT" HtmlEncode="False" HeaderText="Expiry Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="CONTRIBUTION_STATUS" HtmlEncode="False" HeaderText="Status" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="CONTRIBUTION_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  />
                <asp:BoundField DataField="Amount_Paid" HtmlEncode="False" HeaderText="Amount Paid" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:BoundField DataField="Times_Paid" HtmlEncode="False" HeaderText="Times Paid" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:TemplateField HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:Button ID="btnTER" OnClick="btnTER_Click" style="display:block; text-align:center; cursor: pointer; width:60px !important; height:30px !important; border: none; padding-top:3px;background-color:#007bff;color:white; border-radius:3px;" runat="server" Text="TER" 
                Visible='<%#bool.Parse( HasMemberPaid((int)(Eval("PERSON_ID")))) %>'/>
            </ItemTemplate>
        </asp:TemplateField>
                <asp:CommandField EditText="Renew" ShowEditButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
            </Columns>
        </asp:GridView>
    </div>
    <input type="text" runat="server" id="hidden" style="display: none" /> 
</asp:Content>
