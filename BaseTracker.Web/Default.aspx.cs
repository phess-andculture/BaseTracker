using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseTracker.Code;

namespace BaseTracker.Web {
    public partial class Default : System.Web.UI.Page {
        protected string QueryString {
            get {
                if (this.Request.QueryString["q"] == null) { return null; }
                return this.Request.QueryString["q"].ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(QueryString)) {
                var api = new BasecampAPI();
                rptProjects.DataSource = api.GetProjects();
                rptProjects.DataBind();
                projects.Visible = true;
            }
        }
    }
}