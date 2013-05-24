using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTracker.Code.DataClasses {
	public class TodoItem {
		public bool Completed { get; set; }
		public string Name { get; set; }
		public int ID { get; set; }
		public int TodoID { get; set; }

		public TodoItem(todoitem input) {
			this.Completed = bool.Parse(input.completed.First().Value);
			this.Name = input.content;
			this.ID = int.Parse(input.id.First().Value);
			this.TodoID = int.Parse(input.todolistid.First().Value);
		}
	}
}
