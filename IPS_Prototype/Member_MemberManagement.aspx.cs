using IPS_Prototype.Class;
using IPS_Prototype.DAL;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class index2 : System.Web.UI.Page
    {
        DALMembership mem = new DALMembership();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QuestionOptions.SelectedIndex = 0;
                if (QuestionOptions.SelectedIndex == 0)
                {
                    panPerson.Attributes.CssStyle.Add("display", "none");
                    panOrg.Attributes.CssStyle.Add("display", "block");
                }
                else if (QuestionOptions.SelectedIndex == 1)
                {
                    panPerson.Attributes.CssStyle.Add("display", "block");
                    panOrg.Attributes.CssStyle.Add("display", "none");
                }
                gvOrg.DataSource = mem.getAllMembershipDetailOrg();
                gvOrg.DataBind();
                gvOrg.HeaderRow.TableSection = TableRowSection.TableHeader;
                Session.Add("OrganisationID", null);

                gvPerson.DataSource = mem.getAllMembershipDetailPerson();
                gvPerson.DataBind();
                gvPerson.UseAccessibleHeader = true;
                gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
                Session.Add("IndividualID", null);

                DatabaseDAO d1 = new DatabaseDAO();
                DataTable DT = new DataTable();
                DT = d1.GetLookupSearch("HONOURIFIC");
                IndDdlHonorific.DataSource = DT;
                IndDdlHonorific.DataTextField = "Code";
                IndDdlHonorific.DataValueField = "Code"; //When insert, this value
                IndDdlHonorific.DataBind();

               
            }
            else
            {
                //If error, display failure message
                if (QuestionOptions.SelectedIndex == 0)
                {
                    panPerson.Attributes.CssStyle.Add("display", "none");
                    panOrg.Attributes.CssStyle.Add("display", "block");
                }
                else if (QuestionOptions.SelectedIndex == 1)
                {
                    panPerson.Attributes.CssStyle.Add("display", "block");
                    panOrg.Attributes.CssStyle.Add("display", "none");
                }
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
            }
          
            
        }
       


        //Button Add to link to registration
        protected void btn_MemberRegistraton(object sender, EventArgs e)
        {
            Response.Redirect("Membership_Registration.aspx");
        }

        //For CA
        //Renewal for CA
        protected void gvOrg_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //CHRIS CHANGE name: array to pos 3, expdate: array to pos 5
            string name = gvOrg.Rows[e.NewEditIndex].Cells[3].Text;
            string expirydate = gvOrg.Rows[e.NewEditIndex].Cells[5].Text;
            Session.Add("OrganisationID", name);
            Response.Redirect("Member_MemberRenewalOrg.aspx");
        }
        //TER for CA
        protected void gvOrg_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //CHRIS CHANGE name: array to pos 3, expdate: array to pos 5
            string name = gvOrg.Rows[e.RowIndex].Cells[3].Text;
            string expirydate = gvOrg.Rows[e.RowIndex].Cells[5].Text;
            Session.Add("OrganisationID", name);
            Response.Redirect("Member_MemberTerOrg.aspx");
        }

        //CA MODAL
        protected void gvOrg_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //CHRIS CHANGE name: array to pos 3, expdate: array to pos 5
            string name = gvOrg.SelectedRow.Cells[3].Text;
            string expirydate = gvOrg.SelectedRow.Cells[5].Text;
            Session.Add("OrganisationID", name);
            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "showmodalCA();", true);
            lblOrgname.InnerText = name;
            lblmodaltitlenameOrg.InnerText = "Remove Individual Associate: " + name;
        }

        //CA MODAL
        protected void gvOrg_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvOrg, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected string HasMemberPaid(int personId)
        {
            return mem.HasMemberPaid(personId)?"true":"false";
        }

        //For IA
        //Renewal for IA
        protected void gvPerson_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Get values from rows in table where edit button event is triggered
            string indname = gvPerson.Rows[e.NewEditIndex].Cells[1].Text;
            string email = gvPerson.Rows[e.NewEditIndex].Cells[3].Text;
            string expirydate = gvPerson.Rows[e.NewEditIndex].Cells[5].Text;
            string personId = gvPerson.Rows[e.NewEditIndex].Cells[0].Text;
            //Store values taken from table in session and redirect to User_Add.aspx
            Session.Add("IndividualID", email);
            Session.Add("PersonId", personId);
            Response.Redirect("Member_MemberRenewalInd.aspx");
        }
        //TER for IA
        protected void btnTER_Click(object sender, EventArgs e)
        {
            //Add IndividualID to session and redirect to Member_MemberTerInd.aspx
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            string indname = row.Cells[1].Text;
            string expirydate = row.Cells[5].Text;
            string email = row.Cells[3].Text;

            Session.Add("IndividualID", email);
            Response.Redirect("Member_MemberTerInd.aspx");
        }
        protected void gvPerson_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Add IndividualID to session and redirect to Member_MemberTerInd.aspx
            string email = gvPerson.Rows[e.RowIndex].Cells[3].Text;
            string indname = gvPerson.Rows[e.RowIndex].Cells[1].Text;
            string expirydate = gvPerson.Rows[e.RowIndex].Cells[5].Text;

            Session.Add("IndividualID", email);
            Response.Redirect("Member_MemberTerInd.aspx");
        }
        //Action delete for IA to trigger modal
        protected void PersonDelete_ServerClick(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((HtmlButton)sender).NamingContainer;
            int indid = int.Parse(row.Cells[0].Text);
            if (indid != 0)
            {
                BindEventRepeater(indid);
                string name = row.Cells[1].Text;
                lblmodaltitlenameInd.InnerText = name;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalDeleteIND();", true);
            }

            if (hidden.Value == "org")
            {
                panOrg.Attributes.CssStyle.Add("display", "block");
                //grouping.Attributes.CssStyle.Add("display", "block");
                panPerson.Attributes.CssStyle.Add("display", "none");
            }
            else
            {
                panPerson.Attributes.CssStyle.Add("display", "block");
                panOrg.Attributes.CssStyle.Add("display", "none");
                //grouping.Attributes.CssStyle.Add("display", "none");
                hdnPersonToDelete.Value = indid.ToString();
            }

            //What i tried to do before
            //HiddenField hdn = (HiddenField)gvPerson.Rows[gvPerson.SelectedIndex].FindControl("hdnId");
            //if (hdn != null)
            //{
            //    bindPAtable(int.Parse(hdn.Value));
            //    BindEventRepeater(int.Parse(hdn.Value));
            //    int rowIndex = gvPerson.SelectedRow.RowIndex;
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "showmodalIND(" + rowIndex + ");", true);
            //}
        }
        // show IND PAs in delete modal
        private void BindEventRepeater(int personId)
        {
            DALMembership db = new DALMembership();
            rptrIAdets.DataSource = db.GetIndivPAInfo(personId);
            rptrIAdets.DataBind();
        }
        //delete in modal
        protected void btnDeleteInd_ServerClick(object sender, EventArgs e)
        {
            int personId =int.Parse(hdnPersonToDelete.Value);
            if (personId > 0)
            {
                int check = mem.DeleteIARecord(personId);
                if (check == 1 || check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Deleted');", true);
                    gvPerson.DataSource = mem.getAllMembershipDetailPerson();
                    gvPerson.DataBind();
                    gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;                  
                }
                else
                {
                    //  Response.Write("<script>alert('Delete Unsuccessful.');</script>");
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
        }

        protected void addCAREP(object sender, EventArgs e) {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int ORG_ID = int.Parse(row.Cells[2].Text);
            Session["ORG_ID"] = ORG_ID;
            Response.Redirect("Membership_Registration_CorperateAssociateRepresentative.aspx");

                



        }
        protected void editOrg(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int ORG_ID = int.Parse(row.Cells[2].Text);
            Session["EDIT_ORG_ID"] = ORG_ID;
            Session["Person"] = null;
            Response.Redirect("Membership_Registration_OrganisationDetail.aspx");





        }




        //Action edit for IA to trigger modal
        //CHRIS CHANGED THIS METHOD
        protected void btnEditInd_Click(object sender, EventArgs e)
        {
            PersonModel personmodal = new PersonModel();

            //Get Person Data from GetPersonData Method in DALMembership
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int indid = int.Parse(row.Cells[0].Text);
           
            //FIRST GET ID ON ROW CLICK
            //SECOND SEARCH FOR HONOURIFIC BASED ON ID FROM PERSON TABLE
            //DataTable DT = new DataTable();
            //DT = mem.GetLookupSearch(indid);
            //string HON = DT.Rows[0]["HONORIFIC"].ToString();
            //IndDdlHonorific.SelectedValue = HON;

            //DataTable DT2 = new DataTable();
            //DT2 = mem.GetGender(indid);
            //string GEN = DT2.Rows[0]["GENDER"].ToString();
            //if (GEN == "M")
            //{
            //    IndDdlGender.SelectedIndex = 0;
            //}
            //else
            //{
            //    IndDdlGender.SelectedIndex = 1;
            //}

            //DataTable DT3 = new DataTable();
            //DT2 = mem.GetNationality(indid);
            //string NAT = DT2.Rows[0]["NATIONALITY"].ToString();
            //if (NAT == "SGP")
            //{
            //    IndDdlNationality.SelectedIndex = 0;
            //}
            //else
            //{
            //    IndDdlNationality.SelectedIndex = 1;
            //}
            //IndDdlHonorific.DataBind();

            if (indid != 0)
            {
                //    string name = row.Cells[1].Text;
                //    lblmodalnameInd.InnerText = name;
                Session["IndivEdit"] = indid;
                Session["Person"] = null;
                Response.Redirect("Membership_Registration_IndividualDetail.aspx");
                personmodal = mem.GetPersonData(indid);
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalEditIND();", true);
                if (personmodal.email != null)
                {
                    // SDR TB, but must improve it later on

                    //Sets the textbox & ddl values to the values retrieved from GetPersonData Method in DALMembership
                    IndTbGivenName.Text = personmodal.firstName;
                    IndTbSirname.Text = personmodal.surname;
                    IndDdlGender.Text = personmodal.gender;
                    IndTbSource.Text = personmodal.source;
                    IndDdlHonorific.Text = personmodal.honorific;
                    IndTbSalutation.Text = personmodal.salutation;
                    IndTbTelephone.Text = personmodal.telNum;
                    IndTbEmail.Text = personmodal.email;
                    IndDdlNationality.Text = personmodal.nationality;
                    IndTbDes1.Text = personmodal.designation1;
                    IndTbDep1.Text = personmodal.department1;
                    IndTbOrg1.Text = personmodal.organisation1;
                    IndTbDes2.Text = personmodal.designation2;
                    IndTbDep2.Text = personmodal.department2;
                    IndTbOrg2.Text = personmodal.organisation2;
                    IndTbSDR.Text = personmodal.SDR;
                    IndTbfullNameNT.Text = personmodal.fullNameNametag;
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }       
            if (hidden.Value == "org")
            {
                panOrg.Attributes.CssStyle.Add("display", "block");
                //grouping.Attributes.CssStyle.Add("display", "block");
                panPerson.Attributes.CssStyle.Add("display", "none");
            }
            else
            {
                panPerson.Attributes.CssStyle.Add("display", "block");
                panOrg.Attributes.CssStyle.Add("display", "none");
                //grouping.Attributes.CssStyle.Add("display", "none");
                hdnPersonEdit.Value = indid.ToString();
            }
        }
        //CHRIS ADDED NEW METHOD
        protected void btnEditCAREP_Click(object sender, EventArgs e)
        {
            PersonModel personmodal = new PersonModel();

            //Get Person Data from GetPersonData Method in DALMembership
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int CAREP_PERSON_ID = int.Parse(row.Cells[0].Text);
            int CAREP_ORG_ID = int.Parse(row.Cells[2].Text);
            //FIRST GET ID ON ROW CLICK
            //SECOND SEARCH FOR HONOURIFIC BASED ON ID FROM PERSON TABLE
            //DataTable DT = new DataTable();
            //DT = mem.GetLookupSearch(indid);
            //string HON = DT.Rows[0]["HONORIFIC"].ToString();
            //IndDdlHonorific.SelectedValue = HON;

            //DataTable DT2 = new DataTable();
            //DT2 = mem.GetGender(indid);
            //string GEN = DT2.Rows[0]["GENDER"].ToString();
            //if (GEN == "M")
            //{
            //    IndDdlGender.SelectedIndex = 0;
            //}
            //else
            //{
            //    IndDdlGender.SelectedIndex = 1;
            //}

            //DataTable DT3 = new DataTable();
            //DT2 = mem.GetNationality(indid);
            //string NAT = DT2.Rows[0]["NATIONALITY"].ToString();
            //if (NAT == "SGP")
            //{
            //    IndDdlNationality.SelectedIndex = 0;
            //}
            //else
            //{
            //    IndDdlNationality.SelectedIndex = 1;
            //}
            //IndDdlHonorific.DataBind();

            if (CAREP_PERSON_ID != 0)
            {
                //    string name = row.Cells[1].Text;
                //    lblmodalnameInd.InnerText = name;
                Session["CAREPEDIT"] = CAREP_PERSON_ID;
                Session["CAREPORGID"] = CAREP_ORG_ID;
                Session["Person"] = null;
                Response.Redirect("Membership_Registration_CorperateAssociateRepresentative.aspx");
                personmodal = mem.GetPersonData(CAREP_PERSON_ID);
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalEditIND();", true);
                if (personmodal.email != null)
                {
                    // SDR TB, but must improve it later on

                    //Sets the textbox & ddl values to the values retrieved from GetPersonData Method in DALMembership
                    IndTbGivenName.Text = personmodal.firstName;
                    IndTbSirname.Text = personmodal.surname;
                    IndDdlGender.Text = personmodal.gender;
                    IndTbSource.Text = personmodal.source;
                    IndDdlHonorific.Text = personmodal.honorific;
                    IndTbSalutation.Text = personmodal.salutation;
                    IndTbTelephone.Text = personmodal.telNum;
                    IndTbEmail.Text = personmodal.email;
                    IndDdlNationality.Text = personmodal.nationality;
                    IndTbDes1.Text = personmodal.designation1;
                    IndTbDep1.Text = personmodal.department1;
                    IndTbOrg1.Text = personmodal.organisation1;
                    IndTbDes2.Text = personmodal.designation2;
                    IndTbDep2.Text = personmodal.department2;
                    IndTbOrg2.Text = personmodal.organisation2;
                    IndTbSDR.Text = personmodal.SDR;
                    IndTbfullNameNT.Text = personmodal.fullNameNametag;
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
            if (hidden.Value == "org")
            {
                panOrg.Attributes.CssStyle.Add("display", "block");
                //grouping.Attributes.CssStyle.Add("display", "block");
                panPerson.Attributes.CssStyle.Add("display", "none");
            }
            else
            {
                panPerson.Attributes.CssStyle.Add("display", "block");
                panOrg.Attributes.CssStyle.Add("display", "none");
                //grouping.Attributes.CssStyle.Add("display", "none");
                hdnPersonEdit.Value = CAREP_PERSON_ID.ToString();
            }
        }

        //edit in modal
        protected void PersonSave_ServerClick(object sender, EventArgs e)
        {
            int personId = int.Parse(hdnPersonEdit.Value);
            if (personId > 0)
            {
                DALMembership user = new DALMembership();
                int check = user.EditModalPerson(personId, IndTbGivenName.Text, IndTbSirname.Text, IndDdlGender.Text, IndTbSource.Text, IndDdlHonorific.Text, IndTbSalutation.Text, IndTbTelephone.Text, IndTbEmail.Text, IndDdlNationality.Text, DateTime.Now, IndTbDes1.Text, IndTbDep1.Text, IndTbOrg1.Text, IndTbDes2.Text, IndTbDep2.Text, IndTbOrg2.Text, IndTbSDR.Text, IndTbfullNameNT.Text);
                if (check == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated for Individual Associate: " + IndTbfullNameNT.Text + "');", true);
                    gvPerson.DataSource = mem.getAllMembershipDetailPerson();
                    gvPerson.DataBind();
                    gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else if (check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
        }
        //Action view for IA to trigger modal
        protected void PersonView_ServerClick(object sender, EventArgs e)
        {
            //Get Person Data from GetPersonData Method in DALMembership
            GridViewRow row = (GridViewRow)((HtmlButton)sender).NamingContainer;
            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalViewIND();", true);
            if (hidden.Value == "org")
            {
                panOrg.Attributes.CssStyle.Add("display", "block");
                //grouping.Attributes.CssStyle.Add("display", "block");
                panPerson.Attributes.CssStyle.Add("display", "none");
            }
            else
            {
                panPerson.Attributes.CssStyle.Add("display", "block");
                panOrg.Attributes.CssStyle.Add("display", "none");
                //grouping.Attributes.CssStyle.Add("display", "none");
            }
        }
        protected void INDIV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            //rebind your gridview - GetSource(),Datasource of your GirdView
            gvPerson.PageIndex = e.NewPageIndex;
            DataTable dt1 = mem.getAllMembershipDetailPerson();
            gvPerson.DataSource = dt1;
            gvPerson.DataBind();




        }
        protected void ORG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            //rebind your gridview - GetSource(),Datasource of your GirdView
            gvOrg.PageIndex = e.NewPageIndex;

            DataTable dt1 = mem.getAllMembershipDetailOrg();
            gvOrg.DataSource = dt1;
            gvOrg.DataBind();

        }




    }

    }


// not used
//for INDIVIDUAL ASSOCIATE MODAL
//protected void gvPerson_OnSelectedIndexChanged(object sender, EventArgs e)
//{
//    //HiddenField hdn = (HiddenField)gvPerson.Rows[gvPerson.SelectedIndex].FindControl("hdnId");
//    //if (hdn != null)
//    //{
//    //    BindEventRepeater(int.Parse(hdn.Value));
//    //    int rowIndex = gvPerson.SelectedRow.RowIndex;
//    //    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "showmodalIND(" + rowIndex + ");", true);
//    //}
//    ////lblindname.InnerText = indname;
//    ////lblmodaltitlenameInd.InnerText = "Remove Individual Associate: " + indname;

//    //if (hidden.Value == "org")
//    //{
//    //    organisation.Attributes.CssStyle.Add("display", "block");
//    //    grouping.Attributes.CssStyle.Add("display", "block");
//    //    person.Attributes.CssStyle.Add("display", "none");
//    //}
//    //else
//    //{
//    //    person.Attributes.CssStyle.Add("display", "block");
//    //    organisation.Attributes.CssStyle.Add("display", "none");
//    //    grouping.Attributes.CssStyle.Add("display", "none");
//    //}
//}

//protected void gvPerson_OnRowDataBound(object sender, GridViewRowEventArgs e)
//{
//    //if (e.Row.RowType == DataControlRowType.DataRow)
//    //{
//    //    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPerson, "Select$" + e.Row.RowIndex);
//    //    e.Row.Attributes["style"] = "cursor:pointer";
//    //}
//}




//public void lnkType_OnClick(object sender, EventArgs e)
//{
//    GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
//    TextBox TextBox1 = row.FindControl("hiddentext") as TextBox;
//    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "showmodal();", true);
//}
