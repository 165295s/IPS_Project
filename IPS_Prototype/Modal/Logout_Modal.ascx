<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Logout_Modal.ascx.cs" Inherits="IPS_Prototype.WebUserControl1" %>

  
<link href="../css/Modal.css" rel="stylesheet" />
  

 <!--Logout confirmation modal-->
  <div id="logout_confirmation" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        
        <h4 class="modal-title">Do you want to logout?</h4>
        <button type="button" class="close" data-dismiss="modal"><i class="far fa-times-circle"></i></button>
      </div>
      <div class="modal-body">
        <button runat="server" type="button" class="btn btn-primary logout_btn" id="cfmYes" onserverclick="Logout">yes</button>
        <button type="button" class="btn btn-danger logout_btn" id="cfmNo">cancel</button>
      </div>
      
    </div>

  </div>
</div>

