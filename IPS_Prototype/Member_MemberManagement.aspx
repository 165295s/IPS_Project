<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Member_MemberManagement.aspx.cs" Inherits="IPS_Prototype.index2" %>

<%--<%@ Register TagPrefix="UserControl" TagName="Member_DeleteInd" Src="~/Modal/Member_MemberManagementInd_Modal.ascx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <style>
            body {
		font-family: 'Varela Round', sans-serif;
	}
	.modal-confirm {		
		color: #636363;
		width: 700px;
	}
	.modal-confirm .modal-content {
		padding: 20px;
		border-radius: 5px;
		border: none;
        text-align: center;
		font-size: 14px;
	}
	.modal-confirm .modal-header {
		border-bottom: none;   
        position: relative;
        background-color: transparent;
        border:none;
	}
	.modal-confirm h4{
		text-align: center;
		font-size: 26px;
        color:inherit;
        
	}
	.modal-confirm .close {
        position: absolute;
		top: -5px;
		right: -2px;
         color: #000;
	}
	.modal-confirm .modal-body {
		color: #999;
	}
	.modal-confirm .modal-footer {
		border: none;
		text-align: center;		
		border-radius: 5px;
		font-size: 13px;
		padding: 10px 15px 25px;
	}
	.modal-confirm .modal-footer a {
		color: #999;
	}		
	.modal-confirm .icon-box {
		width: 80px;
		height: 80px;
		margin: 0 auto;
		border-radius: 50%;
		z-index: 9;
		text-align: center;
		border: 3px solid #f15e5e;
	}
	.modal-confirm .icon-box i {
		color: #f15e5e;
		font-size: 46px;
		display: inline-block;
		margin-top: 13px;
	}
    .modal-confirm .btn {
        color: #fff;
        border-radius: 4px;
		background: #60c7c1;
		text-decoration: none;
		transition: all 0.4s;
        line-height: normal;
		min-width: 120px;
        border: none;
		min-height: 40px;
		border-radius: 3px;
		margin: 0 5px;
		outline: none !important;
    }
	.modal-confirm .btn-info {
        background: #c1c1c1;
    }
    .modal-confirm .btn-info:hover, .modal-confirm .btn-info:focus {
        background: #a8a8a8;
    }
    .modal-confirm .btn-danger {
        background: #f15e5e;
    }
     .modal-confirm .btn-primary {
        background: #0094ff;
    }
         .modal-confirm .btn-primary:hover, .modal-confirm .btn-primary:focus {
        background: #0088ff;
    }
    .modal-confirm .btn-danger:hover, .modal-confirm .btn-danger:focus {
        background: #ee3535;
    }
	.trigger-btn {
		display: inline-block;
		margin: 100px auto;
	}
    </style>

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
                    //console.log("check", $('#QuestionOptions').index().toString());
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
                // $("#QuestionOptions").change();
                // Declare variables 
                var input, filter, table, tr, td, i;
                input = document.getElementById("period");
                filter = input.innerText.toUpperCase();
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
                        td = tr[i].getElementsByTagName("td")[5];
                        console.log("check", td);
                        if (td) {
                            // get the value of the date
                            var value = td.innerText;
                            var date = value.substring(3, 5);

                            if (date != null) {
                                var index = $('#period option:selected').index();
                                var month = parseInt(date);
                                console.log("check month", date);

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
                        td = tr[i].getElementsByTagName("td")[4];
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

            });

        });

        function showmodalCA() {
            $('#Add_MemberOrg').modal('show');
        };
        function modalDeleteIND() {
            $('#Member_DeleteInd').modal('show');
        };
        function modalEditIND() {
            $('#Member_EditInd').modal('show');
        };
        function modalViewIND() {
            $('#Member_ViewInd').modal('show');
        };
        

        //function showmodalIND(rowId) {
        //    //   alert(rowId);
        //    // $('.modal_name').innerText();

        //    $('.personTable tr').each(function () {
        //        if (this.rowIndex == rowId + 1) {
        //            var personname = $(this).find("td").eq(1).html();
        //            $('.modal_name').text(personname);
        //        }
        //    });
        //    $('#Add_MemberInd').modal('show');
        //    // $('#ContentPlaceHolder1_PA_GridView').show();
        //};

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="title">Membership Management</label>
    <br />
    <div>
        <div style="margin-right: 55px" class="btn-group btn-group-inline">
            <label for="Type">Type:</label>
            <select style="width: 235px; margin-top: -7px" class="form-control" id="QuestionOptions" runat="server" name="D1">
                <option value="organisation">Corporate Associate</option>
                <option value="person">Individual Associate</option>
            </select>
        </div>

        <div style="margin-right: 10px" class="btn-group btn-group-inline">
            <label for="Periods">Period:</label>
            <select style="width: 235px; margin-top: -7px" class="form-control" id="period" name="D1">
                <option value="ALL">All</option>
                <option value="JAN - MAR">Jan - Mar</option>
                <option value="APR - JUN">Apr - Jun</option>
                <option value="JUL - SEP">Jul - Sep</option>
                <option value="OCT - DEC">Oct - Dec</option>
            </select>
        </div>

        <div style="margin-right: 10px" class="btn-group btn-group-inline">
            <label for="lblDonorTier">Donor Tier:</label>
            <select class="form-control" name="D1" id='DonorTier' style="width: 235px; margin-top: -7px;">
                <option value="">All</option>
                <option value="Friend of IPS">Friend of IPS</option>
                <option value="Lifetime Friend of IPS">Lifetime Friend of IPS</option>
                <option value="Lifetime Benefactor of IPS">Lifetime Benefactor of IPS</option>
                <option value="Lifetime Patron of IPS">Lifetime Patron of IPS</option>
            </select>
        </div>


    </div>

    <div id="grouping" style="margin-left: 3px; margin-top: 20px;" runat="server">
        <div style="margin-right: 10px" class="btn-group btn-group-inline">
            <label for="lblrole">Role:</label>
            <select style="width: 235px; margin-top: -7px" class="form-control" id="role" name="D1">
                <option value="fac">Facilitator</option>
                <option value="pr">Principle Rep</option>
                <option value="rep">Representative</option>
            </select>
        </div>

        <div class="btn-group btn-group-inline">
            <label for="lblOrg">Organisation:</label>
            <select style="width: 235px; margin-top: -7px" class="form-control" id="ddlorg" name="D1">
                <option value="ALL">All</option>
                <option value="JAN - MAR">Jan - Mar</option>
                <option value="APR - JUN">Apr - Jun</option>
                <option value="JUL - SEP">Jul - Sep</option>
                <option value="OCT - DEC">Oct - Dec</option>
            </select>
        </div>

    </div>

    <br />

    <button type="button" runat="server" class="btn btn-primary right" onserverclick="btn_MemberRegistraton" id="UserAdd"><i class="fas fa-plus"></i></button>
    <br />
    <br />
    <br />

    <div id="SuccessAlert" class="alert alert-success" >
        <strong>Success!</strong>
        <label id="SuccessMsg"></label>
    </div>
