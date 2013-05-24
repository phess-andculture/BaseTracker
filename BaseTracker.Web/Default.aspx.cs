using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseTracker.Code;

namespace BaseTracker.Web {
    public partial class Default : System.Web.UI.Page {
		protected string ItemID {
			get { return QueryString.Substring(1); } 
		}
        protected string QueryString {
            get {
                if (this.Request.QueryString["q"] == null) { return null; }
                return this.Request.QueryString["q"].ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
			var api = new BasecampAPI();
            if (string.IsNullOrEmpty(QueryString)) {
	            rptProjects.DataSource = api.GetProjects();
	            rptProjects.DataBind();
	            projects.Visible = true;
            } else {
	            to_do_timer.Visible = true;
				if (QueryString.StartsWith("P")) {
					var proj = api.GetProject(int.Parse(QueryString.Substring(1)));
					lblParent.Text = proj.Name;
				} else if (QueryString.StartsWith("T")) {
					var proj = api.GetTodo(int.Parse(QueryString.Substring(1)));
					lblParent.Text = proj.Name + " > " + proj.TodoLists.First().Name;
				} else {
					var proj = api.GetItem(int.Parse(QueryString.Substring(1)));
					var tdoList = proj.TodoLists.First().ListItem.Where(x => x.ID == int.Parse(QueryString.Substring(1))).FirstOrDefault();
					lblParent.Text = proj.Name + " > " + proj.TodoLists.First().Name + " > " + tdoList.Name;
				}
				
            }
        }
    }
}