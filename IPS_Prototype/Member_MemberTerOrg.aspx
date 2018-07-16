<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Member_MemberTerOrg.aspx.cs" Inherits="IPS_Prototype.Member_MemberTerOrg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>       
        $(document).ready(function () {
            $('#UserTerHeader > .fas').toggleClass('active');
            $('#UserTer').collapse();

              $('#ContentPlaceHolder1_sentdate').datepicker({
                dateFormat: "dd/mm/yy"
            });
              $('#ContentPlaceHolder1_receiveddate').datepicker({
                dateFormat: "dd/mm/yy"
            });
            });      
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="SuccessAlert" class="alert alert-success">
  <strong>Success!</strong> <label id="SuccessMsg"></label>
    </div>
    <div id="FailureAlert" class="alert alert-danger">
  <strong>Unsuccessful!</strong> There seems to be an error! Please notify the Administrators.
    </div>
     <label class="title" id="title" runat="server"></label>
    <div class="CommonHeader" id="UserTerHeader" runat="server" style=""></div>
    <div class="collapse" id="UserTer">
   <div class="SubHeader" style="overflow:auto;">


 <!--Start form from here -->
          <asp:HiddenField ID="organisationID" runat="server" />
         <div id="left-div" style="float:left;">

        <div class="form-group">
    <label for="lblSentDate">TER Sent Date:</label>
        <input runat="server" type="text" class="form-control" style="width:350px;" id="sentdate" />
              </div>

          <div class="form-group">
    <label for="lblReceivedDate">TER Received Date:</label>
        <input runat="server" type="text" class="form-control" style="width:350px;" id="receiveddate" />
              </div>

               <div class="form-group">
             <%-- type="button"--%>
            <button type="button" id="UserSubmit" class="btn btn-primary" runat="server" onserverclick="Submit_Ter">Save</button>
        </div>

            </div>

        <div id="right-div" style="float:left; margin-left:138px;">

      <div class="form-group">
    <label for="lbldetails">TER Details:</label>
     <textarea id="terdetails" class="form-control" name="terdetails" style="width: 400px;height:125px;" runat="server" rows="5"></textarea>
        </div>


            </div>

        </div>
        </div>
</asp:Content>