<%--    <div id="FailureAlert" class="alert alert-danger">
  <strong>Unsuccessful!</strong> <label id="FailureMsg"></label>
    </div>--%>
  
    <div runat="server" id="organisation">
        <asp:GridView CssClass="table table-hover BlueTable" ID="gvOrg" OnRowDataBound="gvOrg_OnRowDataBound" OnSelectedIndexChanged="gvOrg_OnSelectedIndexChanged" runat="server" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" OnRowEditing="gvOrg_RowEditing" OnRowDeleting="gvOrg_RowDeleting" AutoGenerateColumns="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
            <Columns>
                <%-- CHRIS ADDED, PERSONID, CAREP ID AND ORG ID --%>
                <asp:BoundField DataField="PERSON_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:BoundField DataField="CA_REP_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                 <asp:BoundField DataField="ORG_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                <asp:BoundField DataField="FULLNAME_NAMETAGS" HeaderText="CA Rep" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:TemplateField HeaderText="Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Button ID="btnEditInd" OnClick="btnEditCAREP_Click" ControlStyle-CssClass="btn btn-link" ButtonType="Link" Text='<%# Eval("FULLNAME_NAMETAGS") %>' runat="server" CommandName="EditInd" CommandArgument='<%# Eval("FULLNAME_NAMETAGS") %>' ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NAME" HeaderText="CA" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="DONOR_TIER" HeaderText="Donor Tier" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="EXPIRY_DT" HtmlEncode="False" HeaderText="Expiry Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="FULLNAME_NAMETAGS" HeaderText="CA Rep" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="ROLE" HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="DESIGNATION_1" HeaderText="Designation" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="DEPARTMENT_1" HeaderText="Department" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="ORGANISATION_1" HeaderText="Organisation" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
