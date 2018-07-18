using IPS_Prototype.Class;
using IPS_Prototype.DAL;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class Membership_Registration_CorperateAssociateRepresentative : System.Web.UI.Page
    {
        private ArrayList orgList;
        MembershipDAO db = new MembershipDAO();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
              
                bindtable();
                slidertoggleDIV.Style.Add("display", "block");
                MembershipDAO d1 = new MembershipDAO();
                MembershipDAO d2 = new MembershipDAO();
                MembershipDAO d3 = new MembershipDAO();
                DataTable DT = new DataTable();
                DataTable DTRole = new DataTable();
                DataTable DTCAREP_PA = new DataTable();

                DT = d1.GetLookupSearch("HONOURIFIC");
                ddlList.DataSource = DT;
                ddlList.DataTextField = "Code_Desc";
                ddlList.DataValueField = "Code"; //When insert, this value
                ddlList.DataBind();


                DTRole = d2.GetLookupSearch("CAREP");
                ddlRole.DataSource = DTRole;
                ddlRole.DataTextField = "Code_Desc";
                ddlRole.DataValueField = "Code"; //When insert, this value
                ddlRole.DataBind();




                DT = d3.GetLookupSearch("HONOURIFIC");
                modalDDList.DataSource = DT;
                modalDDList.DataTextField = "Code_Desc";
                modalDDList.DataValueField = "Code"; //When insert, this value
                modalDDList.DataBind();

                DT = d1.GetSource();
                ddlSource.DataSource = DT;
                ddlSource.DataTextField = "source";
                ddlSource.DataValueField = "source";
                ddlSource.DataBind();
                ddlSource.SelectedValue = "Acad_TT";



                DT = d1.GetCat2();
                ddlCat2.DataSource = DT;
                ddlCat2.DataTextField = "Code_Desc";
                ddlCat2.DataTextField = "Code";
                ddlCat2.DataBind();



                DT = d1.GetCat1(ddlSource.SelectedValue);
                ddlCat1.DataSource = DT;
                ddlCat1.DataTextField = "cat_1";
                ddlCat1.DataTextField = "cat_1";
                ddlCat1.DataBind();





                if (Session["Person"] != null)
                {
                    orgList = (ArrayList)Session["Person"];
                    lblOrgName.InnerText = orgList[3].ToString();
                    slidertoggleDIV.Style.Add("display", "none");
                    ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideToggle();", true);
                }
                else
                {



                }

                if (Session["CAREPEDIT"] != null)
                {

                    slidertoggleDIV.Style.Add("display", "block");
                    hiddentext.Value = Session["CAREPEDIT"].ToString();
                    MembershipDAO dalMem = new MembershipDAO();
                    PersonModel perModel = new PersonModel();



                    perModel = dalMem.GetCAREPData(hiddentext.Value.ToString());
                    lblOrgName.InnerText = perModel.orgName.ToString();
                    txtSalutationField.Value = perModel.salutation.ToString();
                    txtFirstName.Value = perModel.firstName.ToString();
                    txtSurname.Value = perModel.surname.ToString();
                    txtFullNameNameTag.Value = perModel.fullNameNametag.ToString();
                    txtEmail.Value = perModel.email.ToString();
                    txtTelephone.Value = perModel.telNum.ToString();
                    txtOrg1.Value = perModel.organisation1.ToString();
                    txtDept1.Value = perModel.department1.ToString();
                    txtDesig1.Value = perModel.designation1.ToString();
                    txtOrg2.Value = perModel.organisation2.ToString();
                    txtDept2.Value = perModel.department2.ToString();
                    txtDesig2.Value = perModel.designation2.ToString();
                    txtSDR.Value = perModel.SDR.ToString();
                    ddlRole.SelectedValue = perModel.role.ToString();
                    if (perModel.role.Equals("F") == false)
                    {
                        if (perModel.emailSent.Equals("Yes"))
                        {
                            chkbxWelcomeEmail.Style.Add("display", "block");
                            chkbxFaciBriefed.Style.Add("display", "none");
                            welcomeEmail.Checked = true;
                            welcomeEmail.Disabled = true;

                        }
                        else
                        {
                            chkbxWelcomeEmail.Style.Add("display", "block");
                            chkbxFaciBriefed.Style.Add("display", "none");
                            welcomeEmail.Checked = false;
                            welcomeEmail.Disabled = true;
                        }
                        
                    }
                    else if (perModel.role.Equals("F") == true)
                    {
                        if (perModel.faciBriefed.Equals("Yes"))
                        {
                            chkbxWelcomeEmail.Style.Add("display", "none");
                            chkbxFaciBriefed.Style.Add("display", "block");
                            FacilitatorBriefed.Checked = true;
                            FacilitatorBriefed.Disabled = true;

                        }
                        else
                        {
                            chkbxWelcomeEmail.Style.Add("display", "none");
                            chkbxFaciBriefed.Style.Add("display", "block");
                            FacilitatorBriefed.Checked = false;
                            FacilitatorBriefed.Disabled = true;
                        }


                    }
                    else if (perModel.role.Equals("")) {

                    }

                    disableFields(perModel);



                }


            }
            else
            {

                //upPanel.Update();
                bindtable();

            }

        }
        //public void enableFields(Object sender, EventArgs e) {
        //    if (sliderToggle.Checked.Equals(true)) {
        //        txtSalutationField.Disabled = false;
        //        txtFirstName.Disabled = false;
        //        txtSurname.Disabled = false;
        //        txtFullNameNameTag.Disabled = false;
        //        txtEmail.Disabled = false;
        //        txtTelephone.Disabled = false;
        //        txtOrg1.Disabled = false;
        //        txtDept1.Disabled = false;
        //        txtDesig1.Disabled = false;
        //        txtOrg2.Disabled = false;
        //        txtDept2.Disabled = false;
        //        txtDesig2.Disabled = false;
        //        txtSDR.Disabled = false;
        //        //ddlList.Attributes.Add("disabled", "disabled");
        //        //ddlNationality.Attributes.Add("disabled", "disabled");
        //        //ddlSource.Attributes.Add("disabled", "disabled");
        //        //ddlCat1.Attributes.Add("disabled", "disabled");
        //        //ddlCat2.Attributes.Add("disabled", "disabled");
        //        //ddlStatus.Attributes.Add("disabled", "disabled");
        //        //ddlRole.Attributes.Add("disabled", "disabled");
        //        btnSave.Disabled = false;
        //        delPA.Disabled = false;

        //    }

        //}
        public void disableFields(PersonModel perModel) {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "offSlider()", true);
            txtSalutationField.Disabled = true;
            txtFirstName.Disabled = true;
            txtSurname.Disabled = true;
            txtFullNameNameTag.Disabled = true;
            txtEmail.Disabled = true;
            txtTelephone.Disabled = true;
            txtOrg1.Disabled = true;
            txtDept1.Disabled = true;
            txtDesig1.Disabled = true;
            txtOrg2.Disabled = true;
            txtDept2.Disabled = true;
            txtDesig2.Disabled = true;
            txtSDR.Disabled = true;
            ddlList.Attributes.Add("disabled", "disabled");
            ddlNationality.Attributes.Add("disabled", "disabled");
            ddlSource.Attributes.Add("disabled", "disabled");
            ddlCat1.Attributes.Add("disabled", "disabled");
            ddlCat2.Attributes.Add("disabled", "disabled");
            ddlStatus.Attributes.Add("disabled", "disabled");
            ddlRole.Attributes.Add("disabled", "disabled");
            btnSave.Disabled = true;
            delPA.Disabled = true;

            if (perModel.gender.Equals("M"))
            {
                Male.Checked = true;
            }
            else
            {

                Female.Checked = true;
            }
            if (perModel.status.Equals("Active"))
            {
                ddlStatus.SelectedValue = "Active";
            }
            else
            {
                ddlStatus.SelectedValue = "Retired";
            }
        }
        public void button_save(object sender, EventArgs e)
        {
            ArrayList carepList = new ArrayList();
            carepList.Add(txtFirstName.Value); //0
            carepList.Add(txtSurname.Value); //1
            if (Male.Checked == true)
            {
                carepList.Add(Male.Value);
            }
            else                    //2
            {
                carepList.Add(Female.Value);
            }

            carepList.Add(ddlList.SelectedValue); //3
            carepList.Add(txtSalutationField.Value); //4
            carepList.Add(txtTelephone.Value); //5
            carepList.Add(txtEmail.Value); //6
            carepList.Add(txtDesig1.Value); //7
            carepList.Add(txtDept1.Value); //8
            carepList.Add(txtOrg1.Value); //9
            carepList.Add(txtDesig2.Value); //10
            carepList.Add(txtDept2.Value); //11
            carepList.Add(txtOrg2.Value); //12
            carepList.Add(txtSDR.Value); //13
            carepList.Add(ddlNationality.SelectedValue); //14
            //End of Person Table


            //Start of CA REP Table
            carepList.Add(ddlRole.SelectedValue); //15
            carepList.Add(ddlStatus.SelectedValue.ToString()); //16
            //carepList.Add(txtFullName.Value); 
            carepList.Add(txtFullNameNameTag.Value); //17

            if (FacilitatorBriefed.Checked)
            {
                carepList.Add(FacilitatorBriefed.Value); //18
            }
            else
            {
                carepList.Add("NA");
            }
            if (welcomeEmail.Checked)
            {
                carepList.Add(welcomeEmail.Value); //19
            }
            else
            {
                carepList.Add("NA");

            }

            carepList.Add(ddlSource.SelectedValue); //20
            carepList.Add(ddlCat1.SelectedValue); //21
            carepList.Add(ddlCat2.SelectedValue); //22



            int check = 0;
            try
            {
                check = db.addCAREP(carepList);
                bindtable();
                if (check == 2)
                {
                    //bindtable();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Corporate Associate Representative: " + txtSurname.Value + " " + txtFirstName.Value + "');", true);

                }
                else if (check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }

                txtFirstName.Value.Equals("");
                txtSurname.Value.Equals("");
                txtSalutationField.Equals("");
                txtTelephone.Equals("");
                txtEmail.Equals("");
                txtDesig1.Equals("");
                txtDept1.Equals("");
                txtOrg1.Equals("");
                txtDesig2.Equals("");
                txtDept2.Equals("");
                txtOrg2.Equals("");
                txtSDR.Equals("");
                //txtFullName.Equals("");
                txtFullNameNameTag.Equals("");


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);

            }












        }
        //Hides the fist 3 colums
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;

        }


        public void bindtable()
        {
            MembershipDAO db = new MembershipDAO();
            UserTable.DataSource = db.GetCAREP();
            UserTable.DataBind();
            UserTable.HeaderRow.TableSection = TableRowSection.TableHeader;


            if (!IsPostBack)
            {
                UserTable.DataSource = db.GetCAREP();
                UserTable.DataBind();
                //upPanel.Update();
            }
        }
        public void bindPAtable()
        {
            MembershipDAO db = new MembershipDAO();
            PA_GridView.DataSource = db.GetPAInfo(caID.InnerText.ToString());
            PA_GridView.DataBind();
            PA_GridView.HeaderRow.TableSection = TableRowSection.TableHeader;


            if (!IsPostBack)
            {
                PA_GridView.DataSource = db.GetPAInfo();
                PA_GridView.DataBind();
                //upPanel.Update();
            }
        }
        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bindPAtable();
            string pa_ID = PA_GridView.Rows[e.RowIndex].Cells[0].Text;
            //string expirydate = gvOrg.Rows[e.RowIndex].Cells[2].Text;

            int check = 0;
            try
            {
                check = db.DeleteINDIVPA(pa_ID);
                bindPAtable();

                if (check == 1)
                {
                    //bindtable();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Deketed Personal Assistant: " + modalFName.Value + " " + modalSname.Value + "');", true);

                }
                else if (check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }



            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);

            }


        }



        public void Add_Pa_Command(object sender, EventArgs e)
        {
            //int rowIndex = Convert.ToInt32(e.CommandArgument);
            //TextBox tb = UserTable.Rows[rowIndex].FindControl("hiddentext") as TextBox;

            //try
            //{
            //    int orgId = Convert.ToInt32(tb.Text);
            //    Console.WriteLine(orgId);
            //    associateType.InnerText = orgId.ToString();
            //}
            //catch { }

            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            //TextBox TextBox1 = row.FindControl("hiddentext") as TextBox;

            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "showmodal();", true);
            pID.InnerText = row.Cells[0].Text; // PERSON ID
            caID.InnerText = row.Cells[1].Text; // CA_REP ID
            orgID.InnerText = row.Cells[2].Text; // ORG ID

            bindPAtable();

        }

        public void button_saveCAREP_PA(object sender, EventArgs e)
        {



            ArrayList carep_PAList = new ArrayList();
            carep_PAList.Add(modalDDList.SelectedValue);
            carep_PAList.Add(modalFName.Value);
            carep_PAList.Add(modalSname.Value);
            carep_PAList.Add(modalEmail.Value);
            carep_PAList.Add(modalTelNo.Value);
            carep_PAList.Add(pID.InnerText.ToString());
            carep_PAList.Add(caID.InnerText.ToString());
            carep_PAList.Add(orgID.InnerText.ToString());


            int check = 0;
            try
            {
                check = db.addCAREP_PA(carep_PAList);
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "showmodal();", true);
                bindPAtable();

                if (check == 2)
                {
                    //bindtable();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Personal Assistantnt: " + txtSurname.Value + " " + txtFirstName.Value + "');", true);

                }
                else if (check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }



            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);

            }





        }

        public void getCat1(object sender, EventArgs e)
        {
            MembershipDAO d1 = new MembershipDAO();
            DataTable DT = new DataTable();
            DT = d1.GetCat1(ddlSource.SelectedValue);
            ddlCat1.DataSource = DT;
            ddlCat1.DataTextField = "cat_1";
            ddlCat1.DataTextField = "cat_1";
            ddlCat1.DataBind();
        }

        public void updateCAREP(object sender, EventArgs e)
        {
            string genderChk,faciChk,emailChk;
            if (Male.Checked == true)
            {
                genderChk = Male.Value;
            }
            else                    //2
            {
                genderChk = Female.Value;
            }

            if (FacilitatorBriefed.Checked)
            {
                faciChk=FacilitatorBriefed.Value; //18
            }
            else
            {
                faciChk="NA";
            }
            if (welcomeEmail.Checked)
            {
                emailChk=welcomeEmail.Value; //19
            }
            else
            {
                emailChk="NA";

            }

            int personId = int.Parse(hiddentext.Value);
            MembershipDAO d1 = new MembershipDAO();
            //DALMembership user = new DALMembership();
            int check = d1.UpdateCAREP(personId, txtFirstName.Value, txtSurname.Value, genderChk, ddlSource.SelectedValue, ddlList.SelectedValue, txtSalutationField.Value, txtTelephone.Value, txtEmail.Value, ddlNationality.SelectedValue, DateTime.Now, txtDesig1.Value, txtDept1.Value, txtOrg1.Value, txtDesig2.Value, txtDept2.Value, txtOrg2.Value, txtSDR.Value, txtFullNameNameTag.Value,ddlRole.SelectedValue,ddlStatus.SelectedValue,faciChk,emailChk);
            if (check == 2)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated for Coporate Associate: " + txtFullNameNameTag.Value + "');", true);
                //gvPerson.DataSource = mem.getAllMembershipDetailPerson();
                //gvPerson.DataBind();
                //gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (check == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
            }




        }
        public void deleteCAREP(object sender, EventArgs e)
        {
            //GridViewRow row = (GridViewRow)((HtmlButton)sender).NamingContainer;
            int indid = int.Parse(hiddentext.Value);
            if (indid != 0)
            {
                BindEventRepeater(indid);
                string name = txtFullNameNameTag.Value;
                lblmodaltitlenameInd.InnerText = name;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "modalDeleteIND();", true);
            }

        }

        // show IND PAs in delete modal
        private void BindEventRepeater(int personId)
        {
            DALMembership db = new DALMembership();
            rptrIAdets.DataSource = db.GetCAREPPAInfo(personId);
            rptrIAdets.DataBind();
        }

        public void btnDeleteCAREP_ServerClick(object sender, EventArgs e)
        {
            int personId = Int32.Parse(hiddentext.Value);
            MembershipDAO d1 = new MembershipDAO();
            if (personId > 0)
            {
                int check = d1.DeleteCAREPRecord(personId);
                if (check == 1 || check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Deleted');", true);
                    Response.Redirect("Member_MemberManagement.aspx");
                    //gvPerson.DataSource = mem.getAllMembershipDetailPerson();
                    //gvPerson.DataBind();
                    //gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    //  Response.Write("<script>alert('Delete Unsuccessful.');</script>");
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }









        }

        protected void Editing(object sender, EventArgs e)
        {

            bindPAtable();
            string pa_ID = PA_GridView.DataKeys[0]["PA_ID"].ToString();


            //string honorific = UserTable.Rows[e.RowIndex].Cells[1].Text;
            //string fname = UserTable.Rows[e.RowIndex].Cells[2].Text;
            //string sName = UserTable.Rows[e.RowIndex].Cells[3].Text;
            //string email = UserTable.Rows[e.RowIndex].Cells[4].Text;
            //string tel_num = UserTable.Rows[e.RowIndex].Cells[5].Text;
            ////showPAModal
            //bindtable();


            PersonModel p = new PersonModel();
            p = db.getPAEdit(pa_ID);
            hiddentextPA_ID.Value = pa_ID.ToString();
            modalDDList.SelectedValue = p.honorific;
            modalFName.Value = p.firstName;
            modalSname.Value = p.surname;
            modalEmail.Value = p.email;
            modalTelNo.Value = p.telNum;



            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "showmodalAgain();", true);

        }

        public void updatePA_ServerClick(object sender, EventArgs e)
        {
            int check = 0;
            try
            {
                check = db.updatePA(hiddentextPA_ID.Value, modalFName.Value, modalSname.Value, modalTelNo.Value, modalDDList.SelectedValue.ToString(), modalEmail.Value);
                bindPAtable();

                if (check == 1)
                {
                    //bindtable();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated Personal Assistant: " + modalFName.Value + " " + modalSname.Value + "');", true);

                }
                else if (check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }



            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);

            }

        }



    }
}
