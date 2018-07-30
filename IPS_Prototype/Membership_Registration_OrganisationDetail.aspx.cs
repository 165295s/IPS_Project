using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;
using System.Data;
using IPS_Prototype.Class;
using System.Text.RegularExpressions;
using IPS_Prototype.RetrieveClass;

namespace IPS_Prototype
{
    public partial class Membership_Registration_OrganizationDetail : System.Web.UI.Page
    {
        private ArrayList orgList;

        MembershipDAO db = new MembershipDAO();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Person"] != null)
                {
                    Session["EDIT_ORG_ID"] = null;
                    orgList = (ArrayList)Session["Person"];
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "hideToggle();", true);
                }
                else
                {
                    Session["Person"] = null;




                }

                if (Session["EDIT_ORG_ID"] != null)
                {
                    Session["Person"] = null;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "hideBtnNext();", true);

                    OrgInfo o1 = new OrgInfo();
                    MembershipDAO dalMem = new MembershipDAO();
                    string org_id = Session["EDIT_ORG_ID"].ToString();
                    o1 = dalMem.getAllOrgInfo(org_id);

                    txtOrgNameField.Value = o1.orgName.ToString();
                    txtMailAddrLine1.Value = o1.mailLine1.ToString();
                    txtMailAddrLine2.Value = o1.mailLine2.ToString();
                    txtCity.Value = o1.city.ToString();
                    txtPostalCode.Value = o1.postalCode.ToString();
                    txtTelephone.Value = o1.telNo.ToString();
                    txtOffice.Value = o1.officeNo.ToString();
                    txtWebsiteURL.Value = o1.websiteURL.ToString();
                    txtbDesc.Value = o1.busDesc.ToString();
                    pointOfContact.Value = o1.PoC.ToString();
                    txtnotes.Value = o1.notes.ToString();
                    txtUEN.Value = o1.uen.ToString();
                    orgID.Value = org_id.ToString();


