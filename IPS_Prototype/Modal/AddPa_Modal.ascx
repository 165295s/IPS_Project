<%@ Control Language="C#" CodeBehind="AddPa_Modal.ascx.cs" Inherits="IPS_Prototype.DynamicData.FieldTemplates.AddPa_Modal" %>

<%--<asp:Literal runat="server" ID="Literal1" Text="<%# FieldValueString %>" />--%>

<%--<link href="../css/Modal.css" rel="stylesheet" />--%>
<script>
    $(document).ready(function () {

        $("#AddPA").click(function () {
            $("#associateName").text(": " + $("#ContentPlaceHolder1_txtFullName").val());
            $("#associateType").text($("#ContentPlaceHolder1_hiddentext").val());
        });






    });






</script>
<div id="Add_PA" class="modal fade" role="dialog">
    <div class="modal-dialog" style="max-width:860px; ">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">

                <h4 class="modal-title">Add PA</h4>
                <button type="button" class="close" data-dismiss="modal"><i class="far fa-times-circle"></i></button>
            </div>
            <div class="modal-body1" style="padding-top:20px; padding-left:50px; padding-right:50px; padding-bottom:10px;">
                <!--Content goes here -->


                    <div class="form-group">
                        <label for="Associate" runat="server" id="associateType"></label>
                        <label for="AssociateName" id="associateName"></label>

                    </div>
                                <div>

                    <div style="float: left; margin-top:-5px;">
                        <label for="Honorific">Honourific</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlList" runat="server"></asp:DropDownList>
                        </div>



                        <label for="firstName">First Name:</label>
                        <div class="form-group">
                            <input type="text" class="form-control" runat="server" id="txtFirstName" style="width: 350px;" />
                        </div>


                        <label for="surname">Surname:</label>
                        <div class="form-group">
                            <input type="text" class="form-control" runat="server" id="txtSurname" style="width: 350px;" />
                        </div>


                        <div class="form-group">
                            <label for="Email">E-Mail Address:</label>
                        <div class="form-group">
                            <input type="text" class="form-control" runat="server" id="txtEmail" style="width: 350px;" />
                        </div>

                                             <label for="TelephoneNo">Telephone Number:</label>
                        <div class="form-group">
                            <input type="text" class="form-control" runat="server" id="txtTelephone" style="width: 350px;" />
                        </div>

             
                        </div>

                         <button type="button" id="submitPA" runat="server" class="btn btn-primary" onserverclick="Submit_PA"  style="width: 150px; height: 40px;">SAVE</button>

                    </div>

                    <%--<div style="float: left; margin-left:50px; margin-top:-13px;">
       


                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</div>
