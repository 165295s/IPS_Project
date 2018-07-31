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
        ArrayList carepList;
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
                ddlList.Items.Insert(0, "");




                DTRole = d2.GetLookupSearch("CAREP");
                ddlRole.DataSource = DTRole;
                ddlRole.DataTextField = "Code_Desc";
                ddlRole.DataValueField = "Code_Desc"; //When insert, this value
                ddlRole.DataBind();




                DT = d3.GetLookupSearch("HONOURIFIC");
                modalDDList.DataSource = DT;
                modalDDList.DataTextField = "Code_Desc";
                modalDDList.DataValueField = "Code"; //When insert, this value
                modalDDList.DataBind();
                modalDDList.Items.Insert(0, "");

                DT = d1.GetSource();
                ddlSource.DataSource = DT;
                ddlSource.DataTextField = "source";
                ddlSource.DataValueField = "source";
                ddlSource.DataBind();
                ddlSource.SelectedValue = "Acad_TT";



                DT = d1.GetCat2();
                ddlCat2.DataSource = DT;
                ddlCat2.DataTextField = "Code_Desc";
                ddlCat2.DataValueField = "Code";
                //ddlCat2.Items.Insert(0, "");
                ddlCat2.DataBind();
                ddlCat2.Items.Insert(0, "");




                DT = d1.GetCat1(ddlSource.SelectedValue);
                ddlCat1.DataSource = DT;
                ddlCat1.DataTextField = "cat_1";
                ddlCat1.DataTextField = "cat_1";
                ddlCat1.DataBind();

                DT = d1.GetNationality();
                ddlNationality.DataSource = DT;
                ddlNationality.DataTextField = "NATIONALITY";
                ddlNationality.DataValueField = "NATIONALITY";
                ddlNationality.DataBind();




                if (Session["Person"] != null)
                {
                    Session["CAREPEDIT"] = null;
                    Session["CAREPORGID"] = null;
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

                    Session["Person"] = null;
                    //slidertoggleDIV.Style.Add("display", "block");
                    hiddentext.Value = Session["CAREPEDIT"].ToString();
                    string CAREP_ORG_ID = Session["CAREPORGID"].ToString() ;
                    MembershipDAO dalMem = new MembershipDAO();
                    PersonModel perModel = new PersonModel();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "script", "showToggle();", true);


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
                    ddlSource.SelectedValue = perModel.source.ToString();
                    ddlCat1.SelectedValue = perModel.cat1.ToString();
                    ddlCat2.SelectedValue = perModel.cat2.ToString();
                    ddlList.SelectedValue = perModel.honorific.ToString();
                    btnSave.Visible = false;
                    btnUpdate.Attributes.CssStyle.Remove("display");
                    bindTableEdit(CAREP_ORG_ID);
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
                    else if (perModel.role.Equals(""))
                    {

                    }

                    disableFields(perModel);



                }

                if (Session["ORG_ID"] != null) {
                    Session["Person"] = null;
                    Session["CAREPEDIT"] = null;
                    Session["CAREPORGID"] = null;
                    string org_id = Session["ORG_ID"].ToString();
                    PersonModel p1 = new PersonModel();
                    MembershipDAO dalMem = new MembershipDAO();
                    p1 = dalMem.getOrgInfo(org_id);
                    lblOrgName.InnerText = p1.orgName;
                    
                  ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "hideToggle();", true);


                }

                


            }
            else
            {

                //upPanel.Update();
                //string CAREP_ORG_ID = Session["CAREPORGID"].ToString();
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
        public void disableFields(PersonModel perModel)
        {
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
            //btnDelCAREP.Disabled = true;

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
        public void enableFields()
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "hidebtnSave();", true);

            txtSalutationField.Disabled = false;
            txtFirstName.Disabled = false;
            txtSurname.Disabled = false;
            txtFullNameNameTag.Disabled = false;
            txtEmail.Disabled = false;
            txtTelephone.Disabled = false;
            txtOrg1.Disabled = false;
            txtDept1.Disabled = false;
            txtDesig1.Disabled = false;
            txtOrg2.Disabled = false;
            txtDept2.Disabled = false;
            txtDesig2.Disabled = false;
            txtSDR.Disabled = false;
            ddlList.Attributes.Remove("disabled");
            ddlNationality.Attributes.Remove("disabled");
            ddlSource.Attributes.Remove("disabled");
            ddlCat1.Attributes.Remove("disabled");
            ddlCat2.Attributes.Remove("disabled");
            ddlStatus.Attributes.Remove("disabled");
            ddlRole.Attributes.Remove("disabled");
            btnSave.Disabled = false;
            welcomeEmail.Disabled = false;
            FacilitatorBriefed.Disabled = false;

            
            //btnDelCAREP.Disabled = true;


        }
        public void enableCAREPFields()
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "hidebtnSave();", true);

            txtSalutationField.Disabled = false;
            txtFirstName.Disabled = false;
            txtSurname.Disabled = false;
            txtFullNameNameTag.Disabled = false;
            txtEmail.Disabled = false;
            txtTelephone.Disabled = false;
            txtOrg1.Disabled = false;
            txtDept1.Disabled = false;
            txtDesig1.Disabled = false;
            txtOrg2.Disabled = false;
            txtDept2.Disabled = false;
            txtDesig2.Disabled = false;
            txtSDR.Disabled = false;
            ddlList.Attributes.Remove("disabled");
            ddlNationality.Attributes.Remove("disabled");
            ddlSource.Attributes.Remove("disabled");
            ddlCat1.Attributes.Remove("disabled");
            ddlCat2.Attributes.Remove("disabled");
            ddlStatus.Attributes.Remove("disabled");
            ddlRole.Attributes.Remove("disabled");
            btnSave.Disabled = false;
            btnSave.Visible = false;
            welcomeEmail.Disabled = false;
            FacilitatorBriefed.Disabled = false;
            btnUpdate.Attributes.Remove("display");

            //btnDelCAREP.Disabled = true;


        }



        public void button_save(object sender, EventArgs e)
        {
          
            
            if (validateFields() == true) {
                carepList = new ArrayList();
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
            //carepList.Add(ddlList.SelectedValue);//23

          
                int check = 0;
                try
                {
                    check = db.addCAREP(carepList);
                    bindtable();
                    if (check == 2)
                    {
                        //bindtable();

                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Corporate Associate Representative: " + txtSurname.Value + " " + txtFirstName.Value + "');", true);

                        carepList.Clear();




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

                try
                {
                    clearCAREPFields();
                    clearCAREPArrayList();
                }
                catch (Exception ex)
                {

                }
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

        protected void bindTableEdit(string ORG_ID) {
            MembershipDAO db = new MembershipDAO();
            UserTable.DataSource = db.GetCAREPEDIT(ORG_ID);
            UserTable.DataBind();
            UserTable.HeaderRow.TableSection = TableRowSection.TableHeader;


            if (!IsPostBack)
            {
                UserTable.DataSource = db.GetCAREPEDIT(ORG_ID);
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

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Deleted Personal Assistant: " + modalFName.Value + " " + modalSname.Value + "');", true);

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
            bool flag = false;
            ArrayList carep_PAList = new ArrayList();
       

            if (validateCAREPPAFields().Equals(true))
            {
 
                carep_PAList.Add(modalDDList.SelectedValue); //0
                carep_PAList.Add(modalFName.Value);//1
                carep_PAList.Add(modalSname.Value);//2
                carep_PAList.Add(modalEmail.Value);//3
                carep_PAList.Add(modalTelNo.Value);//4
                carep_PAList.Add(pID.InnerText.ToString());
                carep_PAList.Add(caID.InnerText.ToString());
                carep_PAList.Add(orgID.InnerText.ToString());

                flag = true;
            }
            else {


            }

         
            if (flag != false)
            {
                int check = 0;
                try
                {
                    check = db.addCAREP_PA(carep_PAList);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "showmodal();", true);
                    bindPAtable();

                    if (check == 2)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Personal Assistantnt: " + txtSurname.Value + " " + txtFirstName.Value + "');", true);
                        bindtable();

                       

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
            string genderChk, faciChk, emailChk;
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
                faciChk = FacilitatorBriefed.Value; //18
            }
            else
            {
                faciChk = "NA";
            }
            if (welcomeEmail.Checked)
            {
                emailChk = welcomeEmail.Value; //19
            }
            else
            {
                emailChk = "NA";

            }
            if (validateCAREPFields() == true)
            {

                int personId = int.Parse(hiddentext.Value);
                MembershipDAO d1 = new MembershipDAO();
                //DALMembership user = new DALMembership();
                int check = d1.UpdateCAREP(personId, txtFirstName.Value, txtSurname.Value, genderChk, ddlSource.SelectedValue, ddlList.SelectedValue, txtSalutationField.Value, txtTelephone.Value, txtEmail.Value, ddlNationality.SelectedValue, DateTime.Now, txtDesig1.Value, txtDept1.Value, txtOrg1.Value, txtDesig2.Value, txtDept2.Value, txtOrg2.Value, txtSDR.Value, txtFullNameNameTag.Value, ddlRole.SelectedValue, ddlStatus.SelectedValue, faciChk, emailChk, ddlCat1.SelectedValue.ToString(), ddlCat2.SelectedValue.ToString());
                if (check == 2)
                {
                    clearPAModal();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated for Coporate Associate: " + txtFullNameNameTag.Value + "');", true);
                    enableFields();
                    //gvPerson.DataSource = mem.getAllMembershipDetailPerson();
                    //gvPerson.DataBind();
                    //gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else if (check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }

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
            
            

            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            string pa_ID = row.Cells[0].Text;

            bindPAtable();
            //string honorific = UserTable.Rows[e.RowIndex].Cells[1].Text;
            //string fname = UserTable.Rows[e.RowIndex].Cells[2].Text;
            //string sName = UserTable.Rows[e.RowIndex].Cells[3].Text;
            //string email = UserTable.Rows[e.RowIndex].Cells[4].Text;
            //string tel_num = UserTable.Rows[e.RowIndex].Cells[5].Text;
            ////showPAModal
            //bindtable();



            //TextBox TextBox1 = row.FindControl("hiddentext") as TextBox;





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

            if (validateCAREPPAFields().Equals(true))
            {

                int check = 0;
                try
                {
                    check = db.updatePA(hiddentextPA_ID.Value, modalFName.Value, modalSname.Value, modalTelNo.Value, modalDDList.SelectedValue.ToString(), modalEmail.Value);
                    bindPAtable();

                    if (check == 1)
                    {
                        //bindtable();
                        clearPAModal();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated Personal Assistant: " + modalFName.Value + " " + modalSname.Value + "');", true);

                        hiddentextPA_ID.Value = "";
                        modalFName.Value = "";
                        modalSname.Value = "";
                        modalTelNo.Value = "";
                        modalDDList.SelectedIndex = 0;
                        modalEmail.Value = "";


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

        protected void clearCAREPFields()
        {
            Male.Checked = false;
            Female.Checked = false;
            txtFirstName.Value = "";
            txtSurname.Value = "";
            ddlList.SelectedIndex = 0;
            txtSalutationField.Value = "";
            txtTelephone.Value = "";
            txtEmail.Value = "";
            txtDesig1.Value = "";
            txtDept1.Value = "";
            txtOrg1.Value = "";
            txtDesig2.Value = "";
            txtDept2.Value = "";
            txtOrg2.Value = "";
            txtSDR.Value = "";
            ddlNationality.SelectedIndex = 0;
            txtFullNameNameTag.Value = "";
            ddlStatus.SelectedIndex = 0;
            ddlSource.SelectedIndex = 0;
            ddlCat1.SelectedIndex = 0;
            ddlCat2.SelectedIndex = 0;

        }

        protected void clearCAREPArrayList()
        {
            for (int i = 0; i <= carepList.Count; i++)
            {
                carepList[i].Equals("");

            }
        }

        protected void clearPAModal()
        {
            hiddentextPA_ID.Value = "";
            modalDDList.SelectedIndex = 0;
            modalFName.Value = "";
            modalSname.Value = "";
            modalEmail.Value = "";
            modalTelNo.Value = "";
        }
        protected bool validateFields()
        {
            if (string.IsNullOrEmpty(txtFirstName.Value.ToString()) || txtFirstName.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check First Name Field.');", true);

                return false;
                

            }
            else if (string.IsNullOrEmpty(txtSurname.Value.ToString()) || txtSurname.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Surname Field.');", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtSalutationField.Value.ToString()) || txtSalutationField.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Salutation Field.')", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtTelephone.Value.ToString()) || txtTelephone.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Telephone Field.')", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtEmail.Value.ToString()) || txtEmail.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Email Field.')", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtDesig1.Value.ToString()) || txtDesig1.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Designation 1 Field.')", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtDept1.Value.ToString()) || txtDept1.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Department 1 Field.')", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtOrg1.Value.ToString()) || txtOrg1.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Organisation 1 Field.')", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtFullNameNameTag.Value.ToString()) || txtFullNameNameTag.Value.Trim().ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Full Name Name Tag Field.')", true);

                return false;

            }
            else
            {
                return true;
            }
        }
        protected bool validateCAREPFields()
        {
            if (string.IsNullOrEmpty(txtFirstName.Value.ToString()) || txtFirstName.Value.Trim().ToString() == "")
            {
               
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check First Name Field.');", true);
                enableCAREPFields();
                return false;


            }
            else if (string.IsNullOrEmpty(txtSurname.Value.ToString()) || txtSurname.Value.Trim().ToString() == "")
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Surname Field.');", true);
                enableCAREPFields();
                return false;

            }
            else if (string.IsNullOrEmpty(txtSalutationField.Value.ToString()) || txtSalutationField.Value.Trim().ToString() == "")
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Salutation Field.')", true);
                enableCAREPFields();
                return false;

            }
            else if (string.IsNullOrEmpty(txtTelephone.Value.ToString()) || txtTelephone.Value.Trim().ToString() == "")
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Telephone Field.')", true);
                enableCAREPFields();
                return false;

            }
            else if (string.IsNullOrEmpty(txtEmail.Value.ToString()) || txtEmail.Value.Trim().ToString() == "")
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Email Field.')", true);
                enableCAREPFields();
                return false;

            }
            else if (string.IsNullOrEmpty(txtDesig1.Value.ToString()) || txtDesig1.Value.Trim().ToString() == "")
            {
               
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Designation 1 Field.')", true);
                enableCAREPFields();
                return false;

            }
            else if (string.IsNullOrEmpty(txtDept1.Value.ToString()) || txtDept1.Value.Trim().ToString() == "")
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Department 1 Field.')", true);
                enableCAREPFields();
                return false;

            }
            else if (string.IsNullOrEmpty(txtOrg1.Value.ToString()) || txtOrg1.Value.Trim().ToString() == "")
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Organisation 1 Field.')", true);
                enableCAREPFields();
                return false;

            }
            else if (string.IsNullOrEmpty(txtFullNameNameTag.Value.ToString()) || txtFullNameNameTag.Value.Trim().ToString() == "")
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Check Full Name Name Tag Field.')", true);
                enableCAREPFields();
                return false;

            }
            else
            {
                return true;
            }
        }



        protected bool validateCAREPPAFields()
        {

            if (string.IsNullOrEmpty(modalFName.Value.ToString()) || modalFName.Value.Trim().ToString().Equals(""))
            {

                //error message

                //ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayModalFailureMsg('Please FirstName Field.')", true);
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "script", "showPAModalError('Please Check First Name Field');", true);

                return false;

            }
            else if (string.IsNullOrEmpty(modalSname.Value.ToString()) || modalSname.Value.Trim().ToString().Equals(""))
            {

                //error message

                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showPAModalError('Please Check Surname Field.')", true);

                return false;
            }


            else if (string.IsNullOrEmpty(modalEmail.Value.ToString()) || modalEmail.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showPAModalError('Please Check Email Field.')", true);

                return false;
            }
            else if (string.IsNullOrEmpty(modalTelNo.Value.ToString()) || modalTelNo.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showPAModalError('Please Check Telephone Number Field.')", true);

                return false;
            }
            else
            {

                return true;
            }




        }


    }
}
