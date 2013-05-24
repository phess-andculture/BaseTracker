using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaseTracker.Code {
    public class BasecampAPI : Base {
        private string ServiceURL = "https://andculture.basecamphq.com";
        private string APIToken = "4f5a43ed5d1a0122ca50ba16cb16ca784bbb8f7a:X";

        public DataClasses.Projects[] GetProjects() {
            var projectsBase = DeserializeFromResponse<projects>(new StreamReader(CreateWebRequest(ServiceURL + "/projects.xml", "GET", APIToken).GetResponseStream()).ReadToEnd());
            var projects = new List<DataClasses.Projects>();
            for (int i = 0; i < projectsBase.project.Count(); i ++) {
                projects.Add(new DataClasses.Projects(projectsBase.project[i]));
            }
            return projects.OrderBy(x => x.Company).ThenBy(x => x.Name).ToArray();
        }
    }
}
