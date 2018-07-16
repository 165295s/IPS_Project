<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Error_Modal.ascx.cs" Inherits="IPS_Prototype.Modal.Error_Modal" %>
<link href="../css/Modal.css" rel="stylesheet" />
  

 <!--Logout confirmation modal-->
  <div id="ErrorModal" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="height:50px; background-color:orangered; padding:5px; padding-left:0px;">
        
        <h4 class="modal-title" style="margin-left:15px;">Error!</h4>
        
      </div>
      <div class="modal-body" style="text-align:center;">
        <label runat="server" id="ErrorMessage" style="font-size:20px;"></label>
        <br />
        <label style="font-size:20px; color:darkred;">Redirecting to login page...</label>
      </div>
      
    </div>

  </div>
</div>