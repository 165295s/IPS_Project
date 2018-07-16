<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Maintainence_Management.aspx.cs" Inherits="IPS_Prototype.Maintain_CodeLkUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            search();
            $('#ContentPlaceHolder1_hiddenvalue').val($('#dLabel').text());

            $('#dLabel + ul > li').click(function () {
            // Declare variables 
            var input, filter, table, tr, td, i;
            input = document.getElementById("dLabel");

            filter = input.innerText.toUpperCase();
            console.log("check", filter);
            table = document.getElementById("ContentPlaceHolder1_Maintainence_Table");
            tr = table.getElementsByTagName("tr");

                for (i = 0; i < tr.length; i++) {
                  td = tr[i].getElementsByTagName("td")[0];                           
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
            
        });

        function search() {
            
            // Declare variables 
            var input, filter, table, tr, td, i;
            input = document.getElementById("dLabel");

            filter = input.innerText.toUpperCase();
            console.log("test", filter);
            table = document.getElementById("ContentPlaceHolder1_Maintainence_Table");
            tr = table.getElementsByTagName("tr");

                for (i = 0; i < tr.length; i++) {
                  td = tr[i].getElementsByTagName("td")[0];
                            
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="FailureAlert" class="alert alert-danger">
  <strong>Unsuccessful!</strong> There seems to be an error! Please notify the Administrators.
    </div>
   <label class="title">Code Maintainence</label>
    
    
    
        <div style="margin-bottom:30px;">
         
            <button type="button" class="btn btn-primary SearchIcon" id="UserSearchIcon"><i class="fas fa-search"></i></button>
            <div id="MemberHover">
          <input type="text" class="SearchTxtbox" id="CodeSearchTxtbox"/>
         <a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-primary dropdown-toggle customDropdown">CAREP</a>
                                <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                    
                                    <li><a class="dropdown-item" href="#">CAREP</a></li>
                                   <!-- <li><a class="dropdown-item" href="#">DONAR</a></li>  -->
                                    <li><a class="dropdown-item" href="#">EVENT</a></li>
                                    <li><a class="dropdown-item" href="#">MEMTYPE</a></li>
                                    <li><a class="dropdown-item" href="#">HONORIFIC</a></li>
                                    <li><a class="dropdown-item" href="#">PAYMODE</a></li>
                                    <li><a class="dropdown-item" href="#">SOURCE</a></li>
                                    <li><a class="dropdown-item" href="#">CATEGORY</a></li>
                                    </ul>
                </div>
            <input type="text" id="hiddenvalue" runat="server" style="display:none;" />
             

            <button type="button" class="btn btn-primary right" runat="server" id="AddMaintainence" onserverclick="AddMaintainenceCode"><i class="fas fa-plus"></i></button>
            </div>
       <asp:GridView runat="server" ID="Maintainence_Table" CssClass="table table-hover BlueTable" AutoGenerateColumns="false" AutoGenerateEditButton="false" GridLines="None" UseAccessibleHeader="true" BorderStyle="None" OnRowDeleting="CodeTable_RowDeleting" OnRowEditing="CodeTable_RowEditing">
            <Columns>
                <asp:BoundField DataField="Code_Type" HeaderText="Code Type" />
                <asp:BoundField DataField="Code" HeaderText="Look Up Code" />
                <asp:BoundField DataField="Code_Desc" HeaderText="Code Description" />
                <asp:CommandField EditText="Edit" ShowEditButton="true"/>
                <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    
    
</asp:Content>
