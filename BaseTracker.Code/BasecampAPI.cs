using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BaseTracker.Code.DataClasses;

namespace BaseTracker.Code {
    public class BasecampAPI : Base {
        private string ServiceURL = "https://andculture.basecamphq.com";
        private string APIToken = "4f5a43ed5d1a0122ca50ba16cb16ca784bbb8f7a:X";

        public DataClasses.Projects[] GetProjects() {
            var projectsBase = DeserializeFromResponse<projects>(new StreamReader(CreateWebRequest(ServiceURL + "/projects.xml", "GET", APIToken).GetResponseStream()).ReadToEnd());
            var projects = new List<DataClasses.Projects>();
            for (var i = 0; i < projectsBase.project.Count(); i ++) {
	            var proj = new DataClasses.Projects(projectsBase.project[i]);
				var todoBase = DeserializeFromResponse<todolists>(new StreamReader(CreateWebRequest(ServiceURL + "/projects/" + proj.ID + "/todo_lists.xml?filter=pending", "GET", APIToken).GetResponseStream()).ReadToEnd());
				for (var j = 0; j < todoBase.todolist.Count(); j ++) {
					var todo = new TodoLists(todoBase.todolist[j]);
					if (!todo.TimeTracked) { continue; }
					var todoItemBase = DeserializeFromResponse<todolist>(new StreamReader(CreateWebRequest(ServiceURL + "/todo_lists/" + todo.ID, "GET", APIToken).GetResponseStream()).ReadToEnd());
					proj.TodoLists.Add(new TodoLists(todoItemBase));
				}
				projects.Add(proj);
            }
            return projects.OrderBy(x => x.Company).ThenBy(x => x.Name).ToArray();
        }
		public DataClasses.Projects GetProject(int id) {
			var projectBase = DeserializeFromResponse<project>(new StreamReader(CreateWebRequest(ServiceURL + "/projects/" + id, "GET", APIToken).GetResponseStream()).ReadToEnd());
			return new Projects(projectBase);
		}
		public DataClasses.Projects GetTodo(int id) {
			var todoBase = DeserializeFromResponse<todolist>(new StreamReader(CreateWebRequest(ServiceURL + "/todo_lists/" + id, "GET", APIToken).GetResponseStream()).ReadToEnd());
			var todo = new TodoLists(todoBase);
			var project = GetProject(todo.ProjectID);
			project.TodoLists.Add(todo);
			return project;
		}
		public DataClasses.Projects GetItem(int id) {
			var todoItemBase = DeserializeFromResponse<todoitem>(new StreamReader(CreateWebRequest(ServiceURL + "/todo_items/" + id, "GET", APIToken).GetResponseStream()).ReadToEnd());
			var toItem = new List<DataClasses.TodoItem>() { new TodoItem(todoItemBase) };
			var proj = GetTodo(toItem.First().TodoID);
			return proj;
		}
    }
}