<%--                <asp:CommandField DeleteText="TER" ShowDeleteButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White"  />
                <asp:CommandField EditText="Renew" ShowEditButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />--%>
                
                            <asp:TemplateField HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Add CAREP" ID="btnDel" class="btn btn-primary" CommandName="PA" OnClick="addCAREP" Style="height: 30px; padding-top: 3px;"></asp:Button>
                                     <asp:Button runat="server" Text="Edit ORG" ID="btnEditOrg" class="btn btn-primary" CommandName="PA" OnClick="editOrg" Style="height: 30px; padding-top: 3px;"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
<%--    OnRowDataBound="gvPerson_OnRowDataBound" OnSelectedIndexChanged="gvPerson_OnSelectedIndexChanged"--%>
     <%--ItemStyle-BackColor="#f5f3f3"--%>
    <div runat="server" id="person" style="display: none;">
        <asp:GridView CssClass="table table-hover BlueTable personTable" Style="max-width: 100%" ID="gvPerson" class="table table-striped" DataKeyNames="PERSON_ID" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" OnRowEditing="gvPerson_RowEditing" OnRowDeleting="gvPerson_RowDeleting" runat="server" AutoGenerateColumns="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
            <Columns>
                <%--    <asp:TemplateField>
                    <ItemTemplate>
                       <input type="hidden" runat="server" id="hdnId" value='<%# Eval("PERSON_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="PERSON_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  />
                <asp:BoundField DataField="FULLNAME" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ItemStyle-BackColor="#f5f3f3" HeaderText="Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" ItemStyle-ForeColor="Blue" />
                 <asp:TemplateField HeaderText="Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                     <ItemTemplate>
                         <asp:Button ID="btnEditInd" OnClick="btnEditInd_Click" ControlStyle-CssClass="btn btn-link" ButtonType="Link" Text='<%# Eval("FULLNAME") %>' runat="server" CommandName="EditInd" CommandArgument='<%# Eval("FULLNAME") %>' ToolTip="Edit" />
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:BoundField DataField="EMAIL_ADDR" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="DONOR_TIER" HeaderText="Donor Tier" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="EXPIRY_DT" HtmlEncode="False" HeaderText="Expiry Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:TemplateField HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:Button ID="btnTER" OnClick="btnTER_Click" style="display:block; text-align:center; cursor: pointer; width:60px !important; height:30px !important; border: none; padding-top:3px;background-color:#007bff;color:white; border-radius:3px;" runat="server" Text="TER" 
                Visible='<%#bool.Parse( HasMemberPaid((int)(Eval("PERSON_ID")))) %>' 
                />
            </ItemTemplate>
        </asp:TemplateField>
                <asp:CommandField EditText="Renew" ShowEditButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:TemplateField HeaderText="Actions" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <ItemTemplate>

                        <button type="button" onserverclick="PersonView_ServerClick" style="background-color: transparent; border: none; cursor: pointer; color: dodgerblue" title="View" data-toggle="tooltip" runat="server"><i class="fas fa-eye"></i></button>
                        <button type="button" onserverclick="PersonDelete_ServerClick" style="background-color: transparent; border: none; cursor: pointer; color: red" title="Delete" data-toggle="tooltip" runat="server"><i class="fas fa-trash-alt"></i></button>
                        </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>

    <input type="text" runat="server" id="hidden" style="display: none" />

    <%-- MODAL BELOW --%>
    <!-- Modal For Corporate Associate Below-->
    <div class="modal fade" id="Add_MemberOrg" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" style="max-width: 700px;" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalLongTitleOrg" style="color: white">
                        <label for="lblmodaltitlenameOrg" runat="server" id="lblmodaltitlenameOrg"></label>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label style="float: left">Are you sure you want to remove
                        <label for="lblnameOrg" runat="server" id="lblOrgname"></label>
                        ? The following will also be removed:</label>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <button type="button" class="btn btn-primary">Confirm</button>
                </div>
            </div>
        </div>
    </div>

    <div id="Modal_Container">
        <%--            <UserControl:Member_DeleteInd runat="server"/>--%>
        <!-- Modal For Individual Associate Below-->
        <div class="modal fade" id="Member_DeleteInd" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 700px;" role="document">
                <div class="modal-content">
                   <%-- <div class="modal-header">
                        <h5 class="modal-title" id="ModalLongTitleInd" style="color: white">Individual Associate:
                            <label for="lblmodaltitlenameInd" runat="server" id="lblmodaltitlenameInd"></label>
                        </h5>
