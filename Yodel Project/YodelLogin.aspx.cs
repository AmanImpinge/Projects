using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YodelLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    } 
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            var client = new RestClient();
            client.EndPoint = "https://www.vtp.netdespatch.com/mba/40286x0/login/";
            client.Method = HttpVerb.POST; 
            client.PostData = txtRequest.Text;// data.ToString();
            var json = client.MakeRequest();
            json.ToString();
        }
        catch (Exception ex)
        { 
        }
    }
}