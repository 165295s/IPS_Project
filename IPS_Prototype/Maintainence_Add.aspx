<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Maintainence_Add.aspx.cs" Inherits="IPS_Prototype.Maintainence_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $('#AddMaintainenceContent').collapse();
        });


    function checkAddMaintainence() {
    if ($("#ContentPlaceHolder1_LkUpCode").val() == "" || $("#ContentPlaceHolder1_CodeDesc").val() == "") {
        $('#ContentPlaceHolder1_submit').css('pointer-events', 'none');
    }
    else {
        $('#ContentPlaceHolder1_submit').css('pointer-events', 'auto');
    }
        }

   function checkLookUpAvailability() {
    $.ajax({
        type: "POST",
        url: "Maintainence_Add.aspx/checkLookUp",
        data: JSON.stringify({ IDVal: $('#ContentPlaceHolder1_LkUpCode').val(), type: $('#ContentPlaceHolder1_type').val()}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        failure: function (AjaxResponse) {
            $("#ContentPlaceHolder1_showValidity").text("test failure");
        }
    });
    function onSuccess(AjaxResponse) {
        if ($("#ContentPlaceHolder1_LkUpCode").val() == "") {
            $('#lookupfail').addClass("none");
            $('#lookuppass').addClass("none");
            $('#ContentPlaceHolder1_submit').css('pointer-events', 'none');
        }
        else if (AjaxResponse.d == 1) {
            $('#lookupfail').addClass("none");
            $('#lookuppass').removeClass("none");
            checkAddMaintainence();
        }
        else {
            $('#lookupfail').removeClass("none");
            $('#lookuppass').addClass("none");
            $('#ContentPlaceHolder1_submit').css('pointer-events', 'none');
        }

    };
};

function checkCodeDescAvailability() {
    $.ajax({
        type: "POST",
        url: "Maintainence_Add.aspx/checkCodeDesc",
        data: JSON.stringify({ IDVal: $('#ContentPlaceHolder1_CodeDesc').val(), type: $('#ContentPlaceHolder1_type').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        failure: function (AjaxResponse) {
            $("#ContentPlaceHolder1_showValidity").text("test failure");
        }
    });
    function onSuccess(AjaxResponse) {
        if ($("#ContentPlaceHolder1_CodeDesc").val() == "") {
            $('#codedescriptionfail').addClass("none");
            $('#codedescriptionpass').addClass("none");
            $('#ContentPlaceHolder1_submit').css('pointer-events', 'none');
        }
        else if (AjaxResponse.d == 1) {
            $('#codedescriptionfail').addClass("none");
            $('#codedescriptionpass').removeClass("none");
            checkAddMaintainence();
        }
        else {
            $('#codedescriptionfail').removeClass("none");
            $('#codedescriptionpass').addClass("none");
            $('#ContentPlaceHolder1_submit').css('pointer-events', 'none');
        }

    };
};
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="SuccessAlert" class="alert alert-success">
  <strong>Success!</strong> <label id="SuccessMsg"></label>
    </div>
    <div id="FailureAlert" class="alert alert-danger">
  <strong>Unsuccessful!</strong> There seems to be an error! Please notify the Administrators.
    </div>
    <label class="title">Code Maintainence > <label runat="server" id="title">Add</label> Code</label>
    
    <div class="CommonHeader"><label runat="server" id="CommonHeaderTitle">Add</label> <label id="HeaderName" runat="server">CAREP</label></div>
    <div id="AddMaintainenceContent" class="collapse">
    <div class="SubHeader">
        <div class="form-group">
            <label>Code Type</label>
            <input class="form-control" type="text" runat="server" id="type" readonly="readonly" style="width:350px;" />
        </div>
        <div class="form-group">
            <label style="display:block;">Look Up Code</label>
            <input class="form-control" type="text" maxlength="6" runat="server" onkeyup="checkLookUpAvailability()" placeholder="code" id="LkUpCode" style="width:200px; display:inline-block;" /><i id="lookupfail" class="far fa-times-circle validateIconFail none"><label style="font-size:15px; margin-left:10px;">Code is in use</label></i><i id="lookuppass" class="far fa-check-circle validateIconPass none"><label style="font-size:15px; margin-left:10px;">Code is available</label></i>
        </div>
        <div class="form-group">
            <label style="display:block;">Code Description</label>

            <input class="form-control" type="text" runat="server" onkeyup="checkCodeDescAvailability()" placeholder="Code Description" id="CodeDesc" style="width:350px; display:inline-block;" /><i id="codedescriptionfail" class="far fa-times-circle validateIconFail none"><label style="font-size:15px; margin-left:10px;">Code Description is in use</label></i><i id="codedescriptionpass" class="far fa-check-circle validateIconPass none"><label style="font-size:15px; margin-left:10px;">Code Description is available</label></i>
        </div>
        <div class="form-group">
            <button type="button" runat="server" class="btn btn-primary" style="pointer-events:none;" id="submit" onserverclick="SubmitCode">Submit</button>
        </div>
    </div>
        </div>
    
</asp:Content>