<%--                            <h5 class="modal-title" id="ModalLongTitleInd" style="color:white">Individual Associate: <label for="lblmodaltitlenameInd" class="modal_name" runat="server" id="lblmodaltitlenameInd" ></label></h5>--%>

                      <%--   <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>--%>

                    <div class="modal-header">
                        <div class="icon-box">
                            <i class="material-icons">&#xE5CD;</i>
                        </div>         
                        <h4 class="modal-title" style="margin-top:85px;margin-left:233px;">Are you sure?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>

                    <div class="modal-body" style="margin-top:10px;">
<%--                        <label style="float: left; margin-top: 10px; margin-right: auto;">**Note: All details above will be deleted once you click remove.</label>--%>
                                             <p>If you delete <label for="lblmodaltitlenameInd" runat="server" id="lblmodaltitlenameInd"></label>, all information below will also be deleted:</p>
                             <div class="container">
                            <div class="panel panel-default">
                                <ul class="list-group">
                                    <li class="list-group-item">
                                        <div class="row toggle" id="dropdown-detail-2" data-toggle="detail-2">
                                            <div class="col-xs-10">
                                                Personal Assistant
                                            </div>
                                            <div class="col-xs-2"><i style="padding-left: 450px" class="fa fa-chevron-down pull-right"></i></div>
                                        </div>
                                        <div id="detail-2">
                                            <hr />
                                            <div class="container">
                                                <div class="fluid-row">
                                                    <asp:HiddenField ID="hdnPersonToDelete" runat="server" />
                                                    <asp:Repeater ID="rptrIAdets" runat="server">
                                                        <HeaderTemplate>

                                                            <table class="table">
                                                                <thead>

                                                                    <% if (rptrIAdets.Items.Count < 1)
                                                                        { %>
                                                                    <tr>
                                                                        <th>No Personal Assistant</th>
                                                                    </tr>
                                                                    <% }
                                                                    else
                                                                    { %>
                                                                    <tr>
                                                                        <th>Honorific</th>
                                                                        <th>Name</th>
                                                                        <th>Email</th>
                                                                        <th>Telephone</th>
                                                                    </tr>
                                                                    <% } %>
                                                                </thead>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("HONORIFIC") %></td>
                                                                <td><%# Eval("FULLNAME") %></td>
                                                                <td><%#Eval("EMAIL_ADDR") %></td>
                                                                <td><%#Eval("TEL_NUM") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>


                                                <asp:GridView runat="server" EnableViewState="false" ID="PA_GridView" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
                                                    <Columns>
                                                        <asp:BoundField DataField="HONORIFIC" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                                        <asp:BoundField DataField="FULLNAME" HeaderText="Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                                        <asp:BoundField DataField="EMAIL_ADDR" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                                        <asp:BoundField DataField="TEL_NUM" HeaderText="Telephone" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" style="margin-right:189px" class="btn btn-danger" runat="server" ID="btnDeleteInd" onserverclick="btnDeleteInd_ServerClick">Remove</button>
                    </div>
                </div>
            </div>
        </div>
        </div>

        <!-- Modal to EDIT Individual Associate Below-->
        <div class="modal fade" id="Member_EditInd" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 700px;" role="document">
                <div class="modal-content">
                    <div class="modal-header">     
                        <h4 class="modal-title center" style="margin-left:290px">Edit</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
