<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="User_Add.aspx.cs" Inherits="IPS_Prototype.User_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $('#UserRegisterHeader > .fas').toggleClass('active');
            $('#UserRegister').collapse();

        });

function checkDetails() {
    if ($('#ContentPlaceHolder1_User_Input_Name').val() == "" || $('#ContentPlaceHolder1_User_Input_Username').val() == "" || $('#ContentPlaceHolder1_User_Input_Email').val() == "") {
        $('#ContentPlaceHolder1_UserSubmit').css('pointer-events', 'none');
    }
    else {
        $('#ContentPlaceHolder1_UserSubmit').css('pointer-events', 'auto');
    }
};

function checkIDAvailability() {
    $.ajax({
        type: "POST",
        url: "User_Add.aspx/checkUserName",
        data: "{IDVal: '" + $("#ContentPlaceHolder1_User_Input_Username").val() + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        failure: function (AjaxResponse) {
            $("#ContentPlaceHolder1_showValidity").text("test failure");
        }
    });
    function onSuccess(AjaxResponse) {
        if ($("#ContentPlaceHolder1_User_Input_Username").val() == "") {
            $('#usernamefail').addClass("none");
            $('#usernamepass').addClass("none");
            $('#ContentPlaceHolder1_UserSubmit').css('pointer-events', 'none');
        }
        else if (AjaxResponse.d == 1) {
            $('#usernamefail').addClass("none");
            $('#usernamepass').removeClass("none");
            checkDetails();
        }
        else {
            $('#usernamefail').removeClass("none");
            $('#usernamepass').addClass("none");
            $('#ContentPlaceHolder1_UserSubmit').css('pointer-events', 'none');
        }
        
    };
};

function checkEmail() {
    var sEmail = $('#ContentPlaceHolder1_User_Input_Email').val();

    if ($.trim(sEmail).length == 0) {
        
        $('#emailfail').removeClass('none');  
        $('#emailpass').addClass('none');
        $('#ContentPlaceHolder1_UserSubmit').css('pointer-events', 'none');
    }
    
    if (validateEmail(sEmail)) {
        $('#emailpass').removeClass('none');    
        $('#emailfail').addClass('none');
        $('#ContentPlaceHolder1_UserSubmit').css('pointer-events', 'auto');
        checkDetails();
    }
    
    else {
       
        $('#emailfail').removeClass('none');
        $('#emailpass').addClass('none');
        $('#ContentPlaceHolder1_UserSubmit').css('pointer-events', 'none');
    }
}

   
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="SuccessAlert" class="alert alert-success">
  <strong>Success!</strong> <label id="SuccessMsg"></label>
    </div>
    <div id="FailureAlert" class="alert alert-danger">
  <strong>Unsuccessful!</strong> There seems to be an error! Please notify the Administrators.
    </div>
    <label class="title" id="title" runat="server">User Management > Add User</label>
    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Register Account Details <i class="fas fa-angle-up right"></i></div>
    <div class="collapse" id="UserRegister">
    <div class="SubHeader">
  <div class="form-group">
    <label for="FirstName">Name</label>

    <input type="text" class="form-control" onkeyup="checkDetails()" id="User_Input_Name" placeholder="Enter Name" runat="server" style="width:250px;">
  </div>
 
    <div class="form-group">
    <label for="Email" style="margin-bottom:-10px;">Email</label>
    <div class="input-group mb-3" style="width:395px;">
        
    <div class="input-group-prepend">
      <span class="input-group-text"><i class="far fa-envelope"></i></span>
    </div>
    <input class="form-control" type="text" placeholder="Email address" runat="server" onkeyup="checkEmail()" id="User_Input_Email" style="border-top-right-radius:3px; border-bottom-right-radius:3px;"><div class="ValidateIconBox"><i id="emailfail" class="far fa-times-circle validateIconFail none"></i><i id="emailpass" class="far fa-check-circle validateIconPass none"></i></div>
  </div>
  </div>
        <div class="form-group">
    <label for="UserLevel">User Permission Level</label>
    <select class="form-control" id="Select_Permission_Level" runat="server" style="width:350px;">
      <option>SuperAdmin</option>
      <option>Executive</option>
      <option>Staff</option>
      <option>Temp</option>
    </select>
  </div>
                       <div class="form-group">
    <label for="Username" style="margin-bottom:-10px;">User ID</label>
    <div class="input-group mb-3" style="width:550px;">
        
    <div class="input-group-prepend">
      <span class="input-group-text"><i class="fas fa-user"></i></span>
    </div>
    <input class="form-control" type="text" onkeyup="checkIDAvailability()" placeholder="User ID" runat="server" id="User_Input_Username" style="border-top-right-radius:3px; border-bottom-right-radius:3px;"><div style="min-width:200px !important;"><i id="usernamefail" class="far fa-times-circle validateIconFail none"><label style="font-size:15px; margin-left:10px;">UserID is in use</label></i><i id="usernamepass" class="far fa-check-circle validateIconPass none"><label style="font-size:15px; margin-left:10px;">UserID is available</label></i></div>
  </div>
  </div>
        <div class="form-group">
            <button type="button" id="UserSubmit" class="btn btn-primary" runat="server" onserverclick="Submit_User" style="pointer-events:none;">Submit<i class="fas fa-arrow-right" style="margin-left:10px"></i></button>
        </div>
       
   </div>
        </div>
   
    
</asp:Content>
