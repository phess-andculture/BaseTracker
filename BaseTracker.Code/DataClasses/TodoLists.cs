using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTracker.Code.DataClasses {
	public class TodoLists {
		public int CompletedCount { get; set; }
		public string Description { get; set; }
		public int ID { get; set; }
		public string Name { get; set; }
		public int Position { get; set; }
		public int ProjectID { get; set; }
		public bool TimeTracked { get; set; }
		public TodoListItem[] ListItem { get; set; }

		public TodoLists(todolistsTodolist input) {
			this.CompletedCount = int.Parse(input.completedcount.First().Value);
			this.Description = input.description;
			this.ID = int.Parse(input.id.First().Value);
			this.Name = input.name;
			this.Position = int.Parse(input.position.First().Value);
			this.ProjectID = int.Parse(input.projectid.First().Value);
			this.TimeTracked = bool.Parse(input.tracked.First().Value);
		}
		public TodoLists(todolist input) {
			this.CompletedCount = int.Parse(input.completedcount.First().Value);
			this.Description = input.description;
			this.ID = int.Parse(input.id.First().Value);
			this.Name = input.name;
			this.Position = int.Parse(input.position.First().Value);
			this.ProjectID = int.Parse(input.projectid.First().Value);
			this.TimeTracked = bool.Parse(input.tracked.First().Value);
			var todo = new List<TodoListItem>();
			for (var i = 0; i < input.todoitems.todoitem.Count(); i++) {
				var todoItem = new TodoListItem(input.todoitems.todoitem[i]);
				if (!todoItem.Completed) { todo.Add(todoItem); }
			}
			ListItem = todo.ToArray();
		}
	}
}
