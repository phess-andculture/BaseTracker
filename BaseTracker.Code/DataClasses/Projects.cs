using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTracker.Code.DataClasses {
    public class Projects {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int CompanyID { get; set; }
        public string Company { get; set; }

        public Projects(projectsProject input) {
            this.ID = int.Parse(input.id.First().Value);
            this.Name = input.name;
            this.Status = input.status;
            this.CompanyID = int.Parse(input.company.First().id.First().Value);
            this.Company = input.company.First().name;
        }
    }
}
