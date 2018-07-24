﻿<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Events_Management.aspx.cs" Inherits="IPS_Prototype.Events_Management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
        $(document).ready(function () {

        $('#dLabel + ul > li').click(function () {
            // Declare variables 
            var input, filter, table, tr, td, i;
            input = document.getElementById("dLabel");

            filter = input.innerText.toUpperCase();
            console.log("test", filter);
            table = document.getElementsByClassName("BlueTable");
            tr = $(".BlueTable").find('tr');

            if (filter != "ALL") {
                // Loop through all table rows, and hide those who don't match the search query
                for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];
                if (td) {
                        if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                         tr[i].classList.remove("none");
                         tr[i].classList.add("next");
                        } else {
                         tr[i].classList.remove("next");
                         tr[i].classList.add("none");
                        }
                    }
                }
            }

            else {
                for (i = 0; i < tr.length; i++) {
                tr[i].classList.remove("next");
                tr[i].classList.remove("none");

                }
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
    <label class="title">Events Management</label>
   
          <div id="MemberSearch">
             
              
              <div id="MemberHover">
                    <a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-primary dropdown-toggle customDropdown">Event Name</a>
                                <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                
                                    <li><a class="dropdown-item" href="#">Event Name</a></li>
                                    <li><a class="dropdown-item" href="#">Event Status</a></li>
                                    </ul>

              <input type="text" class="SearchTxtbox" id="UserSearchTxtbox"/>

              <button type="button" class="btn btn-primary SearchIcon" id="UserSearchIcon"><i class="fas fa-search"></i></button>

              </div>
              <button type="button" runat="server" class="btn btn-primary right" id="EventsAdd" onserverclick="EventsAddClick"><i class="fas fa-plus"></i></button>
          </div>
        <div id="MemberContent">
        <asp:GridView runat="server" ID="EventTable" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None">
           <Columns>
               <asp:BoundField DataField="Name" HeaderText="Event Name" />
               <asp:BoundField DataField="START_DT_TIME" HeaderText="Start Date" />
               <asp:BoundField DataField="END_DT_TIME" HeaderText="End Date" />
               <asp:BoundField HeaderText="Attendance" />
               <asp:BoundField HeaderText="Contribution" />
               <asp:BoundField DataField="Status" HeaderText="Status" />

              
               <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" OnClick="GuestListClick" CssClass="btnTransparent" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;"><i class="fas fa-clipboard-list"></i></asp:LinkButton>
                       
                       <button type="button" runat="server" onserverclick="AttendanceClick" class="btnTransparent"><i class="fas fa-user"></i></button>
                       <button type="button" runat="server" onserverclick="ContributionClick" class="btnTransparent"><i class="fas fa-hand-holding-usd"></i></button>
                       <asp:LinkButton runat="server" OnClick="InviteClick" CssClass="btnTransparent" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;"><i class="fas fa-envelope"></i></asp:LinkButton>
                       
                   </ItemTemplate>
               </asp:TemplateField>
              
           </Columns>
        </asp:GridView>
     
   </div>
</asp:Content>