<%--                    <label for="lblmodalnameInd" runat="server" id="lblmodalnameInd"></label>--%>
                    <div class="modal-body" style="float:left; margin-top:10px;">
                           <asp:HiddenField ID="hdnPersonEdit" runat="server" />

                        <h5 style="margin-left:-400px">Personal Details</h5>
                        <hr />

                        <div class="btn-group btn-group-inline">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndHonorific" runat="server" Text="Honorific:"></asp:Label>
                                    <asp:DropDownList ID="IndDdlHonorific" runat="server" CssClass="form-control" Height="65%"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndSalutation" runat="server" Text="Salutation:"></asp:Label>
                                    <asp:TextBox ID="IndTbSalutation" CssClass="form-control" runat="server" Height="65%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="TelephoneNo" runat="server" Text="Telephone:"></asp:Label>                                    
                                    <asp:TextBox ID="IndTbTelephone" CssClass="form-control" runat="server" Height="65%"></asp:TextBox>                    
                                </div>
                            </div>
                        </div>

                        <div class="btn-group btn-group-inline">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndGivenName" runat="server" Text="Given Name:"></asp:Label>
                                    <asp:TextBox ID="IndTbGivenName" CssClass="form-control" runat="server" Height="65%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndSurname" runat="server" Text="Surname:"></asp:Label>
                                    <asp:TextBox ID="IndTbSirname" CssClass="form-control" runat="server" Height="65%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndfullNameNT" runat="server" Text="Full Name Nametag:"></asp:Label>
                                    <asp:TextBox ID="IndTbfullNameNT" CssClass="form-control" runat="server" Height="65%"></asp:TextBox>
                                </div>
                            </div>
                            </div>

                            <div class="btn-group btn-group-inline">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="IndEmail" runat="server" Text="Email:"></asp:Label>
                                        <asp:TextBox ID="IndTbEmail" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndNationality" runat="server" Text="Nationality:"></asp:Label>
                                    <asp:DropDownList ID="IndDdlNationality" runat="server" style="width: 180px" Height="65%" CssClass="form-control">
                                        <asp:ListItem Text="Singapore" Value="SGP"></asp:ListItem>
                                        <asp:ListItem Text="Malaysia" Value="MYS"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="IndGender" runat="server" Text="Gender:"></asp:Label>
                                        <asp:DropDownList ID="IndDdlGender" runat="server" Style="width: 180px" Height="65%" CssClass="form-control">
                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                           <h5 style="margin-left:-400px; margin-top:10px;">Additional Details</h5>
                        <hr />
                               <div class="btn-group btn-group-inline">
                                   <div class="col-md-4">
                                       <div class="form-group">
                                           <asp:Label ID="IndSDR" runat="server" Text="SDR:"></asp:Label>
                                           <asp:TextBox ID="IndTbSDR" CssClass="form-control" runat="server" Height="65%"></asp:TextBox>
                                       </div>
                                   </div>
                     
                              <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndSource" runat="server" Text="Source:(Ms Wang)"></asp:Label>
                                    <asp:TextBox ID="IndTbSource" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                                   <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="IndCat" runat="server" Text="Cat:(Ms Wang)"></asp:Label>
                                    <asp:TextBox ID="IndTbCat" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                               <div class="btn-group btn-group-inline">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Indorganization1" runat="server" Text="Organization 1:"></asp:Label>
                                    <asp:TextBox ID="IndTbOrg1" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Inddepartment1" runat="server" Text="Department 1:"></asp:Label>
                                    <asp:TextBox ID="IndTbDep1" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                              <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Inddesignation1" runat="server" Text="Designation 1:"></asp:Label>
                                    <asp:TextBox ID="IndTbDes1" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                               <div class="btn-group btn-group-inline">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Indorganization2" runat="server" Text="Organization 2:"></asp:Label>
                                    <asp:TextBox ID="IndTbOrg2" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Inddepartment2" runat="server" Text="Department 2:"></asp:Label>
                                    <asp:TextBox ID="IndTbDep2" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                              <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="designation2" runat="server" Text="Designation 2:"></asp:Label>
                                    <asp:TextBox ID="IndTbDes2" CssClass="form-control" Height="65%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

               

              </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" style="margin-right:189px" class="btn btn-primary" runat="server" ID="PersonSave" onserverclick="PersonSave_ServerClick">Save</button>
                    </div>

                </div>
                </div>
            </div>

            <!-- Modal to EDIT Individual Associate Below-->
        <div class="modal fade" id="Member_ViewInd" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 700px;" role="document">
                <div class="modal-content">
                    <div class="modal-header">     
                        <h4 class="modal-title center" style="margin-left:290px">View</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body" style="float:left; margin-top:10px;">
                         
                        <h5 style="margin-left:-400px">Personal Details</h5>
                        <hr />

              </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" style="margin-right:189px" class="btn btn-primary" runat="server" ID="Button1" onserverclick="PersonSave_ServerClick">Save</button>
                    </div>

                </div>
                </div>
            </div>

</asp:Content>
