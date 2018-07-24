<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Events_Invite.aspx.cs" Inherits="IPS_Prototype.Events_Invite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
          $(document).ready(function () {
              if ($('#ContentPlaceHolder1_hiddenselect').val() == "") {
                  $('#ContentPlaceHolder1_hiddenselect').val("0");
              }
              
              if ($('#ContentPlaceHolder1_hiddenselect').val() == "0") {
                    $('#divname').css('display', 'block');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'none');
                    
                }
              
                else if ($('#ContentPlaceHolder1_hiddenselect').val() == "1") {
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'block');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'none');
                    
                }
                else if ($('#ContentPlaceHolder1_hiddenselect').val() == "2") {
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'block');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'none');
                    
                }
                else if ($('#ContentPlaceHolder1_hiddenselect').val() == "3") {
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'block');
                    $('#cat2').css('display', 'none');
                   
                }
                else if ($('#ContentPlaceHolder1_hiddenselect').val() == "4"){
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'block');
                }

            $('#UserRegisterHeader > .fas').toggleClass('active');
            $('#UserRegister').collapse();

            $('#ContentPlaceHolder1_CAType').change(function () {
                if ($('#ContentPlaceHolder1_CAType option:selected').index() == 0) {
                    $('#ContentPlaceHolder1_CACheckbox').css('display', 'none');
                    $('#ContentPlaceHolder1_Organisation').css('display', 'none');
                    $('#ContentPlaceHolder1_Individual').css('display', 'block')
                   
                }
                else {
                    $('#ContentPlaceHolder1_CACheckbox').css('display', 'block');
                    $('#ContentPlaceHolder1_Organisation').css('display', 'block');
                    $('#ContentPlaceHolder1_Individual').css('display', 'none')
                 
                }
            });

            if ($('#payment').css('display') == 'block') {
                $('#ContentPlaceHolder1_GuestListBtn').css('pointer-events', 'none');
            }
            else {
                $('#ContentPlaceHolder1_GuestListBtn').css('pointer-events', 'auto');
            }

            function checkPayment() {
                 
                     if ($('#ContentPlaceHolder1_Charge').val() == "") {
                         $('#ContentPlaceHolder1_GuestListBtn').css('pointer-events', 'none');
                     }
                     else {
                         $('#ContentPlaceHolder1_GuestListBtn').css('pointer-events', 'auto');
                     }
            }

            $('#ContentPlaceHolder1_FilterBy').change(function () {
                if ($('#ContentPlaceHolder1_FilterBy option:selected').index() == 0) {
                    $('#divname').css('display', 'block');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'none');
                    $('#ContentPlaceHolder1_hiddenselect').val("0");
                }
               
                else if ($('#ContentPlaceHolder1_FilterBy option:selected').index() == 1) {
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'block');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'none');
                    $('#ContentPlaceHolder1_hiddenselect').val("1");
                }
                else if ($('#ContentPlaceHolder1_FilterBy option:selected').index() == 2) {
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'block');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'none');
                    $('#ContentPlaceHolder1_hiddenselect').val("2");
                }
                else if ($('#ContentPlaceHolder1_FilterBy option:selected').index() == 3) {
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'block');
                    $('#cat2').css('display', 'none');
                    $('#ContentPlaceHolder1_hiddenselect').val("3");
                }
                else {
                    $('#divname').css('display', 'none');
                    
                    $('#past').css('display', 'none');
                    $('#source').css('display', 'none');
                    $('#designation').css('display', 'none');
                    $('#cat2').css('display', 'block');
                    $('#ContentPlaceHolder1_hiddenselect').val("4");
                }
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
    <label class="title" id="title" runat="server">Events Management > Add Events > Events Invite</label>
    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Events Details</div>
    <div class="collapse" id="UserRegister">
    <div class="SubHeader">
 

        <div>
            <label for="EventName" style="margin-left:41px;">Event Name:</label>
            <label runat="server"  id="Name">IPS-Nathan Lectures By Mr Lim Siong Guan: Lecture 11("The Fourth Generation")</label>

            <a href="#" style="font-size:17px; font-weight:600; float:right;"><u>View Guest List</u></a>
        </div>

         <div>
            <label for="EventDateTime">Event Date / Time:</label>
            <label runat="server" id="Datetime">28/3/2018 3:00PM</label>
        </div>

       
    
             <div id="payment" runat="server">
    <div class="input-group mb-3" id="input_paid" style="width:263px; margin-left:74px;">
        <label for="Email" style="margin-bottom:-15px;">Charge:</label>
    <div class="input-group-prepend" style="margin-left:6px; border-bottom-left-radius:3px !important; border-top-left-radius:3px !important; margin-top:-5px;">
        
      <span class="input-group-text"><i class="fas fa-dollar-sign"></i></span>
    </div>
    <input class="form-control" type="number" runat="server" onkeyup="checkPayment()" id="Charge" style="border-top-right-radius:3px; border-bottom-right-radius:3px; margin-top:-5px;">
  </div>
                 </div>


        
         <div class="form-group" style="margin-left:93px;">
            <label for="comment">Filter:</label>
           
            <asp:dropdownlist runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="FilterBy_SelectedIndexChanged" id="FilterBy" style=" width:200px; display:inline-block;">
            
                <asp:ListItem>Name</asp:ListItem>
                 
                 <asp:ListItem>Past Events</asp:ListItem>
                 <asp:ListItem>Source</asp:ListItem>
                 <asp:ListItem>Designation</asp:ListItem>
                 <asp:ListItem>Cat 2</asp:ListItem>
                
            </asp:dropdownlist>
        </div>
       

        <div class="form-group" id="divname" style="margin-left:75px;">
            <label for="comment" style="margin-left:-53px;">Filter By Name:</label>
           
            <input class="form-control" style="width:400px; display:inline-block;" runat="server" type="text" id="FilterTxt" />
        </div>

    

        <div class="form-group" id="past" style="margin-left:3px; display:none">
            <label for="comment">Filter Past Events:</label>
           
            <asp:dropdownlist class="form-control" runat="server" id="FilterPastevent" style=" width:200px; display:inline-block;">
            
               
               
            </asp:dropdownlist>
        </div>

          <div class="form-group" id="source" style="margin-left:13px; display:none;">
            <label for="comment">Filter By Source:</label>
              <asp:DropDownList class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterCat1_SelectedIndexChanged" id="Source" style="margin-left:2px; width:200px; display:inline-block;">

              </asp:DropDownList>
           
          

                <asp:DropDownList class="form-control" runat="server" id="FilterCat1" style="margin-left:20px; width:200px; display:inline-block;">
            
                
                
               
            </asp:DropDownList>
        </div>

         <div class="form-group" id="designation" style="margin-left:5px; display:none;">
            <label for="comment">Filter Designation:</label>
           
            <select class="form-control" runat="server" id="FilterDesignation" style=" width:200px; display:inline-block;">
            
                <option>Manager</option>
                <option>Director</option>
                <option>CEO</option>
               
            </select>
        </div>

         <div class="form-group" id="cat2" style="margin-left:31px; display:none;">
            <label for="comment">Filter By Cat2:</label>
           
            <select class="form-control" runat="server" id="FilterCat2" style=" width:300px; display:inline-block;">
            
                <option>PARL</option>
                <option>EXPARL</option>
                <option>SRNF_Donors</option>
                <option>SRNF_Connector</option>
                <option>SRNF_Committee</option>
                <option>IPS_Former Management</option>
                <option>IPS_Former Staff</option>
                <option>NUS_BOT</option>
                <option>NUS Former BOT</option>
               
            </select>
        </div>

                <div class="form-group" id="type" style="margin-left:29px;">
            <label for="comment">Filter By Type:</label>
            <select class="form-control" runat="server" id="CAType" style="width:200px; display:inline-block;">
            
            <option value="IA">Individual Associate</option>
                <option value="CA">Corporate Associate</option>
            </select>
               <div style="margin-left:110px; display:none;" id="CACheckbox" runat="server">
               <asp:PlaceHolder runat="server" ID="checkboxes">

               </asp:PlaceHolder>
                   </div>
        </div>

        <div class="form-group" style="margin-left:27.4em;">
              <button type="button" class="btn btn-primary" runat="server" onserverclick="RetrieveTable" id="Retrieve">SEARCH</button>
            </div>

        <div class="form-group" style="text-align:right; margin-top:30px; margin-right:5em;">
           
          <label style="font-size:20px;">Rows:</label>
            <asp:DropDownList ID="RowsSelect" runat="server" CssClass="rowsDrop" AutoPostBack="true" OnSelectedIndexChanged="RoesDdl_SelectedIndexChanged">
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
           <label style="font-size:20px;">Per Page</label>
        </div>

        <div runat="server" id="Individual">
             <asp:GridView runat="server" ID="IndividualTable" DataKeyNames="Email_Addr" PageSize="5" AllowPaging="true" OnPageIndexChanging="Individual_PageIndexChanging" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" GridLines="None" BorderStyle="None">
                 
           <Columns>
                <asp:TemplateField>
                   <ItemTemplate>

                        <asp:CheckBox ID="IndividualInvite" OnCheckedChanged="IndividualCheck" CssClass="checkbox" AutoPostBack="true" runat="server"/>
                         
                   </ItemTemplate>
                    <HeaderTemplate>Invite</HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
               <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:BoundField DataField="Full_Name" HeaderText="Full Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" />
               <asp:BoundField DataField="Email_Addr" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White"/>
               
                <asp:TemplateField>
                   <ItemTemplate>
                       <asp:DropDownList ID="Role" runat="server" CssClass="custom-ddl">
                           <asp:ListItem>Guest</asp:ListItem>
                           <asp:ListItem>Speaker</asp:ListItem>
                           <asp:ListItem>VIP</asp:ListItem>
                       </asp:DropDownList>
                      
                   </ItemTemplate>
                    <HeaderTemplate>Role</HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
           </Columns>
                 <PagerStyle CssClass = "GridPager" />
        </asp:GridView>
        </div>
       
          <div runat="server" id="Organisation" style="display:none;">
             <asp:GridView runat="server" ID="OrganisationTable"  DataKeyNames="Email_Addr" CssClass="table table-hover BlueTable" AllowPaging="true" PageSize="5" OnPageIndexChanging="Organisation_PageIndexChanging" AutoGenerateColumns="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
           <Columns>
                <asp:TemplateField>
                   <ItemTemplate>
                      <asp:CheckBox ID="OrganisationInvite" runat="server" CssClass="checkbox" OnCheckedChanged="OrganisationCheck"  AutoPostBack="true"/>
                      
                   </ItemTemplate>
                    <HeaderTemplate>Invite</HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
               <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White"/>
               <asp:BoundField DataField="Full_Name" HeaderText="Full Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White"/>
               <asp:BoundField DataField="Email_Addr" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White"/>
               
               <asp:BoundField DataField="Name" HeaderText="Organisation" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White"/>
                <asp:TemplateField>
                   <ItemTemplate>
                      <asp:DropDownList ID="Role" runat="server" CssClass="custom-ddl">
                           <asp:ListItem>Guest</asp:ListItem>
                           <asp:ListItem>Speaker</asp:ListItem>
                           <asp:ListItem>VIP</asp:ListItem>
                       </asp:DropDownList>
                      
                   </ItemTemplate>
                    <HeaderTemplate>Role</HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
           </Columns>
                 <PagerStyle CssClass = "GridPager" />
        </asp:GridView>
        </div>
        <div style="text-align:right; margin-top:20px;">
         <button type="button" class="btn btn-primary" runat="server" style="pointer-events:none;" id="GuestListBtn" onserverclick="GuestlistBtn">ADD TO GUESTLIST<i style="margin-left:5px;" class="fas fa-angle-right"></i></button>
            </div>
   </div>
        </div>
    <input type="number" runat="server" id="hiddenselect" style="display:none;"/>
</asp:Content>
