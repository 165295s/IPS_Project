﻿<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Membership_Registration_OrganisationDetail.aspx.cs" Inherits="IPS_Prototype.Membership_Registration_OrganizationDetail" %>

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


            function displayFailureMsg(msg) {

                $('#FailureAlert').css('display', 'block');
                $('#FailureMsg').text(msg);

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
        <label for="FailureAlert" id="FailureMsg"></label>
    </div>



    <label class="title" id="title" runat="server">Membership > Member Registration</label>

    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Organisation Detail</div>
    <div class="SubHeader" style="overflow: auto;">

        <div id="left-div" style="float: left;">

            <div class="form-group" id="slidertoggleDIV" runat="server">

                <input id="sliderToggle" enableviewstate="true" type="checkbox" runat="server" data-toggle="toggle" data-width="150" data-height="40" data-style="ios" data-offstyle="danger" data-onstyle="success" data-on="Edit Mode On" data-off="Edit Mode Off">
            </div>

            <label for="organisationName">Organisation Name:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtOrgNameField" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="mAddressLine1">Mailing Address Line 1:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtMailAddrLine1" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="mAddressLine2">Mailing Address Line 2:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtMailAddrLine2" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="City">City:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtCity" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="postalCode">Postal Code:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtPostalCode" autocomplete="off" style="width: 350px;" />
            </div>


            <label for="TelephoneNo">Telephone Number:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtTelephone" autocomplete="off" style="width: 350px;"/>
            </div>

            <label for="OfficeNo">Office Number:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtOffice" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="WebsiteURL">Website URL:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtWebsiteURL" autocomplete="off" style="width: 350px;" />
            </div>

            <div class="form-group">
                <button type="button" runat="server" onserverclick="button_next" class="btn btn-primary" style="width: 150px; height: 40px;">
                    Next
                      <i class="fas fa-arrow-right"></i>
                </button>


            </div>

        </div>

        <div id="right-div" style="float: left; margin-left: 138px;">

            <label for="BusinessDesc">Business Description:</label><label style="color: red;">*</label>
            <div class="form-group">
                <textarea class="form-control" rows="5" runat="server" id="txtbDesc" style="width: 350px; height: 125px;"></textarea>
            </div>

            <label for="PointOfContact">Point Of Contact:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="pointOfContact" autocomplete="off" style="width: 350px;" />
            </div>

            <label for="Notes">Notes:</label>
            <div class="form-group">
                <textarea class="form-control" rows="5" runat="server" id="txtnotes" autocomplete="off" style="width: 350px; height: 125px;"></textarea>
            </div>
            <label for="UEM">UEN:</label><label style="color: red;">*</label>
            <div class="form-group">
                <input type="text" class="form-control" runat="server" id="txtUEN" autocomplete="off" style="width: 350px;" />
            </div>


        </div>


    </div>






</asp:Content>
