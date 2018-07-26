<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Membership_Registration_CorperateAssociateRepresentative.aspx.cs" Inherits="IPS_Prototype.Membership_Registration_CorperateAssociateRepresentative" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap2-toggle.min.css" rel="stylesheet" />
    <style>
        .toggle.ios, .toggle-on.ios, .toggle-off.ios {
            border-radius: 20px;
        }

            .toggle.ios .toggle-handle {
                border-radius: 20px;
            }
        /*.toggle.ios{display:none;}*/
    </style>
    <script>

        $(document).ready(function () {

            changeChkbx();
            SearchText();


            $('#ContentPlaceHolder1_sliderToggle').change(function () {
                if ($('#ContentPlaceHolder1_sliderToggle').is(':checked')) {
                    $('#ContentPlaceHolder1_txtFirstName').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtSalutationField').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtSurname').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtFullNameNameTag').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtTelephone').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtOrg1').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtDept1').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtDesig1').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtOrg2').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtDept2').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtDesig2').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtSDR').removeAttr('disabled');
                    $('#ContentPlaceHolder1_ddlList').removeAttr('disabled');
                    $('#ContentPlaceHolder1_ddlNationality').removeAttr('disabled');
                    $('#ContentPlaceHolder1_ddlSource').removeAttr('disabled');
                    $('#ContentPlaceHolder1_ddlCat1').removeAttr('disabled');
                    $('#ContentPlaceHolder1_ddlCat2').removeAttr('disabled');
                    $('#ContentPlaceHolder1_ddlStatus').removeAttr('disabled');
                    $('#ContentPlaceHolder1_ddlRole').removeAttr('disabled');
                    $('#ContentPlaceHolder1_txtEmail').removeAttr('disabled');
                    $('#ContentPlaceHolder1_btnSave').removeAttr('disabled');
                    $('#ContentPlaceHolder1_btnDelCAREP').removeAttr('disabled');
                    $('#ContentPlaceHolder1_btnSave').remove();
                    $('#ContentPlaceHolder1_btnUpdate').show();
                    $('#ContentPlaceHolder1_btnUpdate').removeAttr('disabled');
                    $('#ContentPlaceHolder1_FacilitatorBriefed').prop('disabled', false);
                    $('#ContentPlaceHolder1_welcomeEmail').prop('disabled', false);

                } else {
                    $('#ContentPlaceHolder1_txtFirstName').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtSalutationField').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtSurname').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtFullNameNameTag').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtTelephone').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtOrg1').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtDept1').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtDesig1').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtOrg2').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtDept2').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtDesig2').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtSDR').attr("disabled", true);
                    $('#ContentPlaceHolder1_ddlList').attr("disabled", true);
                    $('#ContentPlaceHolder1_ddlNationality').attr("disabled", true);
                    $('#ContentPlaceHolder1_ddlSource').attr("disabled", true);
                    $('#ContentPlaceHolder1_ddlCat1').attr("disabled", true);
                    $('#ContentPlaceHolder1_ddlCat2').attr("disabled", true);
                    $('#ContentPlaceHolder1_ddlStatus').attr("disabled", true);
                    $('#ContentPlaceHolder1_ddlRole').attr("disabled", true);
                    $('#ContentPlaceHolder1_txtEmail').attr("disabled", true);
                    $('#ContentPlaceHolder1_btnSave').attr("disabled", true);
                    $('#ContentPlaceHolder1_btnDelCAREP').attr("disabled", true);
                    $('#ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                    $('#ContentPlaceHolder1_FacilitatorBriefed').prop('disabled', true);
                    $('#ContentPlaceHolder1_welcomeEmail').prop('disabled', true);
                }

            });
            //displaySlider();
            $("#AddPA").click(function () {

                $("#associateName").text(": " + $("#ContentPlaceHolder1_txtFirstName").val() + $("#ContentPlaceHolder1_txtLastName").val());
                $("#associateType").text($("#ContentPlaceHolder1_hiddentext").val());


            });

            function changeChkbx() {
                $('#ContentPlaceHolder1_ddlRole').change(function () {
                    if ($('#ContentPlaceHolder1_ddlRole option:selected').index() != 0) {

                        console.log("check", $('#ContentPlaceHolder1_ddlRole option:selected').index());
                        $('#ContentPlaceHolder1_chkbxWelcomeEmail').css('display', 'block');
                        $('#ContentPlaceHolder1_chkbxFaciBriefed').css('display', 'none');
                        $('#ContentPlaceHolder1_FacilitatorBriefed').prop('checked', false);
                        $('#ContentPlaceHolder1_welcomeEmail').prop('checked', false);
                    }
                    else {

                        console.log("check", $('#ContentPlaceHolder1_ddlRole option:selected').index());
                        $('#ContentPlaceHolder1_chkbxWelcomeEmail').css('display', 'none');
                        $('#ContentPlaceHolder1_chkbxFaciBriefed').css('display', 'block');
                        $('#ContentPlaceHolder1_FacilitatorBriefed').prop('checked', false);
                        $('#ContentPlaceHolder1_welcomeEmail').prop('checked', false);
                    }
                });
            }



        });
        function showmodal() {

            $('#Add_PA').modal();
            $("#ContentPlaceHolder1_hiddentextPA_ID").val("");
            $("#ContentPlaceHolder1_modalDDList").prop('selectedIndex', 0);
            $("#ContentPlaceHolder1_modalFName").val("");
            $("#ContentPlaceHolder1_modalSname").val("");
            $("#ContentPlaceHolder1_modalEmail").val("");
            $("#ContentPlaceHolder1_modalTelNo").val("");
            $('#ContentPlaceHolder1_submitPA').css('display', 'block')
            $('#ContentPlaceHolder1_updatePA').css('display', 'none');

            $('#ContentPlaceHolder1_PA_GridView').show();

        };
        function showmodalAgain() {
            //btnUpdate
            // $("#ContentPlaceHolder1_PA_GridView_btnUpdate").on('click',function (event) {

            //                $('#Add_PA').modal('hide');

            //$('#Add_PA').modal('show');
            //      $('#ContentPlaceHolder1_PA_GridView').show();

            //     return false;
            //  });
            $('#Add_PA').modal('show');
            $('#modalTitle').empty();
            $('#modalTitle').append("Update PA");
            $('#ContentPlaceHolder1_submitPA').css('display', 'none')

            $('#ContentPlaceHolder1_PA_GridView').show();

        };
        function showPAModalError(msg) {
            $('#Add_PA').modal('show');
            $('#ModalFailureAlert').css('display', 'block');
            $('#ModalFailureMsg').text(msg);

        }
        function displayFailureMsg(msg) {

            $('#FailureAlert').css('display', 'block');
            $('#FailureMsg').text(msg);

        };
        function offSlider() {
            $('#ContentPlaceHolder1_sliderToggle').bootstrapToggle('off');

        }



        function SearchText() {
            $.ajax({
                url: "Autocomplete_CAREP.asmx/GetAutoCompleteData",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ 'txt' : '" + $("#ContentPlaceHolder1_txtFirstName").val() + "'}",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    //var projects = data.d + ";";
                    $("#ContentPlaceHolder1_txtFirstName").autocomplete({
                        minLength: 1,
                        source: function (request, response) {
                            //data :: JSON list defined
                            response($.map(data.d, function (value, key) {
                                return {
                                    fName: value.firstName,
                                    //value: value.firstName,
                                    email: value.email,
                                    emailAddr: value.email,
                                    surname: value.surname,
                                    salutation: value.salutation,
                                    fullnameNT: value.fullNameNametag,
                                    nationality: value.nationality,
                                    gender: value.gender,
                                    honorific: value.honorific,
                                    role: value.role,
                                    telephone: value.telNum,
                                    org1: value.organisation1,
                                    dept1: value.department1,
                                    desig1: value.designation1,
                                    org2: value.organisation2,
                                    dept2: value.department2,
                                    desig2: value.designation2,
                                    sdr: value.SDR,
                                    src: value.source,
                                    cat1: value.cat1,
                                    cat2: value.cat2,
                                    personStatus: value.status,
                                    emailSentVal: value.emailSent,
                                    faciBriefedVal: value.faciBriefed
                                }
                            }));
                        },
                        //focus: function (event, ui) {
                        //    event.preventDefault();
                        //    $("#ContentPlaceHolder1_txtFullName").val(ui.item.fName);
                        //    $('#ContentPlaceHolder1_txtFirstName').val(ui.item.email);
                        //    $('#ContentPlaceHolder1_txtSurname').val(ui.item.surname);
                        //   $('#ContentPlaceHolder1_txtSalutationField').val(ui.item.salutation);
                        //   $("#ContentPlaceHolder1_txtFullNameNameTag").val(ui.item.fullnameNT);


                        //       return false;
                        //   },
                        select: function (event, ui) {
                            //event.preventDefault();
                            //$("#ContentPlaceHolder1_txtFullName").val(ui.item.fName);
                            $('#ContentPlaceHolder1_txtFirstName').val(ui.item.fName);
                            $('#ContentPlaceHolder1_txtSurname').val(ui.item.surname);
                            $("#ContentPlaceHolder1_txtFullNameNameTag").val(ui.item.fullnameNT);
                            $('#ContentPlaceHolder1_txtEmail').val(ui.item.email);
                            $('#ContentPlaceHolder1_txtSalutationField').val(ui.item.salutation);
                            $("#ContentPlaceHolder1_txtTelephone").val(ui.item.telephone);
                            $('#ContentPlaceHolder1_txtOrg1').val(ui.item.org1);
                            $('#ContentPlaceHolder1_txtDept1').val(ui.item.dept1);
                            $('#ContentPlaceHolder1_txtDesig1').val(ui.item.desig1);
                            $('#ContentPlaceHolder1_txtOrg2').val(ui.item.org2);
                            $('#ContentPlaceHolder1_txtDept2').val(ui.item.dept2);
                            $('#ContentPlaceHolder1_txtDesig2').val(ui.item.desig2);
                            $('#ContentPlaceHolder1_txtSDR').val(ui.item.sdr);
                            $('#ContentPlaceHolder1_ddlStatus').val(ui.item.personStatus);
                            $('#ContentPlaceHolder1_ddlSource').val(ui.item.src);
                            $('#ContentPlaceHolder1_ddlCat1').val(ui.item.cat1);
                            $('#ContentPlaceHolder1_ddlCat2').val(ui.item.cat2);
                            $('#ContentPlaceHolder1_ddlRole').val(ui.item.role);
                            //$('.ios').css('display','block');
                            if (ui.item.role != "FACILITATOR") {
                                $('#chkbxWelcomeEmail').css("display", "block");
                                $('#chkbxFaciBriefed').css("display", "none");
                                $('#ContentPlaceHolder1_FacilitatorBriefed').prop('checked', false);
                                $('#ContentPlaceHolder1_welcomeEmail').prop('checked', false);
                                if (ui.item.emailSentVal != "No") {
                                    $('#ContentPlaceHolder1_FacilitatorBriefed').prop('checked', true)
                                }
                                if (ui.item.faciBriefedVal != "NO") {
                                    $('#ContentPlaceHolder1_welcomeEmail').prop('checked', true)


                                }


                            }
                            else {
                                $('#chkbxWelcomeEmail').css("display", "none");
                                $('#chkbxFaciBriefed').css("display", "block");
                                $('#ContentPlaceHolder1_FacilitatorBriefed').prop('checked', false);
                                $('#ContentPlaceHolder1_welcomeEmail').prop('checked', false);

                            }

                            if (ui.item.gender === "M") {
                                $('input:radio[value=M]').prop("checked", true);


                            }
                            else {

                                $('input:radio[value=F]').prop("checked", true);
                            }

                            return false;
                        }

                    })
                        .autocomplete("instance")._renderItem = function (ul, item) {
                            //alert(item);
                            return $("<li>")
                                .append("<div>" + item.fName + " " + item.surname + "<br>" + item.email + "</div>")
                                .appendTo(ul);

                        };


                },
                error: function (result) {
                    alert("Error");
                }
            });

        }
        function modalDeleteIND() {
            $('#Member_DeleteInd').modal('show');

        };
        function hideToggle() {
            $("#ContentPlaceHolder1_slidertoggleDIV").css('display', 'none');
            $("#ContentPlaceHolder1_btnDelCAREP").css('display', 'none');
            //$("#ContentPlaceHolder1_AddPA").css('display', 'none');


        }
        function showToggle() {
            $("#ContentPlaceHolder1_slidertoggleDIV").css('display', 'block');
            $("#ContentPlaceHolder1_btnDel").css('display', 'none');

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div id="SuccessAlert" class="alert alert-success">
        <strong>Success!</strong>
        <label id="SuccessMsg"></label>
    </div>
    <div id="FailureAlert" class="alert alert-danger">
        <strong>Unsuccessful!</strong>
        <label id="FailureMsg"></label>
    </div>
    <label class="title" id="title" runat="server">Membership > Member Registration</label>

    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Corporate Associate Representatives</div>
    <div class="SubHeader" style="overflow: auto;">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>



        <div class="left_div" style="float: left; margin-top: 2px;">


            <div class="form-group">
                <label for="organisationName" id="lblOrgName" runat="server" style="font-weight: bold; font-size: 1.5em;"></label>
            </div>
            <div class="form-group" id="slidertoggleDIV" runat="server">

                <input id="sliderToggle" enableviewstate="true" type="checkbox" runat="server" data-toggle="toggle" data-width="150" data-height="40" data-style="ios" data-offstyle="danger" data-onstyle="success" data-on="Edit Mode On" data-off="Edit Mode Off">
            </div>
            <label for="Honorific">Honourific</label><label style="color: red;">*</label>
            <div class="form-group">
                <asp:DropDownList ID="ddlList" runat="server" Class="ddlStyle"></asp:DropDownList>
            </div>

            <label for="Saluation">Saluation</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtSalutationField" autocomplete="off" style="width: 150px;" />
            </div>

            <%--   <label for="fullName">Full Name:</label>
        <div class="form-group">
            <input type="text" onkeyup="SearchText()" class="form-control" runat="server" id="txtFullName" autocomplete="off" style="width: 350px;" />
        </div>--%>


            <label for="firstName">First Name:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" onkeyup="SearchText()" runat="server" autocomplete="off" id="txtFirstName" style="width: 350px;" />
            </div>

            <label for="surname">Surname:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtSurname" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="fullNameNT">Full Name Nametag:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtFullNameNameTag" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="Nationality">Nationality</label><label style="color: red;">*</label>
            <div class="form-group">
                <asp:DropDownList ID="ddlNationality" runat="server" Class="ddlStyle"></asp:DropDownList>
            </div>

            <label for="Gender">Gender:</label><label style="color: red;">*</label>
            <div class="form-group">
                <div class="form-check form-check-inline">
                    <label class="radio-inline">
                        <input type="radio" name="optradio" id="Male" runat="server" value="M">Male</label>

                </div>
                <div class="form-check form-check-inline">
                    <label class="radio-inline">
                        <input type="radio" name="optradio" id="Female" runat="server" value="F">Female</label>

                </div>
            </div>
            <label for="Email">E-Mail Address:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtEmail" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="Status">Status:</label><label style="color: red;">*</label>
            <div class="form-group">
                <asp:DropDownList ID="ddlStatus" runat="server" Class="ddlStyle">
                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                    <asp:ListItem Text="Retired" Value="Retired"></asp:ListItem>


                </asp:DropDownList>
            </div>


            <label for="Role">Role:</label><label style="color: red;">*</label>
            <div class="form-group">
                <asp:DropDownList ID="ddlRole" runat="server" Class="ddlStyle"></asp:DropDownList>
            </div>





            <%--        
        <div class="form-group">
                <button type="button" id="AddPA" data-toggle="modal" data-target="#Add_PA"  class="btn btn-primary" style="width: 150px; height: 40px;">
                    Add PA
                </button>

            </div>--%>
            <div class="form-group" style="margin-top: 50px;">
                <button type="button" runat="server" id="btnUpdate" class="btn btn-primary" onserverclick="updateCAREP" style="width: 150px; height: 40px; float: left; display: none;">
                    Update
                </button>
                <%-- <button type="button" id="AddPA" runat="server" data-toggle="modal" data-target="#Add_PA" class="btn btn-primary" style="width: 150px; height: 40px;">
                            Add PA
                        </button>--%>
                <button type="button" runat="server" id="btnSave" class="btn btn-primary" onserverclick="button_save" style="width: 150px; height: 40px; float: left;">
                    Save
                </button>
                <button type="button" id="btnDelCAREP" runat="server" class="btn btn-primary" onserverclick="deleteCAREP" style="width: 150px; height: 40px; float: right;">
                    Delete CAREP
                </button>

            </div>

            <%--                     <div class="form-group">
                         <button type="button" id="btnDelCAREP"  class="btn btn-primary" style="width: 150px; height: 40px; float:right;">
                             Delete
                         </button>

                     </div>--%>
        </div>



        <div class="right_div" style="float: left; margin-top: 40px; margin-left: 55px;">

            <input type="text" id="hiddentext" runat="server" class="none" />


            <label for="Source">Source:</label><label style="color: red;">*</label>
            <div class="form-group">
                <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true" Class="ddlStyle" OnSelectedIndexChanged="getCat1"></asp:DropDownList>
            </div>

            <label for="Cat1">Cat 1:</label><label style="color: red;">*</label>
            <div class="form-group">
                <asp:DropDownList ID="ddlCat1" runat="server" Class="ddlStyle"></asp:DropDownList>
            </div>
            <label for="Cat2">Cat 2:</label><label style="color: red;">*</label>
            <div class="form-group">
                <asp:DropDownList ID="ddlCat2" runat="server" Class="ddlStyle"></asp:DropDownList>
            </div>




            <label for="TelephoneNo">Telephone Number:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtTelephone" autocomplete="off" style="width: 350px;" />
            </div>


            <label for="organization1">Organization 1:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtOrg1" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="department1">Department 1:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtDept1" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="designation1">Designation 1:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtDesig1" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="organization2">Organization 2:</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtOrg2" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="department2">Department 2:</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtDept2" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="designation2">Designation 2:</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtDesig2" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="SDR">Special Dietary Restriction:</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtSDR" autocomplete="off" style="width: 350px;" />
            </div>

            <div class="form-check" id="chkbxFaciBriefed" runat="server">
                <input class="form-check-input" type="checkbox" id="FacilitatorBriefed" runat="server" value="Yes">
                <label class="form-check-label" for="chkbxFaciBriefed">
                    Facilitator Briefed
                </label>
            </div>
            <div class="form-check" id="chkbxWelcomeEmail" runat="server" style="display: none;">
                <input class="form-check-input" type="checkbox" id="welcomeEmail" runat="server" value="Yes">
                <label class="form-check-label" for="chkbxWelcomeEmail">
                    Welcome email sent
                </label>
            </div>
            <%--            <div class="form-group">
            <button type="button" runat="server"  class="btn btn-primary" style="width: 150px; height: 40px;">
                NEXT
                      <i class="fas fa-arrow-right"></i>
            </button>


        </div>--%>
        </div>

        <%-- GRIDVIEW BELOW --%>
        <div id="MemberContent">
            <%--<asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <%--            <label for="UserTable" style="margin-top:1025px;margin-left:-752px;font-weight:bold;">Coporate Associate Representatives:</label>--%>
            <asp:GridView runat="server" EnableViewState="false" ID="UserTable" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
                <Columns>

                    <asp:BoundField DataField="Person_Id" HeaderText="Person ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                    <asp:BoundField DataField="CA_Rep_id" HeaderText="CA_Rep_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                    <asp:BoundField DataField="Org_Id" HeaderText="ORG_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                    <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="First_Name" HeaderText="First Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="Surname" HeaderText="Surname" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />

                    <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="Nationality" HeaderText="Nationality" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="Role" HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                    <asp:BoundField HeaderText="" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                    <asp:BoundField HeaderText="" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                    <asp:TemplateField HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                        <ItemTemplate>
                            <asp:TextBox ID="hiddentext" runat="server" class="none" Text='<%# Eval("Person_Id") %>' />

                            <asp:Button runat="server" Text="PA" ID="AddPA" class="btn btn-primary" CommandName="PA" OnClick="Add_Pa_Command" Style="width: 60px; height: 30px; padding-top: 3px;"></asp:Button>
                            <input type="text" id="Text1" runat="server" class="none" />
                            <asp:Button runat="server" Text="Delete" ID="btnDel" class="btn btn-primary" CommandName="PA" OnClick="deleteCAREP" Style="height: 30px; padding-top: 3px;"></asp:Button>
                            <input type="text" id="Text2" runat="server" class="none" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--                    <asp:CommandField DeleteText="Delete"  ShowDeleteButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />--%>
                </Columns>


            </asp:GridView>
            <%--                    </ContentTemplate>
    </asp:UpdatePanel>--%>
        </div>

    </div>


    <%-- MODAL BELOW --%>


    <div id="Modal_Container">
        <%--<UserControl:AddPA runat="server"/>--%>

        <div id="Add_PA" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 1085px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 id="modalTitle" class="modal-title">Add PA</h4>
                        <button type="button" class="close" data-dismiss="modal"><i class="far fa-times-circle"></i></button>
                    </div>
                    <div class="modal-body1" style="padding-top: 20px; padding-left: 50px; padding-right: 50px; padding-bottom: 10px; overflow-y: scroll">
                        <!--Content goes here -->


                        <div class="form-group">
                            <label for="personId" runat="server" id="pID" class="none"></label>
                            <label for="caID" runat="server" id="caID" class="none"></label>
                            <label for="orgID" runat="server" id="orgID" class="none"></label>
                            <input type="text" id="hiddentextPA_ID" runat="server" class="none" />
                        </div>
                        <div>
                             <div id="ModalFailureAlert" class="alert alert-danger">
                            <strong>Unsuccessful!</strong>
                            <label id="ModalFailureMsg"></label>
                        </div>
                            <div style="float: left; margin-top: -5px;">
                                <label for="Honorific">Honourific</label><label style="color: red;">*</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="modalDDList" runat="server" Class="ddlStyle"></asp:DropDownList>
                                </div>



                                <label for="firstName">First Name:</label><label style="color: red;">*</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="modalFName" autocomplete="off" style="width: 350px;" />
                                </div>


                                <label for="surname">Surname:</label><label style="color: red;">*</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="modalSname" autocomplete="off" style="width: 350px;" />
                                </div>


                                <div class="form-group">
                                    <label for="Email">E-Mail Address:</label><label style="color: red;">*</label>
                                    <div class="form-group">
                                        <input type="text" class="form-control" runat="server" id="modalEmail" autocomplete="off" style="width: 350px;" />
                                    </div>

                                    <label for="TelephoneNo">Telephone Number:</label><label style="color: red;">*</label>
                                    <div class="form-group">
                                        <input type="text" class="form-control" runat="server" id="modalTelNo" autocomplete="off" style="width: 350px;" />

                                    </div>


                                </div>

                                <button type="button" id="submitPA" onserverclick="button_saveCAREP_PA" runat="server" class="btn btn-primary" style="width: 150px; height: 40px;">Save</button>
                                <button type="button" id="updatePA" runat="server" class="btn btn-primary" onserverclick="updatePA_ServerClick" style="width: 150px; height: 40px;">Update</button>
                            </div>

                        </div>



                        <asp:GridView runat="server" ID="PA_GridView" DataKeyNames="PA_ID" ShowHeaderWhenEmpty="true" OnRowDeleting="RowDeleting" EmptyDataText="No Records Found" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
                            <Columns>

                                <%--             <asp:BoundField DataField="Person_Id" HeaderText=""  HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:BoundField DataField="CA_Rep_id" HeaderText="" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:BoundField DataField="Org_Id" HeaderText=""  HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />--%>

                                <asp:BoundField DataField="PA_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  />
                                <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                <asp:BoundField DataField="First_Name" HeaderText="First Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                <asp:BoundField DataField="Surname" HeaderText="Surname" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                <asp:BoundField DataField="Telephone" HeaderText="Telephone" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />

                                <%-- <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:BoundField DataField="Nationality" HeaderText="Nationality" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:BoundField DataField="Role" HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />--%>
                                <%--                <asp:BoundField HeaderText="" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                <asp:BoundField HeaderText="" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />--%>

                                <asp:TemplateField HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">

                                    <ItemTemplate>
                                        <%--                <asp:TextBox ID="hiddentext" runat="server" class="none" Text='<%# Eval("Person_Id") %>' />--%>

                                        <%--   <asp:Button runat="server" Text="PA" ID="AddPA" class="btn btn-primary" CommandName="PA" OnClick="Add_Pa_Command" Style="width: 150px; height: 40px;">
                            
                </asp:Button>--%>
                                        <%--<asp:Button type="button"  ID="btnUpdate" class="btn btn-primary" CommandName="PA" CausesValidation="false" runat="server" OnClick="RowEditing" Style="height: 30px; padding-top: 3px;" Text="update pa"></asp:Button>
                 <input type="text" id="Text3" runat="server" class="none" />--%>

                                        <%--<asp:Button ID="LinkButton1" runat="server" CommandName="PA" OnClick="Editing">Update</asp:Button>--%>
                                        <asp:Button runat="server" Text="Update PA" ID="AddPA" class="btn btn-primary" CommandName="Update" OnClick="Editing" Style="width: 100px; height: 30px; padding-top: 3px;"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />

                            </Columns>
                        </asp:GridView>








                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="Modal_Container_DELETE">
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
                        <h4 class="modal-title" style="margin-top: 85px; margin-left: 233px;">Are you sure?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>

                    <div class="modal-body" style="margin-top: 10px;">
                        <%--                        <label style="float: left; margin-top: 10px; margin-right: auto;">**Note: All details above will be deleted once you click remove.</label>--%>
                        <p>
                            If you delete
                            <label for="lblmodaltitlenameInd" runat="server" id="lblmodaltitlenameInd"></label>
                            , all information below will also be deleted:
                        </p>
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


                                                <asp:GridView runat="server" EnableViewState="false" ID="GridView1" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
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
                        <button type="button" style="margin-right: 189px" class="btn btn-danger" runat="server" id="btnDeleteInd" onserverclick="btnDeleteCAREP_ServerClick">Remove</button>
                    </div>
                </div>
            </div>
        </div>
    </div>








</asp:Content>
