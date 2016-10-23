using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public TodoItem(string text)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
        }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool Remove()
        {
            Id = default(Guid);
            Text = default(string);
            IsCompleted = false;
            DateCompleted = default(DateTime);
            DateCreated = default(DateTime);
            return true;
        }
        
        public void Update(TodoItem newItem)
        {
            Id = newItem.Id;
            Text = newItem.Text;
            IsCompleted = newItem.IsCompleted;
            DateCompleted = newItem.DateCompleted;
            DateCreated = newItem.DateCreated;
        }            
    }
}
