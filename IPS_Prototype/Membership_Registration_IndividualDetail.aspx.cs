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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) {
                MembershipDAO d1 = new MembershipDAO();
                DataTable DT = new DataTable();
                DT = d1.GetLookupSearch("HONOURIFIC");
                ddlList.DataSource = DT;
                ddlList.DataTextField = "Code_Desc";
                ddlList.DataValueField = "Code"; //When insert, this value
                ddlList.DataBind();

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


                if (Session["Person"] != null) {
                    //IF Session not null, means page is triggered by the add IA from member Registration page
                    //VALUES SUCCESFULY PASSED
                    Session["IndivEdit"] = null;
                    pList = (ArrayList)Session["Person"];
                    hiddentext.Value = pList[0].ToString();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "script", "hideToggle();", true);

                }
                if (Session["IndivEdit"] != null)
                {
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
                        ddlStatus.SelectedValue="Active";
                    }
                    else
                    {
                        ddlStatus.SelectedValue = "Retired";
                    }
                    AddPA.Disabled = true;
                    btnSave.Disabled = true;


                }

            }
            //string s = string.Empty;
            //foreach (var item in pList)
            //{
            //    s += item.ToString();
            //}
            //memType.Text = s;
            if (IsPostBack)
            {

                bindtable();
            }
            else
            {

                //upPanel.Update();
                bindtable();

            }


        }


        protected void Button_Save(Object sender, EventArgs e)
        {
            string gender, memRegType, memRegDonorTier, memRegExpDate;
            pList = (ArrayList)Session["Person"];

            memRegType = pList[0].ToString();
            memRegDonorTier = pList[1].ToString();
            memRegExpDate = pList[2].ToString();

            if (Male.Checked == true)
            {
                gender = Male.Value;
            }
            else
            {
                gender = Female.Value;
            }

            pList.Add(txtFirstName.Value); //3




            pList.Add(txtSurname.Value); //4





            pList.Add(gender); //5




            pList.Add(ddlList.SelectedValue.ToString()); //6




            pList.Add(txtSalutationField.Value); //7




            pList.Add(txtTelephone.Value); //8



            pList.Add(txtEmail.Value); //9



            pList.Add(txtDesig1.Value); //10




            pList.Add(txtDept1.Value); //11



            pList.Add(txtOrg1.Value); //12



            pList.Add(txtDesig2.Value); //13



            pList.Add(txtDept2.Value); //14




            pList.Add(txtOrg2.Value); //15




            pList.Add(txtSDR.Value); //16



            pList.Add(ddlNationality.SelectedValue.ToString()); //17


            pList.Add(txtFullNameNameTag.Value); //18

            pList.Add(ddlStatus.SelectedValue.ToString()); //19

            pList.Add(ddlSource.SelectedValue); //20

            pList.Add(ddlCat1.SelectedValue); //21

            pList.Add(ddlCat2.SelectedValue);//22




            //pList.Add(txtFullName.Value);







            Session["indvPerson"] = pList;




            int check = 0;
            try
            {
                check = db.AddPerson(pList);

                if (check == 2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Individual: " + txtSurname.Value + " " + txtFirstName.Value + "');", true);


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






            //Label1.Text = honourfic;
            //Label2.Text = salutation;
            //Label3.Text = fName;
            //Label4.Text = surName;
            //Label5.Text = fullName;
            //Label6.Text = fullNameNT;
            //Label7.Text = nationality;
            //Label8.Text = gender;
            //Label9.Text = eMail;
            //Label10.Text = telPhone;
            //Label11.Text = faxNum;
            //Label12.Text = org1;
            //Label13.Text = dept1;
            //Label14.Text = desig1;
            //Label15.Text = org2;
            //Label16.Text = dept2;
            //Label17.Text = desig2;
            //Label18.Text = SDR;
            //Label19.Text = (string)pList[0];
            //Label20.Text = (string)pList[1];
            //Label21.Text = (string)pList[2];






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
                    check = user_PA.AddPALater(hiddentextPersonID.Value,modalDDList.SelectedValue.ToString(), modalFName.Value, modalSname.Value, modalTelNo.Value, modalEmail.Value);
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
            //DALMembership user = new DALMembership();
            int check = d1.UpdateIndividual(personId, txtFirstName.Value, txtSurname.Value, genderChk, ddlSource.SelectedValue, ddlList.SelectedValue, txtSalutationField.Value, txtTelephone.Value, txtEmail.Value, ddlNationality.SelectedValue, DateTime.Now, txtDesig1.Value, txtDept1.Value, txtOrg1.Value, txtDesig2.Value, txtDept2.Value, txtOrg2.Value, txtSDR.Value, txtFullNameNameTag.Value,ddlStatus.SelectedValue);
            if (check == 2)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated for Individual Associate: " + txtFullNameNameTag.Value + "');", true);
                //gvPerson.DataSource = mem.getAllMembershipDetailPerson();
                //gvPerson.DataBind();
                //gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (check == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
            }




        }


        public void deleteINDIV(object sender, EventArgs e)
        {
            //GridViewRow row = (GridViewRow)((HtmlButton)sender).NamingContainer;
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

        protected void RowEditing(object sender,EventArgs eh)
        {

            string pa_ID = UserTable.DataKeys[0]["PA_ID"].ToString();
            //string honorific = UserTable.Rows[e.RowIndex].Cells[1].Text;
            //string fname = UserTable.Rows[e.RowIndex].Cells[2].Text;
            //string sName = UserTable.Rows[e.RowIndex].Cells[3].Text;
            //string email = UserTable.Rows[e.RowIndex].Cells[4].Text;
            //string tel_num = UserTable.Rows[e.RowIndex].Cells[5].Text;
            //showPAModal
            //bindtable();
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "showUpdatePA()", true);
         
            PersonModel p = new PersonModel();
            p = db.getPAEdit(pa_ID);
            hiddentextPA_ID.Value = pa_ID.ToString() ;
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
            //string expirydate = gvOrg.Rows[e.RowIndex].Cells[2].Text;

            int check = 0;
            try
            {
                check = db.DeleteCAREPPA(pa_ID);
                bindtable();

                if (check == 1)
                {
                    //bindtable();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Deketed Personal Assistant: " + modalFName.Value + " " + modalSname.Value + "');", true);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "script", "showUpdatePA()", true);
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
                check = db.updatePA(hiddentextPA_ID.Value, modalFName.Value,modalSname.Value,modalTelNo.Value, modalDDList.SelectedValue.ToString(),modalEmail.Value);
                bindtable();

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
