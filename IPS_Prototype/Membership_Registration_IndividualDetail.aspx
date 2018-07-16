<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Membership_Registration_IndividualDetail.aspx.cs" Inherits="IPS_Prototype.Membership_Registration_IndividualDetail" %>

<%@ Register TagPrefix="UserControl" TagName="AddPA" Src="~/Modal/AddPa_Modal.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
           
            $('#UserRegisterHeader > .fas').toggleClass('active');
            $('#UserRegister').collapse();

        });

        $(document).ready(function () {
            SearchText();
            $("#AddPA").click(function () {
                $("#associateName").text(": " + $("#ContentPlaceHolder1_txtFullName").val());
                $("#associateType").text($("#ContentPlaceHolder1_hiddentext").val());
            });
        });



        function displayFailureMsg(msg) {

            $('#FailureAlert').css('display', 'block');
            $('#FailureMsg').text(msg);

        };

        function SearchText() {
            $.ajax({
                url: "Autocomplete_CAREP.asmx/GetAutoCompleteDataIndiv",
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
                                    personStatus: value.status
                                  
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
                            $('#ContentPlaceHolder1_ddlNationality').val(ui.item.nationality);
                            $('#ContentPlaceHolder1_ddlStatus').val(ui.item.personStatus);
                            $('#ContentPlaceHolder1_ddlSource').val(ui.item.src);
                            $('#ContentPlaceHolder1_ddlCat1').val(ui.item.cat1);
                            $('#ContentPlaceHolder1_ddlCat2').val(ui.item.cat2);
                           
                            if (ui.item.gender === "M") {
                                 $('input:radio[value=M]').prop("checked",true);


                            }
                            else
                            {

                                  $('input:radio[value=F]').prop("checked",true);
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



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="SuccessAlert" class="alert alert-success">
        <strong>Success!</strong>
        <label id="SuccessMsg"></label>
    </div>
    <div id="FailureAlert" class="alert alert-danger">
        <strong>Unsuccessful!</strong>
        <label id="FailureMsg"></label>
    </div>

    <label class="title" id="title" runat="server">Membership > Member Registration</label>

    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Individual Associate Details <i class="fas fa-angle-up right"></i></div>
    <div class="collapse" id="UserRegister">
        <div class="SubHeader" style="overflow: auto">

            <%--            <asp:Label id="memType" runat="server" ></asp:Label>--%>

            <div class="wrapper">
                <input type="text" id="hiddentext" runat="server" class="none"  />


                <div class="left_div" style="float: left; margin-top: -27px;">

                    <label for="Honorific">Honourific</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlList" runat="server" class="ddlStyle"></asp:DropDownList>
                    </div>

                    <label for="Saluation">Saluation</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtSalutationField" style="width: 150px;" />
                    </div>

                    <label for="firstName">First Name:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtFirstName" style="width: 350px;" required />
                    </div>

                    <label for="surname">Surname:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtSurname" style="width: 350px;" />
                    </div>

                    <%--   <label for="fullName">Full Name:</label>
        <div class="form-group">
            <input type="text" class="form-control" runat="server" id="txtFullName" style="width: 350px;" />
        </div>--%>

                    <label for="fullNameNT">Full Name Nametag:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtFullNameNameTag" style="width: 350px;" />
                    </div>

                    <label for="Nationality">Nationality</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlNationality" runat="server" class="ddlStyle">
                            <asp:ListItem Text="Singapore" Value="SGP"></asp:ListItem>
                            <asp:ListItem Text="Malaysia" Value="MYS"></asp:ListItem>


                        </asp:DropDownList>
                    </div>

                    <label for="Gender">Gender:</label>


                    <div class="radio">
                        <label>
                            <input type="radio" id="Male" name="optradio" runat="server" value="M">Male</label>
                    </div>
                    <div class="radio">
                        <label>
                            <input type="radio" id="Female" name="optradio" runat="server" value="F">Female</label>
                    </div>



                    <label for="Email">E-Mail Address:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtEmail" style="width: 350px;" />
                    </div>


                    <label for="Status">Status:</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlStatus" runat="server" Class="ddlStyle">
                            <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                            <asp:ListItem Text="Retired" Value="Retired"></asp:ListItem>


                        </asp:DropDownList>
                    </div>


                    <div class="form-group" style="float: left; margin-top:122px;">
                        <button type="button" id="AddPA" runat="server" data-toggle="modal" data-target="#Add_PA" class="btn btn-primary" style="width: 150px; height: 40px;">
                            Add PA
                        </button>

                            <button type="button" runat="server" id="btnSave" onserverclick="Button_Save" class="btn btn-primary" style="width: 150px; height: 40px;">
                            Save
                        </button>

                    </div>
               <%--     <div class="form-group" style="float: right;">
                    

                    </div>--%>


                </div>


                <div class="right_div" style="float: left; margin-top: -27px; margin-left: 138px;">


                    <label for="Source">Source:</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true" Class="ddlStyle" OnSelectedIndexChanged="getCat1"></asp:DropDownList>
                    </div>

                    <label for="Cat1">Cat 1:</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCat1" runat="server" Class="ddlStyle"></asp:DropDownList>
                    </div>
                    <label for="Cat2">Cat 2:</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCat2" runat="server" Class="ddlStyle"></asp:DropDownList>
                    </div>


                    <label for="TelephoneNo">Telephone Number:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtTelephone" style="width: 350px;" />
                    </div>

                    <label for="organization1">Organization 1:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtOrg1" style="width: 350px;" />
                    </div>

                    <label for="department1">Department 1:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtDept1" style="width: 350px;" />
                    </div>

                    <label for="designation1">Designation 1:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtDesig1" style="width: 350px;" />
                    </div>

                    <label for="organization2">Organization 2:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtOrg2" style="width: 350px;" />
                    </div>

                    <label for="department2">Department 2:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtDept2" style="width: 350px;" />
                    </div>

                    <label for="designation2">Designation 2:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtDesig2" style="width: 350px;" />
                    </div>

                    <label for="SDR">Special Dietary Restriction:</label>
                    <div class="form-group">
                        <input type="text" class="form-control" runat="server" id="txtSDR" style="width: 350px;" />
                    </div>





                </div>
                <div id="MemberContent">
                    <asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView runat="server" EnableViewState="false" ID="UserTable" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
                                <Columns>
                                    <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                    <asp:BoundField DataField="First_Name" HeaderText="First Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                    <asp:BoundField DataField="Surname" HeaderText="Surname" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />

                                    <asp:BoundField DataField="Email_Addr" HeaderText="E-mail Address" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                    <asp:BoundField DataField="Tel_Num" HeaderText="Telephone Number" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />

                                    <asp:CommandField EditText="Edit" ShowEditButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                    <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>



            </div>
        </div>
        <%--   <div id="Modal_Container"><UserControl:AddPA runat="server" /></div>--%>
        <%-- a --%>
        <%--        <div class="wrapper">          
                </div>--%>

        <%--        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
         <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
         <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
         <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
         <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>   
     <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>
         <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label>
             <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label>--%>
    </div>

    <%-- </div>--%>
    <%--<UserControl:AddPA runat="server"/>--%>
    <div id="Modal_Container">


        <div id="Add_PA" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 860px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">Add PA</h4>
                        <button type="button" class="close" data-dismiss="modal"><i class="far fa-times-circle"></i></button>
                    </div>
                    <div class="modal-body1" style="padding-top: 20px; padding-left: 50px; padding-right: 50px; padding-bottom: 10px;">
                        <!--Content goes here -->


                        <div class="form-group">
                            <label for="Associate" id="associateType"></label>
                            <label for="AssociateName" id="associateName"></label>
                        </div>
                        <div>

                            <div style="float: left; margin-top: -5px;">
                                <label for="Honorific">Honourific</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="modalDDList" runat="server" class="ddlStyle"></asp:DropDownList>
                                </div>



                                <label for="firstName">First Name:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="modalFName" style="width: 350px;" />
                                </div>


                                <label for="surname">Surname:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="modalSname" style="width: 350px;" />
                                </div>


                                <div class="form-group">
                                    <label for="Email">E-Mail Address:</label>
                                    <div class="form-group">
                                        <input type="text" class="form-control" runat="server" id="modalEmail" style="width: 350px;" />
                                    </div>

                                    <label for="TelephoneNo">Telephone Number:</label>
                                    <div class="form-group">
                                        <input type="text" class="form-control" runat="server" id="modalTelNo" style="width: 350px;" />
                                    </div>


                                </div>

                                <button type="button" id="submitPA" runat="server" class="btn btn-primary" onserverclick="Submit_PA" style="width: 150px; height: 40px;">Save</button>

                            </div>

                            <div style="float: left; margin-left: 50px; margin-top: -13px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>









</asp:Content>
