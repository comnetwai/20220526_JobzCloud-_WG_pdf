using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Common;

namespace jobzcolud
{
    public partial class JC04Mailkakunin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //20220310 Addedエインドリ－
                if (Request.QueryString["id"] == "JC16")  　 //20220315 Updated id エインドリ－
                {
                    Div_koujiMailkaku.Attributes["style"] = "display:block";
                    Div_load.Attributes["style"] = "display:none";
                    Div_ok.Attributes["style"] = "display:none";
                    Div_error.Attributes["style"] = "display:none";
                    Div_check_error.Attributes["style"] = "display:none";
                    Div_passwordHenkouMailkaku.Attributes["style"] = "display:none"; //20220315 Added　エインドリ－
                }
                else if(Request.QueryString["id"] == "JC17")
                {
                    Div_passwordHenkouMailkaku.Attributes["style"] = "display:block";
                    Div_load.Attributes["style"] = "display:none";
                    Div_ok.Attributes["style"] = "display:none";
                    Div_error.Attributes["style"] = "display:none";
                    Div_check_error.Attributes["style"] = "display:none";
                    Div_koujiMailkaku.Attributes["style"] = "display:none";
                }
                else
                {
                    Div_ok.Attributes["style"] = "display:none";
                    Div_error.Attributes["style"] = "display:none";
                    Div_check_error.Attributes["style"] = "display:none";
                    Div_koujiMailkaku.Attributes["style"] = "display:none"; //20220310 Added エインドリ－
                    Div_passwordHenkouMailkaku.Attributes["style"] = "display:none"; //20220315 Added Phoo

                    if (DBUtilitycs.Server == "")
                    {
                        DBUtilitycs.get_connetion_ifo();
                    }
                    string script = "$(document).ready(function () { $('[id*=BT_Submit]').click(); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                }
                
            }
        }

        protected void BT_Submit_Click(object sender, EventArgs e)
        {
            Div_load.Attributes["style"] = "display:none";

            String strGuid = Request.QueryString["id"];
            if (JC04Mailkakunin_Class.Fu_Check_Guid_value(strGuid) == true)
            {
                DateTime startdate = JC04Mailkakunin_Class.Fu_CreateDate(strGuid);
                try
                {
                    var hours = (DateTime.Now - startdate).TotalHours;
                    if (hours < 24)
                    {
                        if (JC04Mailkakunin_Class.Change_Flag(strGuid))
                        {
                            Div_ok.Attributes["style"] = "display:block";
                            Div_error.Attributes["style"] = "display:none";
                            Div_check_error.Attributes["style"] = "display:none";
                        }
                        else
                        {
                            Div_ok.Attributes["style"] = "display:none";
                            Div_error.Attributes["style"] = "display:block";
                            Div_check_error.Attributes["style"] = "display:none";
                        }
                    }
                    else
                    {
                        Div_ok.Attributes["style"] = "display:none";
                        Div_error.Attributes["style"] = "display:block";
                        Div_check_error.Attributes["style"] = "display:none";
                    }
                }

                catch (Exception)
                {
                    //Handle exception
                }
            }
            else
            {
                Div_ok.Attributes["style"] = "display:none";
                Div_error.Attributes["style"] = "display:none";
                Div_check_error.Attributes["style"] = "display:block";
            }
        }
    }
}