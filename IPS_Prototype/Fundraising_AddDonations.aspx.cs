using IPS_Prototype.Class;
using IPS_Prototype.DAL;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class Fundraising_AddDonations : System.Web.UI.Page
    {
        private ArrayList prospectiveList;

        DALFundraising fundraising = new DALFundraising();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                prospectiveList = (ArrayList)Session["Prospective"];

                BindEvent();

                MembershipDAO d1 = new MembershipDAO();
                DataTable DT = new DataTable();
                DT = d1.GetLookupSearch("HONOURIFIC");
                prospective_ddlHonorific.DataSource = DT;
                prospective_ddlHonorific.DataTextField = "Code_Desc";
                prospective_ddlHonorific.DataValueField = "Code"; //When insert, this value
                prospective_ddlHonorific.DataBind();
                //Donors
                DT = fundraising.GetSource();
                prospective_ddlSource.DataSource = DT;
                prospective_ddlSource.DataTextField = "source";
                prospective_ddlSource.DataValueField = "source";
                prospective_ddlSource.DataBind();
                prospective_ddlSource.SelectedValue = "Acad_TT";

                DT = fundraising.GetCat2();
                prospective_ddlCat2.DataSource = DT;
                prospective_ddlCat2.DataTextField = "Code_Desc";
                prospective_ddlCat2.DataTextField = "Code";
                prospective_ddlCat2.DataBind();

                DT = fundraising.GetCat1(prospective_ddlSource.SelectedValue);
                prospective_ddlCat1.DataSource = DT;
                prospective_ddlCat1.DataTextField = "cat_1";
                prospective_ddlCat1.DataTextField = "cat_1";
                prospective_ddlCat1.DataBind();

                if (Session["check"].ToString() == "Select")
                {
                    var indEdit = Session["DonorIndEdit"]?.ToString() ?? string.Empty;
                    var orgEdit = Session["DonorOrgEdit"]?.ToString() ?? string.Empty;
                    var prodEdit = Session["DonorProsEdit"]?.ToString() ?? string.Empty;
                    string donationId = string.Empty;
                    int personId = -1;

                    if (!string.IsNullOrWhiteSpace(indEdit) && (orgEdit == ""))
                    {
                        Donors.SelectedIndex = 0;
                        donationId = Session["DonorIndEditID"].ToString();

                        Session["DonorInd"] = null;
                        // IF Session not null means that page is triggered by Fundraising management page 
                        hdnExistingIA.Value = indEdit;
                        personId = int.Parse(hdnExistingIA.Value);
                        PersonModel personmodal = new PersonModel();
                        personmodal = fundraising.GetPersonData(personId);
                        searchindname.Value = personmodal.fullNameNametag;
                        searchindname.Disabled = true;
                        Donors.Disabled = true;
                        Session["DonorIndEdit"] = "";
                    }
                    else if (!string.IsNullOrWhiteSpace(orgEdit) && (indEdit == ""))
                    {
                        Donors.SelectedIndex = 1;
                        donationId = Session["DonorOrgEditID"].ToString();

                        Session["DonorOrg"] = null;
                        hdnExistingCA.Value = orgEdit;
                        personId = int.Parse(hdnExistingCA.Value);
                        OrganisationModel organisationModel = new OrganisationModel();
                        organisationModel = fundraising.GetOrgData(personId);
                        searchorgname.Value = organisationModel.orgname;
                        searchorgname.Disabled = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorOrgSave();", true);
                        Donors.Disabled = true;
                        Session["DonorOrgEdit"] = "";
                    }
                    else
                    {
                        Donors.SelectedIndex = 2;
                        donationId = Session["DonorProsEditID"].ToString();

                        Session["DonorPros"] = null;
                        hdnProsID.Value = prodEdit;
                        personId = int.Parse(hdnProsID.Value);
                        ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorProsSave();", true);
                        Donors.Disabled = true;
                        Session["DonorProsEdit"] = "";
                    }
                    //                    ShowDonationInfo(sender, e, donationId, personId);
                    ShowDonationInfo(sender, e, donationId);
                }
                else
                {
                    //shows save btn for existing IA
                    ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndEdit();", true);
                    Session["check"] = "";
                }



            }
        }
        private void ShowDonationInfo(object sender, EventArgs e, string donationId)
        {
            //show ind name + disable for existing IA 
            //PersonModel personmodal = new PersonModel();
            //personmodal = fundraising.GetPersonData(personId);
            //searchindname.Value = personmodal.fullNameNametag;
            //searchindname.Disabled = true;

            hdnDonationID.Value = donationId;
            EventInfo fr = new EventInfo();
            fr = fundraising.GetFundraisingData(int.Parse(hdnDonationID.Value));
            TbDonationAmt.Value = fr.DonationAmt.ToString();
            TbDonationDate.Value = fr.DonationDate.ToString("dd/MM/yyyy");
            ddlType.SelectedValue = fr.EventType;
            ddlType_SelectedIndexChanged(sender, e);
            ddlSubType.SelectedValue = fr.EventName;
            ddlSubType_SelectedIndexChanged(sender, e);
            if (!string.IsNullOrWhiteSpace(fr.EventName))
            {
                submitIndDonation.Visible = false;
                submitOrgDonation.Visible = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "script45", "showEvent();", true);
                rbYes.Checked = true;
                rbNo.Disabled = true;
                startdatetime.Value = fr.EventStartDate.ToString();
                enddatetime.Value = fr.EventEndDate.ToString();
            }
            else
            {
                rbNo.Checked = true;
                rbYes.Disabled = true;
            }
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndSave();", true);
            Session["check"] = "";
        }

        private void BindEvent()
        {
            var allCat = fundraising.GetAllType("EVENT");
            ddlType.DataSource = allCat;
            ddlType.DataTextField = "Code_Desc";
            ddlType.DataValueField = "Code";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("Select a type", "0"));
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = fundraising.GetEventName(ddlType.SelectedValue.ToString());
            ddlSubType.DataSource = dt;
            ddlSubType.DataTextField = "NAME";
            ddlSubType.DataBind();
            ddlSubType.Items.Insert(0, new ListItem("Select an event", "0"));
            startdatetime.Value = "";
            enddatetime.Value = "";
        }

        protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var eventTypeCode = ddlSubType.SelectedValue;
            if (!string.IsNullOrEmpty(eventTypeCode) && !eventTypeCode.StartsWith("0"))
            {
                DataTable dataTable = fundraising.GetEventTbDates(eventTypeCode);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    startdatetime.Value = dataTable.Rows[0][0].ToString();
                    enddatetime.Value = dataTable.Rows[0][1].ToString();
                }
                else
                {
                    startdatetime.Value = "";
                    enddatetime.Value = "";
                }
            }
            else
            {
                startdatetime.Value = "";
                enddatetime.Value = "";
            }
        }

        //Existing Individual Associate clicking 'Save' button
        protected void submitIndDonation_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndEdit();", true);

            if (rbYes.Checked)
            {

                if (!string.IsNullOrEmpty(TbDonationAmt.Value) && !string.IsNullOrEmpty(TbDonationDate.Value)
                  && !string.IsNullOrEmpty(startdatetime.Value) && !string.IsNullOrEmpty(searchindname.Value))
                {
                    int createindcheck = 0;
                    EventInfo eventInfo = new EventInfo();
                    eventInfo.DonationAmt = decimal.Parse(TbDonationAmt.Value);
                    eventInfo.DonationDate = DateTime.Parse(TbDonationDate.Value);
                    eventInfo.CreatedDate = DateTime.Now;
                    eventInfo.EventName = ddlSubType.Text;
                    eventInfo.EventType = ddlType.Text;
                    eventInfo.EventStartDate = DateTime.Parse(startdatetime.Value);
                    eventInfo.EventEndDate = DateTime.Parse(enddatetime.Value);
                    eventInfo.PersonId = int.Parse(hdnExistingIA.Value);

                    createindcheck = fundraising.InsertIndividualDonation(eventInfo);
                    if (createindcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackChecked();", true);
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        ddlType.SelectedIndex = 0;
                        ddlSubType.SelectedIndex = 0;
                        startdatetime.Value = "";
                        enddatetime.Value = "";
                        searchindname.Value = "";
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedFailure();", true);
                }
            }
            else if (rbNo.Checked)
            {
                if (!string.IsNullOrEmpty(TbDonationAmt.Value) && !string.IsNullOrEmpty(TbDonationDate.Value)
                    && !string.IsNullOrEmpty(searchindname.Value))
                {
                    int createindcheck = 0;
                    EventInfo eventInfo = new EventInfo();
                    eventInfo.DonationAmt = decimal.Parse(TbDonationAmt.Value);
                    eventInfo.DonationDate = DateTime.Parse(TbDonationDate.Value);
                    eventInfo.CreatedDate = DateTime.Now;
                    eventInfo.PersonId = int.Parse(hdnExistingIA.Value);

                    createindcheck = fundraising.InsertIndividualDonationWithoutEvent(eventInfo);

                    if (createindcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully saved!');", true);
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        searchindname.Value = "";
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
        }
        //Viewing Individual Associate Details
        protected void individualMoreInfo_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndEdit();", true);
            if (rbYes.Checked)
            {
                PersonModel personmodal = new PersonModel();
                if (!string.IsNullOrEmpty(searchindname.Value))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalViewIND();", true);
                    int indid = int.Parse(hdnExistingIA.Value);
                    if (indid != 0)
                    {
                        personmodal = fundraising.GetPersonData(indid);
                        if (personmodal.email != null)
                        {
                            //personal
                            LblIndName2.Text = personmodal.fullNameNametag;
                            LblIndFirstName.Text = personmodal.firstName;
                            LblIndLastName.Text = personmodal.surname;
                            LblIndName.Text = personmodal.fullNameNametag;
                            LblIndGender.Text = personmodal.gender;
                            LblIndHonorific.Text = personmodal.honorific;
                            LblIndSalutation.Text = personmodal.salutation;
                            LblIndNationality.Text = personmodal.nationality;
                            LblIndSDR.Text = personmodal.SDR;
                            //contact
                            LblIndTelephone.Text = personmodal.telNum;
                            LblIndEmail.Text = personmodal.email;
                            //additional
                            LblIndDesignation.Text = personmodal.designation1;
                            LblIndDepartment.Text = personmodal.department1;
                            LblIndOrganisation.Text = personmodal.organisation1;
                            LblIndDesignation2.Text = personmodal.designation2;
                            LblIndDepartment2.Text = personmodal.department2;
                            LblIndOrganisation2.Text = personmodal.organisation2;
                            LblIndSource.Text = personmodal.source;
                            LblIndCat1.Text = personmodal.cat1;
                            LblIndCat2.Text = personmodal.cat2;
                        }
                    }
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedFailure();", true);
                }
            }
            else if (rbNo.Checked)
            {
                PersonModel personmodal = new PersonModel();
                if (!string.IsNullOrEmpty(searchindname.Value))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalViewINDNoEvent();", true);
                    int indid = int.Parse(hdnExistingIA.Value);
                    if (indid != 0)
                    {
                        personmodal = fundraising.GetPersonData(indid);
                        if (personmodal.email != null)
                        {
                            //personal
                            LblIndName2.Text = personmodal.fullNameNametag;
                            LblIndFirstName.Text = personmodal.firstName;
                            LblIndLastName.Text = personmodal.surname;
                            LblIndName.Text = personmodal.fullNameNametag;
                            LblIndGender.Text = personmodal.gender;
                            LblIndHonorific.Text = personmodal.honorific;
                            LblIndSalutation.Text = personmodal.salutation;
                            LblIndNationality.Text = personmodal.nationality;
                            LblIndSDR.Text = personmodal.SDR;
                            //contact
                            LblIndTelephone.Text = personmodal.telNum;
                            LblIndEmail.Text = personmodal.email;
                            //additional
                            LblIndDesignation.Text = personmodal.designation1;
                            LblIndDepartment.Text = personmodal.department1;
                            LblIndOrganisation.Text = personmodal.organisation1;
                            LblIndDesignation2.Text = personmodal.designation2;
                            LblIndDepartment2.Text = personmodal.department2;
                            LblIndOrganisation2.Text = personmodal.organisation2;
                            LblIndSource.Text = personmodal.source;
                            LblIndCat1.Text = personmodal.cat1;
                            LblIndCat2.Text = personmodal.cat2;
                        }
                    }
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
        }

        //Existing Corporate Associate clicking 'Save' button
        protected void submitOrgDonation_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndEdit();", true);

            if (rbYes.Checked)
            {
                if (!string.IsNullOrEmpty(TbDonationAmt.Value) && !string.IsNullOrEmpty(TbDonationDate.Value)
                    && !string.IsNullOrEmpty(startdatetime.Value) && !string.IsNullOrEmpty(searchorgname.Value))
                {
                    int createorgcheck = 0;
                    EventInfo eventInfo = new EventInfo();
                    eventInfo.DonationAmt = decimal.Parse(TbDonationAmt.Value);
                    eventInfo.DonationDate = DateTime.Parse(TbDonationDate.Value);
                    eventInfo.CreatedDate = DateTime.Now;
                    eventInfo.EventName = ddlSubType.Text;
                    eventInfo.EventType = ddlType.Text;
                    eventInfo.EventStartDate = DateTime.Parse(startdatetime.Value);
                    eventInfo.EventEndDate = DateTime.Parse(enddatetime.Value);
                    eventInfo.OrgId = int.Parse(hdnExistingCA.Value);

                    createorgcheck = fundraising.InsertOrganisationDonation(eventInfo);
                    if (createorgcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackCheckedOrg();", true);
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        ddlType.SelectedIndex = 0;
                        ddlSubType.SelectedIndex = 0;
                        startdatetime.Value = "";
                        enddatetime.Value = "";
                        searchorgname.Value = "";
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedFailureOrg();", true);
                }

            }
            else if (rbNo.Checked)
            {
                if (!string.IsNullOrEmpty(TbDonationAmt.Value) && !string.IsNullOrEmpty(TbDonationDate.Value)
                    && !string.IsNullOrEmpty(searchorgname.Value))
                {
                    int createorgcheck = 0;
                    EventInfo eventInfo = new EventInfo();
                    eventInfo.DonationAmt = decimal.Parse(TbDonationAmt.Value);
                    eventInfo.DonationDate = DateTime.Parse(TbDonationDate.Value);
                    eventInfo.CreatedDate = DateTime.Now;
                    eventInfo.OrgId = int.Parse(hdnExistingCA.Value);

                    createorgcheck = fundraising.InsertOrganisationDonationWithoutEvent(eventInfo);
                    if (createorgcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackCheckedOrgNoEvent();", true);
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        searchorgname.Value = "";
                    }
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedNoEventFailureOrg();", true);
                }
            }
        }
        //Viewing Corporate Associate Details
        protected void organisationMoreInfo_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndEdit();", true);
            if (rbYes.Checked)
            {
                OrganisationModel organisationModel = new OrganisationModel();

                if (!string.IsNullOrEmpty(searchorgname.Value))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalViewORG();", true);
                    int orgid = int.Parse(hdnExistingCA.Value);
                    if (orgid != 0)
                    {
                        organisationModel = fundraising.GetOrgData(orgid);
                        if (organisationModel.orgname != null)
                        {
                            LblOrgName1.Text = organisationModel.orgname;
                            LblOrgName.Text = organisationModel.orgname;
                            LblOrgTelephone.Text = organisationModel.telNo;
                            LblOrgOffice.Text = organisationModel.officeNo;
                            LblOrgPointOfContact.Text = organisationModel.pointOfContact;
                            LblOrgMailingAddLine1.Text = organisationModel.AddLine1;
                            LblOrgMailingAddLine2.Text = organisationModel.AddLine2;
                            LblOrgMailingCity.Text = organisationModel.City;
                            LblOrgMailingPostal.Text = organisationModel.Postal;
                            LblOrgWebsite.Text = organisationModel.Website;
                            LblOrgBizDesc.Text = organisationModel.BizDesc;
                            LblOrgUEN.Text = organisationModel.UEN;
                            LblOrgNotes.Text = organisationModel.Notes;
                        }
                    }
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedFailureOrg();", true);
                }
            }
            else if (rbNo.Checked)
            {
                OrganisationModel organisationModel = new OrganisationModel();

                if (!string.IsNullOrEmpty(searchorgname.Value))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalViewORGNoEvent();", true);
                    int orgid = int.Parse(hdnExistingCA.Value);
                    if (orgid != 0)
                    {
                        organisationModel = fundraising.GetOrgData(orgid);
                        if (organisationModel.orgname != null)
                        {
                            LblOrgName1.Text = organisationModel.orgname;
                            LblOrgName.Text = organisationModel.orgname;
                            LblOrgTelephone.Text = organisationModel.telNo;
                            LblOrgOffice.Text = organisationModel.officeNo;
                            LblOrgPointOfContact.Text = organisationModel.pointOfContact;
                            LblOrgMailingAddLine1.Text = organisationModel.AddLine1;
                            LblOrgMailingAddLine2.Text = organisationModel.AddLine2;
                            LblOrgMailingCity.Text = organisationModel.City;
                            LblOrgMailingPostal.Text = organisationModel.Postal;
                            LblOrgWebsite.Text = organisationModel.Website;
                            LblOrgBizDesc.Text = organisationModel.BizDesc;
                            LblOrgUEN.Text = organisationModel.UEN;
                            LblOrgNotes.Text = organisationModel.Notes;
                        }
                    }
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedNoEventFailureOrg();", true);
                }
            }
        }

        //Add New Prospective Member Button
        protected void submitProspectiveDonation_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndEdit();", true);

            string eventtype, donationAmount, donationDate;

            if (rbNo.Checked == true)
            {
                eventtype = rbNo.Value;
            }
            else
            {
                eventtype = rbYes.Value;
            }

            donationAmount = TbDonationAmt.Value.ToString();
            donationDate = TbDonationDate.Value.ToString();
            ArrayList prospectiveList = new ArrayList();
            prospectiveList.Add(eventtype);
            prospectiveList.Add(donationAmount);
            prospectiveList.Add(donationDate);
            Session["Prospective"] = prospectiveList;

            if (rbYes.Checked)
            {
                if (!string.IsNullOrEmpty(TbDonationAmt.Value) && !string.IsNullOrEmpty(TbDonationDate.Value)
                    && !string.IsNullOrEmpty(startdatetime.Value))
                {
                    if (eventtype.Equals("rbYes"))
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalViewProspective();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "modalViewProspectiveFailure();", true);
                }
            }
            else if (rbNo.Checked)
            {
                if (!string.IsNullOrEmpty(TbDonationAmt.Value) && !string.IsNullOrEmpty(TbDonationDate.Value))
                {
                    if (eventtype.Equals("rbNo"))
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalViewProspectiveNoEvent();", true);
                    }
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "modalViewProspectiveFailureNoEvent();", true);
                }
            }
        }
        //Prospective (Add)
        protected void btnProceedProspectiveMember_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideDonorIndEdit();", true);

            string gender, eventtype, donationAmount, donationDate;
            prospectiveList = (ArrayList)Session["Prospective"];

            if (prospective_Male.Checked == true)
            {
                gender = prospective_Male.Value;
            }
            else
            {
                gender = prospective_Female.Value;
            }

            eventtype = prospectiveList[0].ToString();
            donationAmount = prospectiveList[1].ToString();
            donationDate = prospectiveList[2].ToString();

            prospectiveList.Add(prospective_txtFirstName.Value);
            prospectiveList.Add(prospective_txtSurname.Value);
            prospectiveList.Add(gender);
            prospectiveList.Add(prospective_ddlHonorific.SelectedValue.ToString());
            prospectiveList.Add(prospective_txtSalutationField.Value);
            prospectiveList.Add(prospective_txtTelephone.Value);
            prospectiveList.Add(prospective_txtEmail.Value);
            prospectiveList.Add(prospective_txtDesig1.Value);
            prospectiveList.Add(prospective_txtDept1.Value);
            prospectiveList.Add(prospective_txtOrg1.Value);
            prospectiveList.Add(prospective_txtDesig2.Value);
            prospectiveList.Add(prospective_txtDept2.Value);
            prospectiveList.Add(prospective_txtOrg2.Value);
            prospectiveList.Add(prospective_txtSDR.Value);
            prospectiveList.Add(prospective_ddlNationality.SelectedValue.ToString());
            prospectiveList.Add(prospective_txtFullNameNameTag.Value);
            prospectiveList.Add(startdatetime.Value);
            prospectiveList.Add(enddatetime.Value);
            prospectiveList.Add(ddlType.SelectedValue.ToString());
            prospectiveList.Add(ddlSubType.SelectedValue.ToString()); //22
            prospectiveList.Add(prospective_ddlSource.SelectedValue); //23
            prospectiveList.Add(prospective_ddlCat1.SelectedValue); //24
            prospectiveList.Add(prospective_ddlCat2.SelectedValue);//25
                                                                   //  prospectiveList.Add(DateTime.Now);//26 
            Session["ProspectiveList"] = prospectiveList;
            //Insert person & donation table
            if (rbYes.Checked)
            {
                int check = 0;
                check = fundraising.AddProspective(prospectiveList);
                if (check == 2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "ProspectiveEventSuccess();", true);
                    TbDonationAmt.Value = "";
                    TbDonationDate.Value = "";
                    ddlType.SelectedIndex = 0;
                    ddlSubType.SelectedIndex = 0;
                    startdatetime.Value = "";
                    enddatetime.Value = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "modalViewProspectiveFailure();", true);
                }
            }
            else if (rbNo.Checked)
            {
                int check = 0;
                check = fundraising.AddProspectiveNoEvent(prospectiveList);
                if (check == 2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "ProspectiveNoEventSuccess();", true);
                    TbDonationAmt.Value = "";
                    TbDonationDate.Value = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "modalViewProspectiveFailureNoEvent();", true);
                }
            }
        }

        //Update existing IA
        protected void UpdateIndDonation_ServerClick(object sender, EventArgs e)
        {
            if (rbYes.Disabled == true)
            {
                var donationdate = TbDonationDate.Value;
                var donationamount = TbDonationAmt.Value;
                if (!string.IsNullOrEmpty(donationamount) && !string.IsNullOrEmpty(donationdate))
                {
                    int indcheck = fundraising.EditIndividualDonorNoEvent(donationamount, donationdate, int.Parse(hdnDonationID.Value));
                    if (indcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "updateIANoEvent();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "updateIANoEventFailure();", true);
                }
            }
            else if (rbNo.Disabled == true)
            {
                var donationdate = TbDonationDate.Value;
                var donationamount = TbDonationAmt.Value;
                var eventname = ddlSubType.Text;
                var eventtype = ddlType.Text;
                var startdate = DateTime.Parse(startdatetime.Value);
                var enddate = DateTime.Parse(enddatetime.Value);
                if (!string.IsNullOrEmpty(donationamount) && !string.IsNullOrEmpty(donationdate))
                {
                    int indcheck = fundraising.EditIndividualDonorWithEvent(donationamount, donationdate, int.Parse(hdnDonationID.Value), eventname, eventtype, startdate, enddate);
                    if (indcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "updateIAEvent();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "updateIAFailure();", true);
                }
            }
        }
        //modal to delete existing IA
        protected void DeleteIndDonation_ServerClick(object sender, EventArgs e)
        {
            if (rbYes.Disabled == true)
            {
                lblmodaltitlenameInd.InnerText = searchindname.Value;              
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showIANoEventmodal();", true);
            }
            else if (rbNo.Disabled == true)
            {
                lblmodaltitlenameInd.InnerText = searchindname.Value;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showIAEventmodal();", true);
            }      
        }
        //delete existing IA
        protected void btnDeleteInd_ServerClick(object sender, EventArgs e)
        {
            int donationId = Int32.Parse(hdnDonationID.Value);
            if (rbYes.Disabled == true)
            {
                if (donationId > 0)
                {
                    int check = fundraising.DeleteDonationRecord(donationId);
                    if (check == 1 || check == 0)
                    {
                        searchindname.Value = "";
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        UpdateIndDonation.Disabled = true;
                        DeleteIndDonation.Disabled = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "deleteIANoEvent();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "deleteIANoEventFailure();", true);
                    }
                }
            }
            else if (rbNo.Disabled == true)
            {
                if (donationId > 0)
                {
                    int check = fundraising.DeleteDonationRecord(donationId);
                    if (check == 1 || check == 0)
                    {
                        searchindname.Value = "";
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        startdatetime.Value = "";
                        enddatetime.Value = "";
                        ddlType.SelectedIndex = 0;
                        ddlSubType.SelectedIndex = 0;
                        UpdateIndDonation.Disabled = true;
                        DeleteIndDonation.Disabled = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "deleteIAEvent();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "deleteIAEventFailure();", true);
                    }
                }
            }    
        }

        //Update existing CA
        protected void UpdateOrgDonation_ServerClick(object sender, EventArgs e)
        {
            if (rbYes.Disabled == true)
            {
                var donationdate = TbDonationDate.Value;
                var donationamount = TbDonationAmt.Value;
                if (!string.IsNullOrEmpty(donationamount) && !string.IsNullOrEmpty(donationdate))
                {
                    int orgcheck = fundraising.EditCorporateDonorNoEvent(donationamount, donationdate, int.Parse(hdnDonationID.Value));
                    if (orgcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "updateCANoEvent();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "updateCANoEventFailure();", true);
                }
            }
            else if (rbNo.Disabled == true)
            {
                var donationdate = TbDonationDate.Value;
                var donationamount = TbDonationAmt.Value;
                var eventname = ddlSubType.Text;
                var eventtype = ddlType.Text;
                var startdate = DateTime.Parse(startdatetime.Value);
                var enddate = DateTime.Parse(enddatetime.Value);
                if (!string.IsNullOrEmpty(donationamount) && !string.IsNullOrEmpty(donationdate))
                {
                    int indcheck = fundraising.EditCorporateDonorWithEvent(donationamount, donationdate, int.Parse(hdnDonationID.Value), eventname, eventtype, startdate, enddate);
                    if (indcheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "updateCAEvent();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "updateCAFailure();", true);
                }
            }
        }
        //modal to delete existing CA
        protected void DeleteOrgDonation_ServerClick(object sender, EventArgs e)
        {
            if (rbYes.Disabled == true)
            {
                lblmodaltitlenameOrg.InnerText = searchorgname.Value;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showCANoEventmodal();", true);
            }
            else if (rbNo.Disabled == true)
            {
                lblmodaltitlenameOrg.InnerText = searchorgname.Value;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showCAEventmodal();", true);
            }
        }
        //delete existing IA
        protected void btnDeleteOrg_ServerClick(object sender, EventArgs e)
        {
            int donationId = Int32.Parse(hdnDonationID.Value);
            if (rbYes.Disabled == true)
            {
                if (donationId > 0)
                {
                    int check = fundraising.DeleteDonationRecord(donationId);
                    if (check == 1 || check == 0)
                    {
                        searchorgname.Value = "";
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        UpdateIndDonation.Disabled = true;
                        DeleteIndDonation.Disabled = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "deleteCANoEvent();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "deleteCANoEventFailure();", true);
                    }
                }
            }
            else if (rbNo.Disabled == true)
            {
                if (donationId > 0)
                {
                    int check = fundraising.DeleteDonationRecord(donationId);
                    if (check == 1 || check == 0)
                    {
                        searchorgname.Value = "";
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        startdatetime.Value = "";
                        enddatetime.Value = "";
                        ddlType.SelectedIndex = 0;
                        ddlSubType.SelectedIndex = 0;
                        UpdateIndDonation.Disabled = true;
                        DeleteIndDonation.Disabled = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "deleteCAEvent();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "deleteCAEventFailure();", true);
                    }
                }
            }
        }

        //Update prospective donors
        protected void UpdateProsDonation_ServerClick(object sender, EventArgs e)
        {
            if (rbYes.Disabled == true)
            {
                var donationdate = TbDonationDate.Value;
                var donationamount = TbDonationAmt.Value;
                if (!string.IsNullOrEmpty(donationamount) && !string.IsNullOrEmpty(donationdate))
                {
                    int perspectivecheck = fundraising.EditIndividualDonorNoEvent(donationamount, donationdate, int.Parse(hdnDonationID.Value));
                    if (perspectivecheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "updateProspectivesNoEvent();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "updateProspectivesNoEventFailure();", true);
                }
            }
            else if (rbNo.Disabled == true)
            {
                var donationdate = TbDonationDate.Value;
                var donationamount = TbDonationAmt.Value;
                var eventname = ddlSubType.Text;
                var eventtype = ddlType.Text;
                var startdate = DateTime.Parse(startdatetime.Value);
                var enddate = DateTime.Parse(enddatetime.Value);
                if (!string.IsNullOrEmpty(donationamount) && !string.IsNullOrEmpty(donationdate))
                {
                    int perspectivecheck = fundraising.EditIndividualDonorWithEvent(donationamount, donationdate, int.Parse(hdnDonationID.Value), eventname, eventtype, startdate, enddate);
                    if (perspectivecheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "updateProspectivesEvent();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "updateProspectivesFailure();", true);
                }
            }
        }
        //modal to delete prospective donors
        protected void DeleteProspectives_ServerClick(object sender, EventArgs e)
        {
            PersonModel personmodal = new PersonModel();
            personmodal = fundraising.GetPersonData(int.Parse(hdnProsID.Value));
            if (rbYes.Disabled == true)
            {
                lblmodaltitlenameProspectives.InnerText = personmodal.fullNameNametag;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showProspectivesNoEventmodal();", true);
            }
            else if (rbNo.Disabled == true)
            {
                lblmodaltitlenameProspectives.InnerText = personmodal.fullNameNametag;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showProspectivesEventmodal();", true);
            }
        }
        //delete prospective donors
        protected void btnDeleteProspectives_ServerClick(object sender, EventArgs e)
        {
            int donationId = Int32.Parse(hdnDonationID.Value);
            if (rbYes.Disabled == true)
            {
                if (donationId > 0)
                {
                    int check = fundraising.DeleteDonationRecord(donationId);
                    if (check == 1 || check == 0)
                    {
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        UpdateIndDonation.Disabled = true;
                        DeleteIndDonation.Disabled = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "deleteProspectivesNoEvent();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "deleteProspectivesNoEventFailure();", true);
                    }
                }
            }
            else if (rbNo.Disabled == true)
            {
                if (donationId > 0)
                {
                    int check = fundraising.DeleteDonationRecord(donationId);
                    if (check == 1 || check == 0)
                    {
                        TbDonationAmt.Value = "";
                        TbDonationDate.Value = "";
                        startdatetime.Value = "";
                        enddatetime.Value = "";
                        ddlType.SelectedIndex = 0;
                        ddlSubType.SelectedIndex = 0;
                        UpdateIndDonation.Disabled = true;
                        DeleteIndDonation.Disabled = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "deleteProspectivesEvent();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "deleteProspectivesEventFailure();", true);
                    }
                }
            }
        }

    }
}