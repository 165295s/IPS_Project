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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class Membership_Registration_IndividualDetail : System.Web.UI.Page
    {
        private ArrayList pList;
        MembershipDAO db = new MembershipDAO();
        string gender, memRegType, memRegDonorTier, memRegExpDate;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                MembershipDAO d1 = new MembershipDAO();
                DataTable DT = new DataTable();
                DT = d1.GetLookupSearch("HONOURIFIC");
                ddlList.DataSource = DT;
                ddlList.DataTextField = "Code_Desc";
                ddlList.DataValueField = "Code"; //When insert, this value
                ddlList.DataBind();
                ddlList.Items.Insert(0, "");

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
                ddlCat2.DataValueField = "Code";
                
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
                    //IF Session not null, means page is triggered by the add IA from member Registration page
                    //VALUES SUCCESFULY PASSED
                    Session["IndivEdit"] = null;
                    pList = (ArrayList)Session["Person"];
                    hiddentext.Value = pList[0].ToString();
                    memRegType = pList[0].ToString();
                    memRegDonorTier = pList[1].ToString();
                    memRegExpDate = pList[2].ToString();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideToggle();", true);

                }
                if (Session["IndivEdit"] != null)
                {
                    Session["Person"] = null;
                    // IF Session not null means that page is triggered by member management page 
                    hiddentextPersonID.Value = Session["IndivEdit"].ToString();
                    MembershipDAO dalMem = new MembershipDAO();
                    PersonModel perModel = new PersonModel();
                    hiddentext.Value = "Individual Associate";
                    perModel = dalMem.GetPersonData(hiddentextPersonID.Value.ToString());
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
                    AddPA.Disabled = true;
                    btnSave.Disabled = true;


                }

            }

            if (IsPostBack)
            {

                bindtable();
            }
            else
            {

                bindtable();

            }


        }


        protected void Button_Save(Object sender, EventArgs e)
        {
            bool flag = false;
            pList = (ArrayList)Session["Person"];
            Session["p1"] = Session["Person"];

            pList = (ArrayList)Session["p1"];


            if (Male.Checked == true)
            {
                gender = Male.Value;
            }
            else
            {
                gender = Female.Value;
            }

              

            if (string.IsNullOrEmpty(txtFirstName.Value.ToString()) || txtFirstName.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please First Name Field.')", true);

            }
            else
            {
                pList.Add(txtFirstName.Value); //3
                flag = true;
            }

            if (string.IsNullOrEmpty(txtSurname.Value.ToString()) || txtSurname.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Surname Field.')", true);

            }
            else
            {
                pList.Add(txtSurname.Value); //4
                flag = true;
            }






            





            pList.Add(gender); //5




            pList.Add(ddlList.SelectedValue.ToString()); //6


            if (string.IsNullOrEmpty(txtSalutationField.Value.ToString()) || txtSalutationField.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Salutation Field.')", true);

            }
            else
            {
                pList.Add(txtSalutationField.Value); //7
                flag = true;
            }


            if (string.IsNullOrEmpty(txtTelephone.Value.ToString()) || txtTelephone.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Telephone Number Field.')", true);

            }
            else
            {
                pList.Add(txtTelephone.Value); //8
                flag = true;
            }



            if (string.IsNullOrEmpty(txtEmail.Value.ToString()) || txtEmail.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Email Field.')", true);

            }
            else
            {
                pList.Add(txtEmail.Value); //9
                flag = true;
            }


            if (string.IsNullOrEmpty(txtDesig1.Value.ToString()) || txtDesig1.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Designation 1 Field.')", true);

            }
            else
            {
                pList.Add(txtDesig1.Value); //10
                flag = true;
            }


            if (string.IsNullOrEmpty(txtDept1.Value.ToString()) || txtDept1.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Department 1 Field.')", true);

            }
            else
            {
                pList.Add(txtDept1.Value); //11
                flag = true;
            }


            if (string.IsNullOrEmpty(txtOrg1.Value.ToString()) || txtOrg1.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Organisation 1 Field.')", true);

            }
            else
            {
                pList.Add(txtOrg1.Value); //12
                flag = true;
            }



            pList.Add(txtDesig2.Value); //13



            pList.Add(txtDept2.Value); //14




            pList.Add(txtOrg2.Value); //15




            pList.Add(txtSDR.Value); //16



            pList.Add(ddlNationality.SelectedValue.ToString()); //17



            if (string.IsNullOrEmpty(txtFullNameNameTag.Value.ToString()) || txtFullNameNameTag.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Full Name Name Tag Field.')", true);

            }
            else
            {
                pList.Add(txtFullNameNameTag.Value); //18
                flag = true;
            }



            

            pList.Add(ddlStatus.SelectedValue.ToString()); //19

            pList.Add(ddlSource.SelectedValue); //20

            pList.Add(ddlCat1.SelectedValue); //21

            pList.Add(ddlCat2.SelectedValue);//22












            Session["indvPerson"] = pList;



            if (flag != false)
            {
                int check = 0;
                try
                {
                    check = db.AddPerson(pList);

                    if (check == 2)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Individual: " + txtSurname.Value + " " + txtFirstName.Value + "');", true);
                        ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideToggle();", true);

                    }
                    else if (check == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure('There seems to be an error! Please notify the Administrators.');", true);
                    }




                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);

                }


                try
                {
                    clearFields();
                    clearArrayList();
                }
                catch (Exception ex)
                {

                }

            }







        }

        public void bindtable()
        {
            MembershipDAO db = new MembershipDAO();
            UserTable.DataSource = db.GetIndivPAInfo();
            UserTable.DataBind();
            UserTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (IsPostBack)
            {
                UserTable.DataSource = db.GetIndivPAInfo();
                UserTable.DataBind();
                //upPanel.Update();
            }
        }

        protected void Submit_PA(object sender, EventArgs e)
        {
         
            // GO HOME AND DO



            int check = 0;
            try
            {
                MembershipDAO user_PA = new MembershipDAO();
                if (Session["Person"] != null)
                {
                    check = user_PA.AddPA(modalDDList.SelectedValue.ToString(), modalFName.Value, modalSname.Value, modalTelNo.Value, modalEmail.Value);
                }
                if (Session["IndivEdit"] != null)
                {
                    check = user_PA.AddPALater(hiddentextPersonID.Value, modalDDList.SelectedValue.ToString(), modalFName.Value, modalSname.Value, modalTelNo.Value, modalEmail.Value);
                }
                if (check == 1 || check == 2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Personal Assistant: " + modalSname.Value + " " + modalFName.Value + "');", true);
                    UserTable.DataSource = db.GetIndivPAInfo();
                    UserTable.DataBind();

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


        public void updateINDIV(object sender, EventArgs e)
        {
            string genderChk;
            if (Male.Checked == true)
            {
                genderChk = Male.Value;
            }
            else
            {
                genderChk = Female.Value;
            }


            int personId = int.Parse(hiddentextPersonID.Value);
            MembershipDAO d1 = new MembershipDAO();
            int check = d1.UpdateIndividual(personId, txtFirstName.Value, txtSurname.Value, genderChk, ddlSource.SelectedValue, ddlList.SelectedValue, txtSalutationField.Value, txtTelephone.Value, txtEmail.Value, ddlNationality.SelectedValue, DateTime.Now, txtDesig1.Value, txtDept1.Value, txtOrg1.Value, txtDesig2.Value, txtDept2.Value, txtOrg2.Value, txtSDR.Value, txtFullNameNameTag.Value, ddlStatus.SelectedValue);
            if (check == 2)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated for Individual Associate: " + txtFullNameNameTag.Value + "');", true);

            }
            else if (check == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('testing');", true);
            }




        }


        public void deleteINDIV(object sender, EventArgs e)
        {
            int indid = int.Parse(hiddentextPersonID.Value);
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
            rptrIAdets.DataSource = db.GetIndivPAInfo(personId);
            rptrIAdets.DataBind();
        }

        public void btnDeleteInd_ServerClick(object sender, EventArgs e)
        {
            int personId = Int32.Parse(hiddentextPersonID.Value);
            MembershipDAO d1 = new MembershipDAO();
            if (personId > 0)
            {
                int check = d1.DeleteIARecord(personId);
                if (check == 1 || check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Deleted');", true);
                    Response.Redirect("Member_MemberManagement.aspx");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }


        }

        protected void RowEditing(object sender, EventArgs eh)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            string pa_ID = row.Cells[0].Text;

            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "showUpdatePA()", true);

            PersonModel p = new PersonModel();
            p = db.getPAEdit(pa_ID);
            hiddentextPA_ID.Value = pa_ID.ToString();
            modalDDList.SelectedValue = p.honorific;
            modalFName.Value = p.firstName;
            modalSname.Value = p.surname;
            modalEmail.Value = p.email;
            modalTelNo.Value = p.telNum;






        }



        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bindtable();
            string pa_ID = UserTable.Rows[e.RowIndex].Cells[0].Text;

            int check = 0;
            try
            {
                check = db.DeleteCAREPPA(pa_ID);
                bindtable();

                if (check == 1)
                {

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

        public void updatePA_ServerClick(object sender, EventArgs e)
        {
            int check = 0;
            try
            {

                check = db.updatePA(hiddentextPA_ID.Value, modalFName.Value, modalSname.Value, modalTelNo.Value, modalDDList.SelectedValue.ToString(), modalEmail.Value);
                bindtable();

                if (check == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "script", "offToggle();", true);
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

        protected void clearFields()
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

        protected void clearArrayList()
        {
            for (int i = 3; i <= pList.Count; i++)
            {

                pList[i].Equals("");

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

    }


}
