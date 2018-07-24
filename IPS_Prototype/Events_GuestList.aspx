<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Events_GuestList.aspx.cs" Inherits="IPS_Prototype.Events_GuestList" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
        $(document).ready(function () {
            $('#UserRegisterHeader > .fas').toggleClass('active');
            $('#UserRegister').collapse();
            
            
            $('#ContentPlaceHolder1_GuestListTable_Invite_dt_0').datetimepicker({
                dateFormat: "dd-MM-yy",
                timeFormat: "hh:mm tt"
            });

            $('#ContentPlaceHolder1_GuestListGuest_Invite_dt_0').datetimepicker({
                dateFormat: "dd-MM-yy",
                timeFormat: "hh:mm tt"
            });

            $('#ContentPlaceHolder1_GuestListSpeaker_Invite_dt_0').datetimepicker({
                dateFormat: "dd-MM-yy",
                timeFormat: "hh:mm tt"
            });

            $('#ContentPlaceHolder1_GuestListVIP_Invite_dt_0').datetimepicker({
                dateFormat: "dd-MM-yy",
                timeFormat: "hh:mm tt"
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
    <label class="title" id="title" runat="server">Events GuestList</label>
    <div class="CommonHeader" id="UserRegisterHeader" runat="server" style="">Events Details</div>
    <div class="collapse" id="UserRegister">
    <div class="SubHeader">
         <div>
            <label for="EventName" style="margin-left:41px;">Event Name:</label>
            <label runat="server"  id="Name">IPS-Nathan Lectures By Mr Lim Siong Guan: Lecture 11("The Fourth Generation")</label>

          
        </div>

         <div>
            <label for="EventDateTime">Event Date / Time:</label>
            <label runat="server" id="Datetime">28/3/2018 3:00PM</label>
        </div>

         <div runat="server" id="chargediv" style="margin-left:40px;">
            <label for="EventCharge">Event Charge:</label>
            <label runat="server" id="Charge">$200.00</label>
        </div>

          <div class="form-group" style="text-align:right; margin-top:30px;">
          <label style="margin-right:10px; font-size:18px; font-weight:700;">Rows:</label>
               <asp:DropDownList ID="RowsDdl1" runat="server" style="width:80px; display:inline-block" CssClass="rowsDrop" AutoPostBack="true" OnSelectedIndexChanged="RoesDdl_SelectedIndexChanged">
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
           
        </div>

        <label style="font-size:17px; font-weight:700;">All</label>
        <button type="button" style="float:right; margin-bottom:10px;" class="btn btn-primary" runat="server" onserverclick="ExportExcel">Export</button>
        
          <asp:GridView runat="server" ID="GuestListTable" PageSize="5" AllowPaging="true" OnPageIndexChanging="All_PageIndexChanging" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" style="margin-top:5px;" DataKeyNames="Email_Addr" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" GridLines="None" BorderStyle="None">
                 
           <Columns>
               <asp:TemplateField>
                   <ItemTemplate>
                       <%--<button type="button" runat="server" id="Delete" onserverclick="DeleteALLRows" class="btnTransparent"><i class="fas fa-times"></i></button>--%>
                       <asp:LinkButton runat="server" id="Delete" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="DeleteALLRows" CssClass="btnTransparent"><i class="fas fa-times"></i></asp:LinkButton>
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

               <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="50">
                    </controlstyle>
                   </asp:BoundField>
               <asp:BoundField DataField="Full_Name" HeaderText="Full Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="150">
                    </controlstyle>
               </asp:BoundField>
               <asp:BoundField DataField="Email_Addr" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                   <controlstyle Width="200">
                    </controlstyle>
               </asp:BoundField>
               <asp:TemplateField HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1"  style="height:30px; width:100px;" runat="server">
                            <asp:ListItem>Guest</asp:ListItem>
                            <asp:ListItem>Speaker</asp:ListItem>
                            <asp:ListItem>VIP</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Guest_Role") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <%--<asp:BoundField DataField="Guest_Role" HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="110">
                    </controlstyle>
               </asp:BoundField>--%>
               <asp:TemplateField HeaderText="Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="Invite_dt" style="width:200px;"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("INVITE_DT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:BoundField DataField="EVENT_CHARGE" HeaderText="Event Charge" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="100">
                    </controlstyle>
               </asp:BoundField>
               
              <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" CssClass="btn" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="EditAllRows" ID="edit" ><i class="fas fa-edit"></i></asp:LinkButton>
                       <%--<button type="button" runat="server" onserverclick="EditAllRows" id="Edit" class="btnTransparent"><i class="fas fa-edit"></i></button>--%>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

                <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" CssClass="btn" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;"  OnClick="SaveAllRows" ID="save" ><i class="fas fa-check"></i></asp:LinkButton>
                       <%--<button type="button" runat="server" onserverclick="EditAllRows" id="Edit" class="btnTransparent"><i class="fas fa-edit"></i></button>--%>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

           </Columns>
                 <PagerStyle CssClass = "GridPager" />
        </asp:GridView>

        <label style="margin-top:20px; font-size:17px; font-weight:700;">Speaker</label>
         <asp:GridView runat="server" ID="GuestListSpeaker" PageSize="5" AllowPaging="true" OnPageIndexChanging="Speaker_PageIndexChanging" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" style="margin-top:5px;" DataKeyNames="Email_Addr" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" GridLines="None" BorderStyle="None">
                 
           <Columns>
               <asp:TemplateField>
                   <ItemTemplate>
                        <asp:LinkButton runat="server" id="Delete" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="DeleteSpeakerRows" CssClass="btnTransparent"><i class="fas fa-times"></i></asp:LinkButton>
                      
                   </ItemTemplate>
                    <HeaderTemplate>Delete</HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

               <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="50">
                    </controlstyle>
                </asp:BoundField>
               <asp:BoundField DataField="Full_Name" HeaderText="Full Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                    <controlstyle Width="150">
                    </controlstyle>
                </asp:BoundField>
               <asp:BoundField DataField="Email_Addr" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                     <controlstyle Width="200">
                    </controlstyle>
                </asp:BoundField>
               <asp:TemplateField HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1"  style="height:30px; width:100px;" runat="server">
                            <asp:ListItem>Guest</asp:ListItem>
                            <asp:ListItem>Speaker</asp:ListItem>
                            <asp:ListItem>VIP</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Guest_Role") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="Invite_dt" style="width:200px;"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("INVITE_DT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:BoundField DataField="EVENT_CHARGE" HeaderText="Event Charge" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                    <controlstyle Width="100">
                    </controlstyle>
                </asp:BoundField>
               
              <asp:TemplateField>
                   <ItemTemplate>
                     <asp:LinkButton runat="server" CssClass="btn " style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="EditSpeakerRows" ID="edit" ><i class="fas fa-edit"></i></asp:LinkButton>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

                <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" CssClass="btn " style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="SaveSpeakerRows" ID="save" ><i class="fas fa-check"></i></asp:LinkButton>
                       <%--<button type="button" runat="server" onserverclick="EditAllRows" id="Edit" class="btnTransparent"><i class="fas fa-edit"></i></button>--%>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
           </Columns>
                 <PagerStyle CssClass = "GridPager" />
        </asp:GridView>

        <label style="margin-top:20px; font-size:17px; font-weight:700;">Guest</label>
         <asp:GridView runat="server" ID="GuestListGuest" PageSize="5" AllowPaging="true" OnPageIndexChanging="Guest_PageIndexChanging" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" style="margin-top:5px;" DataKeyNames="Email_Addr" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" GridLines="None" BorderStyle="None">
                 
           <Columns>
               <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" id="Delete" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="DeleteGuestRows" CssClass="btnTransparent"><i class="fas fa-times"></i></asp:LinkButton>
                      
                   </ItemTemplate>
                    <HeaderTemplate>Delete</HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

               <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="50">
                    </controlstyle>
               </asp:BoundField>
               <asp:BoundField DataField="Full_Name" HeaderText="Full Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                    <controlstyle Width="150">
                    </controlstyle>
               </asp:BoundField>
               <asp:BoundField DataField="Email_Addr" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <controlstyle Width="200">
                    </controlstyle>
               </asp:BoundField>
               <asp:TemplateField HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1"  style="height:30px; width:100px;" runat="server">
                            <asp:ListItem>Guest</asp:ListItem>
                            <asp:ListItem>Speaker</asp:ListItem>
                            <asp:ListItem>VIP</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Guest_Role") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="Invite_dt" style="width:200px;"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("INVITE_DT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:BoundField DataField="EVENT_CHARGE" HeaderText="Event Charge" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="100">
                    </controlstyle>
               </asp:BoundField>
               
              <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" CssClass="btn" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;"  OnClick="EditGuestRows" ID="edit"><i class="fas fa-edit"></i></asp:LinkButton>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
                <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" CssClass="btn" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="SaveGuestRows" ID="save" ><i class="fas fa-check"></i></asp:LinkButton>
                       <%--<button type="button" runat="server" onserverclick="EditAllRows" id="Edit" class="btnTransparent"><i class="fas fa-edit"></i></button>--%>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
           </Columns>
                 <PagerStyle CssClass = "GridPager" />
        </asp:GridView>

        <label style="margin-top:20px; font-size:17px; font-weight:700;">VIP</label>
        <asp:GridView runat="server" ID="GuestListVIP" PageSize="5" AllowPaging="true" OnPageIndexChanging="VIP_PageIndexChanging" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" style="margin-top:5px;" DataKeyNames="Email_Addr" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" GridLines="None" BorderStyle="None">
                 
           <Columns>
               <asp:TemplateField>
                   <ItemTemplate>
                        <asp:LinkButton runat="server" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" id="Delete" OnClick="DeleteVIPRows" CssClass="btnTransparent"><i class="fas fa-times"></i></asp:LinkButton>
                      
                   </ItemTemplate>
                    <HeaderTemplate>Delete</HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

               <asp:BoundField DataField="Honorific" HeaderText="Honorific" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="50">
                    </controlstyle>
               </asp:BoundField>
               <asp:BoundField DataField="Full_Name" HeaderText="Full Name" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <controlstyle Width="150">
                    </controlstyle>
               </asp:BoundField>
               <asp:BoundField DataField="Email_Addr" HeaderText="Email" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <controlstyle Width="200">
                    </controlstyle>
               </asp:BoundField>
               <asp:TemplateField HeaderText="Role" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1"  style="height:30px; width:100px;" runat="server">
                            <asp:ListItem>Guest</asp:ListItem>
                            <asp:ListItem>Speaker</asp:ListItem>
                            <asp:ListItem>VIP</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Guest_Role") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Date" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="Invite_dt" style="width:200px;"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("INVITE_DT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:BoundField DataField="EVENT_CHARGE" HeaderText="Event Charge" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" >
                   <controlstyle Width="100">
                    </controlstyle>
               </asp:BoundField>
               
              <asp:TemplateField>
                   <ItemTemplate>
                      <asp:LinkButton runat="server" CssClass="btn" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="EditVIPRows" ID="edit"><i class="fas fa-edit"></i></asp:LinkButton>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>

                <asp:TemplateField>
                   <ItemTemplate>
                       <asp:LinkButton runat="server" CssClass="btn" style="display:unset !important; text-align:unset !important; width:unset !important; height:unset !important; padding-top:unset !important; border-radius:unset !important; background-color:transparent !important; color:black !important; transition-duration:unset !important;" OnClick="SaveVIPRows" ID="save" ><i class="fas fa-check"></i></asp:LinkButton>
                       <%--<button type="button" runat="server" onserverclick="EditAllRows" id="Edit" class="btnTransparent"><i class="fas fa-edit"></i></button>--%>
                      
                   </ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" />
               </asp:TemplateField>
           </Columns>
                 <PagerStyle CssClass = "GridPager" />
            <EditRowStyle CssClass="GridViewEditRow" />
        </asp:GridView>
 
   </div>
        </div>
    
</asp:Content>
