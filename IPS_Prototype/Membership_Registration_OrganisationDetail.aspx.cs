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

namespace IPS_Prototype
{
    public partial class Membership_Registration_OrganizationDetail : System.Web.UI.Page
    {
        private ArrayList orgList;

        MembershipDAO db = new MembershipDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            orgList = (ArrayList)Session["Person"];




        }

        protected void button_next(object sender, EventArgs e)
        {
            //STORE RESPONSE INTO A SESSION


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

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Org: " + txtOrgNameField.Value + "');", true);


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