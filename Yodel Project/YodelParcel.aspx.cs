using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Xml.Linq;

public partial class YodelParcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = "";
        XDocument doc = XDocument.Load(Server.MapPath("YodelXML.xml")); 
        if (doc != null)
        {
            result = doc.ToString().Replace("@pickupDateTime", "21:00:00");
        }
       
    }
}