                    disableFields();

                }

            }
            else
            {
                if (Session["Person"] != null)
                {
                    Session["EDIT_ORG_ID"] = null;
                    orgList = (ArrayList)Session["Person"];
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "hideToggle();", true);
                }
                else
                {
                    Session["Person"] = null;




                }
            }
        }


        protected void button_next(object sender, EventArgs e)
        {

            //STORE RESPONSE INTO A SESSION
            if (validateORGFields() != false)
            {
                orgList.Add(txtOrgNameField.Value.ToString());
                orgList.Add(txtMailAddrLine1.Value.ToString());
                orgList.Add(txtMailAddrLine2.Value.ToString());
                orgList.Add(txtCity.Value.ToString());
                orgList.Add(txtPostalCode.Value.ToString());
                orgList.Add(txtTelephone.Value.ToString());
                orgList.Add(txtOffice.Value.ToString());
                orgList.Add(txtWebsiteURL.Value.ToString());
                orgList.Add(txtbDesc.Value.ToString());
                orgList.Add(pointOfContact.Value.ToString());
                orgList.Add(txtnotes.Value.ToString());
                orgList.Add(txtUEN.Value.ToString());

                int check = 0;
                try
                {
                    check = db.addOrg(orgList);
                    if (check == 1 || check == 2)
                    {
                        Response.Redirect("Membership_Registration_CorperateAssociateRepresentative.aspx");

                        //ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Org: " + txtOrgNameField.Value + "');", true);


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
            else
            {
                //error message
            }

        }

        protected void btn_Update(object sender, EventArgs e)
        {
            if (validateORGFields() != false)
            {
                int check = 0;
                try
                {
                    check = db.updateORG(txtOrgNameField.Value, txtMailAddrLine1.Value, txtMailAddrLine2.Value, txtCity.Value, txtPostalCode.Value, txtTelephone.Value, txtOffice.Value, txtWebsiteURL.Value, txtbDesc.Value, pointOfContact.Value, txtnotes.Value, txtUEN.Value, orgID.Value);
                    if (check == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccessMsg('Successfully Updated Org: " + txtOrgNameField.Value + "');", true);
                        enableFields();





                    }
                    else if (check == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                    }
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "hideBtnNext();", true);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);

                }

            }
            else
            {

            }



        }


        private bool IsUrlValid(string url)
        {


            string pattern = @"(http[s]?:\/\/|[a-z]*\.[a-z]{3}\.[a-z]{2})([a-z]*\.[a-z]{3})|([a-z]*\.[a-z]*\.[a-z]{3}\.[a-z]{2})|([a-z]+\.[a-z]{3})";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
        private bool IsUENValid(string uen) {
            string pattern = @"([Uu])+\d{9,10}";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(uen);

        }

        protected void disableFields()
        {
            txtOrgNameField.Disabled = true;
            txtMailAddrLine1.Disabled = true;
            txtMailAddrLine2.Disabled = true;
            txtCity.Disabled = true;
            txtPostalCode.Disabled = true;
            txtTelephone.Disabled = true;
            txtOffice.Disabled = true;
            txtWebsiteURL.Disabled = true;
            txtbDesc.Disabled = true;
            pointOfContact.Disabled = true;
            txtnotes.Disabled = true;
            txtUEN.Disabled = true;



        }
        protected void enableFields()
        {
            txtOrgNameField.Disabled = false;
            txtMailAddrLine1.Disabled = false;
            txtMailAddrLine2.Disabled = false;
            txtCity.Disabled = false;
            txtPostalCode.Disabled = false;
            txtTelephone.Disabled = false;
            txtOffice.Disabled = false;
            txtWebsiteURL.Disabled = false;
            txtbDesc.Disabled = false;
            pointOfContact.Disabled = false;
            txtnotes.Disabled = false;
            txtUEN.Disabled = false;



        }

        protected bool validateORGFields()
        {

            if (string.IsNullOrEmpty(txtOrgNameField.Value.ToString()) || txtOrgNameField.Value.Trim().ToString().Equals(""))
            {

                //error message

                ScriptManager.RegisterStartupScript(Page, GetType(), "script", "displayFailureMsg('Please Check Organisaton Name Field');", true);

                return false;

            }
            else if (string.IsNullOrEmpty(txtMailAddrLine1.Value.ToString()) || txtMailAddrLine1.Value.Trim().ToString().Equals(""))
            {

                //error message

                ScriptManager.RegisterStartupScript(Page, GetType(), "script", "displayFailureMsg('Please Mailing Address Line 1 Field.');", true);

                return false;
            }


            else if (string.IsNullOrEmpty(txtCity.Value.ToString()) || txtCity.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "script", "displayFailureMsg('Please City Field.');", true);

                return false;
            }
            else if (string.IsNullOrEmpty(txtPostalCode.Value.ToString()) || txtPostalCode.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "script", "displayFailureMsg('Please Postal Code Field.');", true);

                return false;
            }
            else if (string.IsNullOrEmpty(txtTelephone.Value.ToString()) || txtTelephone.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "script", "displayFailureMsg('Please Telephone Number Field.');", true);

                return false;
            }
            else if (string.IsNullOrEmpty(txtOffice.Value.ToString()) || txtOffice.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "script", "displayFailureMsg('Please Office Number Field.');", true);


                return false;
            }
            else if (string.IsNullOrEmpty(txtWebsiteURL.Value.ToString()) || txtWebsiteURL.Value.Trim().ToString().Equals(""))
            {
                //if (IsUrlValid(txtWebsiteURL.Value.ToString()).Equals(false))
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Website URL Not Valid. (URL not valid format)');", true);
                //    return false;
                //}
                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Website URL Field.');", true);

                return false;
            }
            else if (string.IsNullOrEmpty(pointOfContact.Value.ToString()) || pointOfContact.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Point of Contact Field.');", true);

                return false;
            }
            else if (string.IsNullOrEmpty(txtnotes.Value.ToString()) || txtnotes.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Notes Field.');", true);

                return false;
            }
            else if (string.IsNullOrEmpty(txtbDesc.Value.ToString()) || txtbDesc.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Business Description Field.');", true);

                return false;
            }
            else if (string.IsNullOrEmpty(txtUEN.Value.ToString()) || txtUEN.Value.Trim().ToString().Equals(""))
            {

                //error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please UEN Field.');", true);

                return false;
            }
            else
            {

                return true;
            }




        }

    }

}
