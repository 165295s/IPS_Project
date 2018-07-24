<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Events_Add.aspx.cs" Inherits="IPS_Prototype.Events_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
        $(document).ready(function () {
            $('#UserRegisterHeader > .fas').toggleClass('active');
            $('#UserRegister').collapse();
            $('#ContentPlaceHolder1_datetime').datetimepicker({
                dateFormat: "dd-MM-yy",
                timeFormat: "hh:mm tt"
            });

            $('#ContentPlaceHolder1_datetimeEnd').datetimepicker({
                dateFormat: "dd-MM-yy",
                timeFormat: "hh:mm tt"
            });
            
           
          
         });
       
         function check() {
             console.log("check", "check");
                if ($('#ContentPlaceHolder1_Event_Input_Name').val() == "" || $('#ContentPlaceHolder1_Event_Input_Description').val() == "" || $('#ContentPlaceHolder1_Event_Input_Venue').val() == "") {
                    $('#ContentPlaceHolder1_SubmitEvent').css('pointer-events', 'none');
                }
                else {
                    console.log("success", "allowed");
                    $('#ContentPlaceHolder1_SubmitEvent').css('pointer-events', 'auto');
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
    <label class="title" id="title" runat="server">Events Management > Add Events</label>
    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Events Details</div>
    <div class="collapse" id="UserRegister">
    <div class="SubHeader">
         <div class="form-group">
    <label for="FirstName">Event Name</label>

    <input type="text" class="form-control" id="Event_Input_Name" onkeyup="check()" placeholder="Enter Event Name" runat="server" style="width:450px; ">
  </div>

        <div class="form-group">
            <label for="FirstName">Event Type</label>
            <select class="form-control" runat="server" id="EventType_ddl" style="width:300px;">
            
            </select>
        </div>

         <div class="form-group">
            <label for="comment">Event Description:</label>
            <textarea class="form-control" rows="3" runat="server" onkeyup="check()" id="Event_Input_Description" style="width:450px;"></textarea>
        </div>

        <div class="form-group">
            <label for="comment">Event Venue:</label>
            <textarea class="form-control" rows="3" runat="server" onkeyup="check()" id="Event_Input_Venue" style="width:450px;"></textarea>
        </div>

         <div class="form-group">
            <label for="comment">Event Start Date/Time:</label>
            <input class="form-control" style="width:300px;" runat="server" type="text" id="datetime" />
        </div>

         <div class="form-group">
            <label for="comment">Event End Date/Time:</label>
            <input class="form-control" style="width:300px;" runat="server" type="text" id="datetimeEnd" />
        </div>

           <div class="form-group">
            <label for="comment">Paid Event:</label>
            <input type="checkbox" runat="server" id="eventPaid" />
        </div>

          <div class="form-group">
            <label for="comment">Fundraising Event:</label>
            <input type="checkbox" runat="server" id="CheckFund" />
        </div>

         <div class="form-group">
            
            <button type="button" class="btn btn-primary" style="pointer-events:none;" runat="server" onserverclick="EventAddClick" id="SubmitEvent">SUBMIT</button>
        </div>
   </div>
        </div>
    
</asp:Content>
