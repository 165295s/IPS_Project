using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class Membership_Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (MemebershipDDL.SelectedIndex != 0)
            {


            }
            else
            {

            }

        }
        protected void Button_click1(Object sender, EventArgs e)
        {

            string type, donorTier, expDate ;
            if (CA.Checked == true)
            {
                type = CA.Value;
            }
            else {
                type = IA.Value;
            }
            donorTier = MemebershipDDL.SelectedItem.ToString();
            expDate = hiddentext.Value;

            //ArrayList arPerson = Session["Person"] as ArrayList;
            //if (arPerson == null)
            //{
            //    arPerson = new ArrayList();
            //    Session["Person"] = arPerson;
            //}
            //if (Request.QueryString.Get("Person") != null)
            //{
            //    string memType, donTier, memExpiry;

            //    //memType = Request.QueryString.Get("ProductId");
            //    //arProduct.Add(productId);
            //    //arPerson.Add(optradio.is)
            //    arPerson.Add(type);
            //    arPerson.Add(donorTier);
            //    arPerson.Add(expDate);
            //}

            ArrayList pList = new ArrayList();
            pList.Add(type);
            pList.Add(donorTier);
            pList.Add(expDate);
            Session["Person"] = pList;
            if (type.Equals("Coporate Associate")){
                Session["EDIT_ORG_ID"] = null;
                Response.Redirect("Membership_Registration_OrganisationDetail.aspx");
            }
            else {
                Response.Redirect("Membership_Registration_IndividualDetail.aspx");
            }
            }
        protected void DdlSelected(object sender, EventArgs e) {
            
            if (MemebershipDDL.SelectedIndex != 0)
            {
                //calendarbtn.Attributes.CssStyle.Add("pointer-events", "none");
                //calendarInput.Value = "";
            }
            else{

                //calendarbtn.Attributes.CssStyle.Add("pointer-events", "auto");

            }



        }
    }
}