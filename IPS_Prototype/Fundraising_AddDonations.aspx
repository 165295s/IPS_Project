<%@ Page Title="" Language="C#" MasterPageFile="~/IPS_Vertical.Master" AutoEventWireup="true" CodeBehind="Fundraising_AddDonations.aspx.cs" Inherits="IPS_Prototype.Fundraising_AddDonations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
  <link href="css/UpdatedModal.css" rel="stylesheet" />
    <script>       
        $(document).ready(function () {
            
            $('[id^=detail-]').hide();
            $('.toggle').click(function () {
                $input = $(this);
                $target = $('#' + $input.attr('data-toggle'));
                $target.slideToggle();
            });

            SearchTextIND();
            SearchTextORG();

            // Show datepicker when donation date selected
            $('#ContentPlaceHolder1_TbDonationDate').datepicker({
                dateFormat: "dd/mm/yy"
            });

            // Display event details if radio button Yes is selected
            $("input[type=radio]").click(function () {
                if ($("input:radio[value='rbYes']").is(":checked")) {
                    $('#dvEvents').toggle().show();
                } else if ($("input:radio[value='rbNo']").is(":checked")) {
                    $('#dvEvents').toggle().hide();
                }
            });

            // Display autocomplete text boxes according to drop down lists
            $("#ContentPlaceHolder1_Donors").change(function () {
                if ($('#ContentPlaceHolder1_Donors option:selected').index() == 0) {
                    $('#ContentPlaceHolder1_organisationname').css("display", "none");
                    $('#ContentPlaceHolder1_individualname').css("display", "block");
                    $('#ContentPlaceHolder1_Prospective').css("display", "none");
                }
                else if ($('#ContentPlaceHolder1_Donors option:selected').index() == 1) {
                    $('#ContentPlaceHolder1_organisationname').css("display", "block");
                    $('#ContentPlaceHolder1_individualname').css("display", "none");
                    $('#ContentPlaceHolder1_Prospective').css("display", "none");
                } else {
                    $('#ContentPlaceHolder1_organisationname').css("display", "none");
                    $('#ContentPlaceHolder1_individualname').css("display", "none");
                    $('#ContentPlaceHolder1_Prospective').css("display", "block");
                }
            });

        });

        //backend
        function hideDonorIndSave() {
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }

        //backend
        function showEvent() {
            $('#dvEvents').toggle().show();
        }

        //default add donations page
        function hideDonorIndEdit() {
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'block');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'none');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'none');
        } 
        function hideDonorOrgSave() {
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');

            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

        function hideDonorProsSave() {
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
       
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }
  
        function SearchTextIND() {
            $.ajax({
                url: "/Fundraising_Autocomplete.asmx/GetFundraisingIndAutoComplete",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ 'txt' : '" + $("#ContentPlaceHolder1_searchindname").val() + "'}",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#ContentPlaceHolder1_searchindname").autocomplete({
                        minLength: 1,
                        source: function (request, response) {
                            response($.map(data.d, function (value, key) {
                                return {
                                    fName: value.firstName,
                                    surname: value.surname,
                                    fullnameNT: value.fullNameNametag,
                                    email: value.email,
                                    existingIndID: value.id
                                }
                            }));
                        },
                        select: function (event, ui) {
                            $('#ContentPlaceHolder1_searchindname').val(ui.item.fullnameNT);
                            $('#ContentPlaceHolder1_hdnExistingIA').val(ui.item.existingIndID);
                            return false;
                        }
                    })
                        .autocomplete("instance")._renderItem = function (ul, item) {
                            return $("<li>")
                                .append("<div>" + item.surname + " " + item.fName + "<br>" + item.email + "</div>")
                                .appendTo(ul);
                        };
                },
                error: function (result) {
                    alert("Error");
                }
            });
        }

        function SearchTextORG() {
            $.ajax({
                url: "/Fundraising_Autocomplete.asmx/GetFundraisingOrgAutoComplete",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ 'txt' : '" + $("#ContentPlaceHolder1_searchorgname").val() + "'}",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#ContentPlaceHolder1_searchorgname").autocomplete({
                        minLength: 1,
                        source: function (request, response) {
                            response($.map(data.d, function (value, key) {
                                return {
                                    Name: value.orgname,
                                    existingCAID: value.orgid,
                                    officeNo: value.officeNo
                                }
                            }));
                        },
                        select: function (event, ui) {
                            $('#ContentPlaceHolder1_searchorgname').val(ui.item.Name);
                            $('#ContentPlaceHolder1_hdnExistingCA').val(ui.item.existingCAID);
                            return false;
                        }
                    })
                        .autocomplete("instance")._renderItem = function (ul, item) {
                            return $("<li>")
                                .append("<div>" + item.Name + "<br>" + item.officeNo + "</div>")
                                .appendTo(ul);
                        };
                },
                error: function (result) {
                    alert("Error");
                }
            });
        }

        //IA show modal w/ event
        function modalViewIND() {
            $('#Member_ViewInd').modal('show');
            $('#dvEvents').toggle().show();
        };
        //IA show modal w/out event
        function modalViewINDNoEvent() {
            $('#Member_ViewInd').modal('show');
        };
        //IA failure w/ event
        function showControlsAfterPostBackCheckedFailure() {
            $('#dvEvents').toggle().show();
            displayFailure();
        }
        //IA success w/ event
        function showControlsAfterPostBackChecked() {
            $('#dvEvents').toggle().show();
            displaySuccess('Successfully saved!');
        }

        //update existing CA no event
        function updateCANoEvent() {
            displaySuccess('Successfully saved for existing CA!');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');

            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

        function updateCANoEventFailure() {
            displayFailure('Make sure the fields contain both donation amount and date!');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');

            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

        //update existing CA w/ event
        function updateCAEvent() {
            displaySuccess('Successfully saved for existing CA!');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');

            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

        function updateCAFailure() {
            displayFailure('Please check and try again!');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');

            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

        //update existing IA no event
        function updateIANoEvent() {
            displaySuccess('Successfully saved for existing IA!');
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }

        function updateIANoEventFailure() {
            displayFailure('Make sure the fields contain both donation amount and date!');
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }
        function deleteIANoEvent() {
            displaySuccess('Successfully deleted for existing IA!');
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }
        function deleteIANoEventFailure() {
            displayFailure('Delete unsuccessful for existing IA!');
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }
        function deleteIAEvent() {
            displaySuccess('Successfully deleted for existing IA!');
            $('#dvEvents').toggle().show();
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }
        function deleteIAEventFailure() {
            displayFailure('Delete unsuccessful for existing IA!');
            $('#dvEvents').toggle().show();
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }
        //update existing IA w/ event
        function updateIAEvent() {
            displaySuccess('Successfully saved for existing IA!');
            $('#dvEvents').toggle().show();
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }

        function updateIAFailure() {
            displayFailure('Please check and try again!');
            $('#dvEvents').toggle().show();
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }
       
        //CA show modal w/ event
        function modalViewORG() {
            $('#Member_ViewoRG').modal('show');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");
        };
        //CA show modal w/out event
        function modalViewORGNoEvent() {
            $('#Member_ViewoRG').modal('show');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");
        };
        //CA failure w/ event
        function showControlsAfterPostBackCheckedFailureOrg() {
            $('#dvEvents').toggle().show();
            displayFailure();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");
        }
        //CA success w/ event
        function showControlsAfterPostBackCheckedOrg() {
            $('#dvEvents').toggle().show();
            displaySuccess('Successfully saved!');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");
        }
        //CA show modal w/out event failure
        function showControlsAfterPostBackCheckedNoEventFailureOrg() {
            displayFailure();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");
        };
        //CA success w/out event
        function showControlsAfterPostBackCheckedOrgNoEvent() {
            displaySuccess('Successfully saved!');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");
        }
        //Prospective show modal w/ event 
        function modalViewProspective() {
            $('#Member_ViewProspective').modal('show');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
        };
        //Prospective show modal w/ event failure
        function modalViewProspectiveFailure() {
            displayFailure();
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
        };
        //Prospective show modal w/out event 
        function modalViewProspectiveNoEvent() {
            $('#Member_ViewProspective').modal('show');
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
        };
        //Prospective show modal w/out event failure
        function modalViewProspectiveFailureNoEvent() {
            displayFailure();
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
        };
        //Prospective success w/ event
        function ProspectiveEventSuccess() {
            $('#dvEvents').toggle().show();
            displaySuccess('Successfully saved!');
             $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
        }   
        //Prospective success w/out event
         function ProspectiveNoEventSuccess() {
            displaySuccess('Successfully saved!');
             $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
        }   
           //show modal no event
        function showIANoEventmodal() {
            $('#Member_DeleteInd').modal('show');
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }

        function showIAEventmodal() {
            $('#Member_DeleteInd').modal('show');
            $('#dvEvents').toggle().show();
            $("#ContentPlaceHolder1_submitIndDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteIndDonation").css('display', 'block');
            $("#ContentPlaceHolder1_individualMoreInfo").css('display', 'none');
        }

        function showCANoEventmodal() {
            $('#Member_DeleteOrg').modal('show');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

        function showCAEventmodal() {
            $('#Member_DeleteOrg').modal('show');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

        function deleteCANoEvent() {
            displaySuccess('Successfully deleted for existing CA!');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }
        function deleteCANoEventFailure() {
            displayFailure('Delete unsuccessful for existing CA!');
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }
        function deleteCAEvent() {
            displaySuccess('Successfully deleted for existing CA!');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }
        function deleteCAEventFailure() {
            displayFailure('Delete unsuccessful for existing CA!');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "block");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "none");

            $("#ContentPlaceHolder1_submitOrgDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteOrgDonation").css('display', 'block');
            $("#ContentPlaceHolder1_organisationMoreInfo").css('display', 'none');
        }

          //update prospectives no event
        function updateProspectivesNoEvent() {
            displaySuccess('Successfully saved for prospectives!');

            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");

            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }

        function updateProspectivesNoEventFailure() {
            displayFailure('Make sure the fields contain both donation amount and date!');

            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");

            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }

        //update existing CA w/ event
        function updateProspectivesEvent() {
            displaySuccess('Successfully saved for prospectives!');
            $('#dvEvents').toggle().show();

            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");

            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }

        function updateProspectivesFailure() {
            displayFailure('Please check and try again!');
            $('#dvEvents').toggle().show();

            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");

            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }

        function showProspectivesEventmodal() {
            $('#Member_DeleteProspectives').modal('show');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }

        function showProspectivesNoEventmodal() {
            $('#Member_DeleteProspectives').modal('show');
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }

          function deleteProspectivesNoEvent() {
            displaySuccess('Successfully deleted for prospectives!');
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }
        function deleteProspectivesNoEventFailure() {
            displayFailure('Delete unsuccessful for prospectives!');
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }
        function deleteProspectivesEvent() {
            displaySuccess('Successfully deleted for prospectives!');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }
        function deleteProspectivesEventFailure() {
            displayFailure('Delete unsuccessful for prospectives!');
            $('#dvEvents').toggle().show();
            $('#ContentPlaceHolder1_organisationname').css("display", "none");
            $('#ContentPlaceHolder1_individualname').css("display", "none");
            $('#ContentPlaceHolder1_Prospective').css("display", "block");
            $("#ContentPlaceHolder1_submitProspectiveDonation").css('display', 'none');
            $("#ContentPlaceHolder1_UpdateProsDonation").css('display', 'block');
            $("#ContentPlaceHolder1_DeleteProspectives").css('display', 'block');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hdnDonationID" runat="server" />
    <asp:HiddenField ID="hdnProsID" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <label class="title">Fundraising Campaign</label>

    <br />
    <br />
      <div id="SuccessAlert" class="alert alert-success">
        <strong>Success!</strong>
        <label id="SuccessMsg"></label>
    </div>
    <div id="FailureAlert" class="alert alert-danger">
        <strong>Unsuccessful!</strong> Please check if the fields are correct.
    </div>

    <table>
        <tr>
            <td>
                <label style="margin-left: 80px; margin-right: 10px" for="lblDonor">Donor:</label></td>
            <td>
                <select class="form-control" name="Donors" id='Donors' runat="server" style="margin-top: -7px;">
                    <option value="Existing Individual Associate">Existing Individual Associate</option>
                    <option value="Existing Corporate Associate">Existing Corporate Associate</option>
                    <option value="Prospective">Prospective</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <label style="margin-top: 20px;">Donation Amount:</label>
            </td>
            <td>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span style="margin-top: 20px;" class="input-group-text">$</span>
                    </div>
                    <input class="form-control" style="margin-top: 20px;" type="number" id="TbDonationAmt" runat="server">
                </div>
            </td>
            <td>
                <label style="margin-left: 86px; margin-right: 10px; margin-top: 20px;">Donation Date:</label></td>
            <td>
                <input runat="server" style="margin-top: 15px; width: 258px" type="text" class="form-control" id="TbDonationDate" />
            </td>
        </tr>
    </table>

    <div style="display: inline">
        <div style="margin-top: 20px; " class="form-group">
            <label style="margin-left: -5px; margin-right: 17px;" for="lblEventsRelated">Related To Events:</label>
            <div style="margin-left: -5px; margin-right: 10px;" class="form-check form-check-inline">
                <label class="radio-inline">
                    <input type="radio" name="rbEvent" id="rbNo" runat="server" checked value="rbNo">No</label>
            </div>
            <div class="form-check form-check-inline">
                <label class="radio-inline">
                    <input type="radio" name="rbEvent" id="rbYes" runat="server" value="rbYes">Yes</label>
            </div>
        </div>
    </div>

    <div class="input-group" style="display: none;" id="dvEvents">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <label style="margin-top:13px; margin-left: 45px; margin-right: 10px;" for="lblEventType">Event Type:</label>
                        </td>
                        <td>
                            <div class="btn-group btn-group-inline">
                                <asp:DropDownList Style="margin-top: 7px; width: auto" class="pull-left form-control" ID="ddlType" runat="server" DataValueField="CODE" AutoPostBack="true" DataTextField="CODE_DESC" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList Style="margin-top: 7px;" class="pull-left form-control" ID="ddlSubType" runat="server" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                </table>

                <div class="btn-group btn-group-inline">
                    <label style="margin-left: 15px; margin-top: 31px; margin-right: 12px">Start Date/Time:</label>
                    <input runat="server" disabled="disabled" style="margin-top: 25px; width: 258px;" type="text" class="form-control" id="startdatetime" />
                    <label style="margin-top: 31px; margin-right: 10px !important; margin-left: 84px !important;">End Date/Time:</label>
                    <input runat="server" disabled="disabled" style="margin-top: 25px; width: 258px" type="text" class="form-control" id="enddatetime" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSubType" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

    </div>

    <div runat="server" id="individualname">
        <table style="margin-top: 20px">
            <tr>
                <td>
                    <label style="margin-top: 10px; margin-left: 11px; margin-right: 10px">Individual Name:</label>
                </td>
                <td>
                                                <div class="btn-group btn-group-inline">
                    <input runat="server" onkeyup="SearchTextIND()" style="margin-top: 5px; width: 258px" type="text" class="form-control" placeholder="Search Individual Name" id="searchindname" />
                    <button type="button" id="individualMoreInfo" onserverclick="individualMoreInfo_ServerClick" runat="server" style="margin-top: 5px;margin-left: 5px; background-color: #5bc0de; border-color: #46b8da;" class="btn btn-info"><i class="fas fa-info-circle" style="margin-right: 5px;"></i>More Info</button>
               </div>
                                                    </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <button type="button" class="btn" id="submitIndDonation" onserverclick="submitIndDonation_ServerClick" runat="server" style="color:#fff; border-radius: 4px; border: none; line-height:normal; outline:none !important; margin: 0 5px; border-radius: 3px; background-color:#5d9cec; min-height: 40px; margin-top: 30px;"><i class="far fa-save" style="margin-right: 5px;" color:"#fff;"></i>Save Individual</button>
                    <button type="button" class="btn" id="UpdateIndDonation" onserverclick="UpdateIndDonation_ServerClick" runat="server" style="color:#fff; border-radius: 4px; border: none; line-height:normal; outline:none !important; border-radius: 3px; background-color:#5d9cec; min-height: 40px; margin-top: 30px;"><i class="far fa-edit" style="margin-right: 5px;" color:"#fff;"></i>Update Individual</button>
                </td>
                <td>
                    <button type="button" class="btn" id="DeleteIndDonation" onserverclick="DeleteIndDonation_ServerClick" runat="server" style="color:#fff; margin-left:-80px; border-radius: 4px; border: none; line-height:normal; outline:none !important; border-radius: 3px; background-color:#f15e5e; min-height: 40px; margin-top: 30px; width:170px;"><i class="fas fa-trash-alt" style="margin-right: 5px;" color:"#fff;"></i>Delete Individual</button>
                </td>
            </tr>
        </table>
    </div>

    <div runat="server" id="organisationname" style="display: none;">
        <table style="margin-top: 20px">
            <tr>
                <td>
                    <label style="margin-top: 10px; margin-left: -12px; margin-right: 10px">Organisation Name:</label>
                </td>
                <td>
                      <div class="btn-group btn-group-inline">
                    <input runat="server" onkeyup="SearchTextORG()" style="margin-top: 5px; width: 258px" type="text" class="form-control" placeholder="Search Organisation Name" id="searchorgname" />
                    <button type="button" id="organisationMoreInfo" onserverclick="organisationMoreInfo_ServerClick" runat="server" style="margin-top: 5px; margin-left: 5px; background-color: #5bc0de; border-color: #46b8da;" class="btn btn-info"><i class="fas fa-info-circle" style="margin-right: 5px;"></i>More Info</button>
               </div>
                          </td>
            </tr>
             <tr>
                <td></td>
                <td>
                    <button type="button" class="btn" id="submitOrgDonation" onserverclick="submitOrgDonation_ServerClick" runat="server" style="color:#fff; border-radius: 4px; border: none; line-height:normal; outline:none !important; margin: 0 5px; border-radius: 3px; background-color:#5d9cec; min-height: 40px; margin-top: 30px;"><i class="far fa-save" style="margin-right: 5px;" color:"#fff;"></i>Save Corporate</button>
                   <button type="button" class="btn" id="UpdateOrgDonation" runat="server" onserverclick="UpdateOrgDonation_ServerClick" style="color:#fff; border-radius: 4px; border: none; line-height:normal; outline:none !important; border-radius: 3px; background-color:#5d9cec; min-height: 40px; margin-top: 30px; margin-right:205px;"><i class="far fa-edit" style="margin-right: 5px;" color:"#fff;"></i>Update Corporate</button>
                </td>
                <td>
                    <button type="button" class="btn" id="DeleteOrgDonation" onserverclick="DeleteOrgDonation_ServerClick" runat="server" style="color:#fff; margin-left:-197px; border-radius: 4px; border: none; line-height:normal; outline:none !important; border-radius: 3px; background-color:#f15e5e; min-height: 40px; margin-top: 30px; width:170px;"><i class="fas fa-trash-alt" style="margin-right: 5px;" color:"#fff;"></i>Delete Corporate</button>
                </td>
            </tr>
        </table>
    </div>

     <div runat="server" id="Prospective" style="display: none;">
        <table style="margin-top: 20px">
             <tr>
                <td></td>
                <td>
                    <button type="button" id="submitProspectiveDonation" onserverclick="submitProspectiveDonation_ServerClick" runat="server" class="btn" style="color:#fff; border-radius: 4px; border: none; line-height:normal; outline:none !important; margin: 0 5px; border-radius: 3px; background-color:#0094ff!important; min-height: 40px; margin-top: 11px; margin-left:148px"><i class="fas fa-plus" style="margin-right: 5px;"></i>Add Prospective</button>
                    <button type="button" class="btn" id="UpdateProsDonation" onserverclick="UpdateProsDonation_ServerClick" runat="server" style="color:#fff; border-radius: 4px; border: none; line-height:normal; outline:none !important; border-radius: 3px; background-color:#5d9cec; min-height: 40px; margin-right:205px; margin-left:143px;"><i class="far fa-edit" style="margin-right: 5px;" color:"#fff;"></i>Update Prospective</button>
                  </td>
                  <td><%--DeleteProspectives--%>
                    <button type="button" class="btn" id="DeleteProspectives" onserverclick="DeleteProspectives_ServerClick" runat="server" style="color:#fff; margin-left:-197px; border-radius: 4px; border: none; line-height:normal; outline:none !important; border-radius: 3px; background-color:#f15e5e; min-height: 40px; width:180px;"><i class="fas fa-trash-alt" style="margin-right: 5px;" color:"#fff;"></i>Delete Prospective</button>
                </td>
            </tr>
        </table>
    </div>

     <%--   Modal for Individual Associate--%>
        <div class="modal fade" id="Member_ViewInd" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 950px;" role="document">
                <div class="modal-content">
                    <div class="modal-header">     
                        <h4 class="modal-title center" style="margin-left:283px"><u>Individual Associate Detail</u></h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body" style="margin-top: 10px;">
                        <p>See below for further information on <asp:Label Font-Underline="true" ID="LblIndName2" runat="server" />:</p>
                        <asp:HiddenField ID="hdnExistingIA" runat="server" />
                        <div class="row">
                            <div style="width: 300px">
                                <p class="lead"><i class="fas fa-user" style="margin-right:5px"></i><u>Personal</u></p>
                                 <div class="form-group">
                                    <span class="fa fa-check text-success"></span>
                                    <asp:Label Text="Given Name:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndFirstName" runat="server" />
                                </div>
                                 <div class="form-group">
                                    <span class="fa fa-check text-success"></span>
                                    <asp:Label Text="Surname:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndLastName" runat="server" />
                                </div>
                                   <div class="form-group">
                                    <span class="fa fa-check text-success"></span>
                                    <asp:Label Text="Full Name:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndName" runat="server" />
                                </div>
                                  <div class="form-group">
                                    <asp:Label Text="Gender:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndGender" runat="server" />
                                </div>
                                  <div class="form-group">
                                    <asp:Label Text="Honorific:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndHonorific" runat="server" />
                                </div>
                                  <div class="form-group">
                                    <asp:Label Text="Salutation:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndSalutation" runat="server" />
                                </div>
                                  <div class="form-group">
                                    <asp:Label Text="Nationality:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndNationality" runat="server" />
                                </div>
                                 <div class="form-group">
                                    <asp:Label Text="Special Dietary Requirement:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndSDR" runat="server" />
                                </div>
                            </div>
                            <div style="width: 300px">
                                <p class="lead"><i class="fas fa-phone-square" style="margin-right:5px"></i><u>Contact</u></p>
                                <div class="form-group">
                                    <span class="fa fa-check text-success"></span>
                                    <asp:Label Text="Email:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndEmail" runat="server" />
                                </div>
                                <div class="form-group">
                                    <span class="fa fa-check text-success"></span>
                                    <asp:Label Text="Telepone No:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndTelephone" runat="server" />
                                </div>
                            </div>
                            <div style="width: 300px">
                                <p class="lead"><i class="fas fa-plus-circle" style="margin-right:5px"></i><u>Additional</u></p>
                                <div class="form-group">
                                    <asp:Label Text="Designation:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndDesignation" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Department:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndDepartment" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Organisation:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndOrganisation" runat="server" />
                                </div>
                                 <div class="form-group">
                                    <asp:Label Text="Designation 2:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndDesignation2" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Department 2:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndDepartment2" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Organisation 2:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndOrganisation2" runat="server" />
                                </div>
                                  <div class="form-group">
                                    <asp:Label Text="Source:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndSource" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Cat 1:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndCat1" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Cat 2:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblIndCat2" runat="server" />
                                </div>
                               
                            </div>
                            </div>
                        </div>

                    <div class="modal-footer">
                    </div>

                    </div>
                </div>
                </div>
  
    <%--   Modal for Corporate Associate--%>
        <div class="modal fade" id="Member_ViewoRG" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 950px;" role="document">
                <div class="modal-content">
                    <div class="modal-header">     
                        <h4 class="modal-title center" style="margin-left:283px"><u>Corporate Associate Detail</u></h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body" style="margin-top: 10px;">
                        <p>See below for further information on <asp:Label Font-Underline="true" ID="LblOrgName1" runat="server" />:</p>

                        <asp:HiddenField ID="hdnExistingCA" runat="server" />

                        <div class="row">

                            <div style="width: 300px">
                                <div class="well">
                                    <p class="lead"><i class="fas fa-building" style="margin-right:5px"></i><u>Organisation</u></p>
                                    <div class="form-group">
                                        <span class="fa fa-check text-success"></span>
                                        <asp:Label Text="Name:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgName" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label Text="Website:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgWebsite" runat="server" />
                                    </div>
                                       <div class="form-group">
                                        <asp:Label Text="Business Description:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgBizDesc" runat="server" />
                                    </div>
                                           <div class="form-group">
                                        <asp:Label Text="UEN:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgUEN" runat="server" />
                                    </div>
                                           <div class="form-group">
                                        <asp:Label Text="Notes:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgNotes" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div style="width: 300px">
                                <p class="lead"><i class="fas fa-phone-square" style="margin-right:5px"></i><u>Contact</u></p>
                                  <div class="form-group">
                                        <span class="fa fa-check text-success"></span>
                                        <asp:Label Text="Telepone No:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgTelephone" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <span class="fa fa-check text-success"></span>
                                        <asp:Label Text="Office No:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgOffice" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <span class="fa fa-check text-success"></span>
                                        <asp:Label Text="Point of Contact:" CssClass="font-weight-bold" runat="server" />
                                        <asp:Label ID="LblOrgPointOfContact" runat="server" />
                                    </div>
                            </div>

                            <div style="width: 300px">
                                <p class="lead"><i class="fas fa-address-card" style="margin-right:5px"></i><u>Address</u></p>
                                <div class="form-group">
                                    <asp:Label Text="Line 1:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblOrgMailingAddLine1" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Line 2:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblOrgMailingAddLine2" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="City:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblOrgMailingCity" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label Text="Postal:" CssClass="font-weight-bold" runat="server" />
                                    <asp:Label ID="LblOrgMailingPostal" runat="server" />
                                </div>
                            </div>

                        </div>

              </div>

                    <div class="modal-footer">
                    </div>

                    </div>
                </div>
            </div>
 
     <%--   Modal for Prospective Member--%>
        <div class="modal fade" id="Member_ViewProspective" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 950px;" role="document">
                <div class="modal-content">
                    <div class="modal-header">     
                        <h4 class="modal-title center" style="margin-left:347px"><u>Add Prospective</u></h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body" style="margin-top: 10px;">
                        <asp:HiddenField ID="HiddenField1" runat="server" />

                        <div class="wrapper">

                            <div class="left_div" style="float: left; margin-left: 40px; margin-top: -27px;">

                                <label for="Honorific">Honorific:</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="prospective_ddlHonorific" style="display:inline-block;" runat="server" class="ddlStyle"></asp:DropDownList>
                                </div>

                                <label for="Saluation">Saluation:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtSalutationField" style="width: 350px;" />
                                </div>

                                <label for="firstName">First Name:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtFirstName" style="width: 350px;" required />
                                </div>

                                <label for="surname">Surname:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtSurname" style="width: 350px;" />
                                </div>
                                <label for="fullNameNT">Full Name Nametag:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtFullNameNameTag" style="width: 350px;" />
                                </div>

                                <label for="Nationality">Nationality</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="prospective_ddlNationality" runat="server" class="ddlStyle">
                                        <asp:ListItem Text="Singapore" Value="SGP"></asp:ListItem>
                                        <asp:ListItem Text="Malaysia" Value="MYS"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label for="Gender">Gender:</label>
                                <div style="display: inline;">
                                    <div class="form-group">
                                        <div class="form-check form-check-inline" style="margin-bottom: 9px">
                                            <label class="radio-inline">
                                                <input type="radio" id="prospective_Male" name="optradio" runat="server" value="M">Male</label>
                                        </div>
                                        <div class="form-check form-check-inline"  style="margin-bottom: 9px">
                                            <label class="radio-inline">
                                                <input type="radio" id="prospective_Female" name="optradio" runat="server" value="F">Female</label>
                                        </div>
                                    </div>
                                </div>


                                <label for="Email">E-Mail Address:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtEmail" style="width: 350px;" />
                                </div>


                                <label for="SDR">Special Dietary Restriction:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtSDR" style="width: 350px;" />
                                </div>
                            </div>


                            <div class="right_div" style="float: left; margin-top: -27px; margin-left: 100px;">

                                <label for="Source">Source:</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="prospective_ddlSource" runat="server" Class="ddlStyle"></asp:DropDownList>
                                </div>

                                <label for="Cat1">Cat 1:</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="prospective_ddlCat1" runat="server" Class="ddlStyle"></asp:DropDownList>
                                </div>
                                <label for="Cat2">Cat 2:</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="prospective_ddlCat2" runat="server" Class="ddlStyle"></asp:DropDownList>
                                </div>


                                <label for="TelephoneNo">Telephone Number:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtTelephone" style="width: 350px;" />
                                </div>

                                <label for="organization1">Organization 1:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtOrg1" style="width: 350px;" />
                                </div>

                                <label for="department1">Department 1:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtDept1" style="width: 350px;" />
                                </div>

                                <label for="designation1">Designation 1:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtDesig1" style="width: 350px;" />
                                </div>

                                <label for="organization2">Organization 2:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtOrg2" style="width: 350px;" />
                                </div>

                                <label for="department2">Department 2:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtDept2" style="width: 350px;" />
                                </div>

                                <label for="designation2">Designation 2:</label>
                                <div class="form-group">
                                    <input type="text" class="form-control" runat="server" id="prospective_txtDesig2" style="width: 350px;" />
                                </div>

                               
                            </div>

                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" style="color:#fff; border-radius: 4px; border: none; line-height:normal; outline:none !important; margin: 0 5px; border-radius: 3px; background-color:#5d9cec; min-height: 40px; margin-right: 315px" class="btn" runat="server" id="btnProceedProspectiveMember" onserverclick="btnProceedProspectiveMember_ServerClick"><i class="far fa-save" style="margin-right: 5px;" color:"#fff;"></i>Save</button>
                    </div>

                </div>
            </div>
        </div>

      <div class="modal fade" id="Member_DeleteInd" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 700px;" role="document">
                <div class="modal-content">

                    <div class="modal-header">
                        <div class="icon-box">
                            <i class="material-icons">&#xE5CD;</i>
                        </div>         
                        <h4 class="modal-title" style="margin-top:85px;margin-left:233px;">Are you sure?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>

                    <div class="modal-body" style="margin-top: 10px;">
                        <p>This donation made by <label for="lblmodaltitlenameInd" runat="server" id="lblmodaltitlenameInd"></label> would be deleted.</p>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" style="margin-right:189px" class="btn btn-danger" runat="server" onserverclick="btnDeleteInd_ServerClick" ID="btnDeleteInd" >Remove</button>
                    </div>
                </div>
            </div>
        </div>

      <div class="modal fade" id="Member_DeleteOrg" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 700px;" role="document">
                <div class="modal-content">

                    <div class="modal-header">
                        <div class="icon-box">
                            <i class="material-icons">&#xE5CD;</i>
                        </div>         
                        <h4 class="modal-title" style="margin-top:85px;margin-left:233px;">Are you sure?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>

                    <div class="modal-body" style="margin-top: 10px;">
                        <p>This donation made by <label for="lblmodaltitlenameOrg" runat="server" id="lblmodaltitlenameOrg"></label> would be deleted.</p>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" style="margin-right:189px" class="btn btn-danger" runat="server" ID="btnDeleteOrg" onserverclick="btnDeleteOrg_ServerClick" >Remove</button>
                    </div>
                </div>
            </div>
        </div>

     <div class="modal fade" id="Member_DeleteProspectives" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-confirm" style="max-width: 700px;" role="document">
                <div class="modal-content">

                    <div class="modal-header">
                        <div class="icon-box">
                            <i class="material-icons">&#xE5CD;</i>
                        </div>         
                        <h4 class="modal-title" style="margin-top:85px;margin-left:233px;">Are you sure?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>

                    <div class="modal-body" style="margin-top: 10px;">
                        <p>This donation made by <label for="lblmodaltitlenameProspectives" runat="server" id="lblmodaltitlenameProspectives"></label> would be deleted.</p>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" style="margin-right:189px" class="btn btn-danger" runat="server" ID="btnDeleteProspectives" onserverclick="btnDeleteProspectives_ServerClick" >Remove</button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
