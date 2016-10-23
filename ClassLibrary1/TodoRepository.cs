using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using ClassLibrary1;
using ClassLibrary2;

namespace Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
        // Shorter way to write this in C# using ?? operator :
        // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
        // x ?? y -> if x is not null , expression returns x. Else y.
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null) throw new ArgumentNullException();
            else if (_inMemoryTodoDatabase.Contains(todoItem)) throw new DuplicateTodoItemException();
            else _inMemoryTodoDatabase.Add(todoItem);
        }

        public TodoItem Get(Guid todoId)
        {
            TodoItem todoItem = _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
            return todoItem;
        }

        private bool Contains(TodoItem todoItem)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoItem.Id).FirstOrDefault();
            if (item == default(TodoItem)) return false;
            return true;
        }

        public List<TodoItem> GetActive(Func<TodoItem, bool> filter)
        {
            List<TodoItem> list = new List<TodoItem>(_inMemoryTodoDatabase.Where(i => i.IsCompleted == filter(i)).ToList());
            return list;
        }

        public List<TodoItem> GetAll()
        {
            List<TodoItem> list = new List<TodoItem>(_inMemoryTodoDatabase.ToList());            
            return list;
        }

        public List<TodoItem> GetCompleted(Func<TodoItem, bool> filter)
        {
            List<TodoItem> list = new List<TodoItem>(_inMemoryTodoDatabase.Where(i => i.IsCompleted == filter(i)).ToList());
            return list;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            List<TodoItem> list = new List<TodoItem>(_inMemoryTodoDatabase.Where(i => i.IsCompleted == filterFunction(i)).ToList());
            return list;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
            if (item == default(TodoItem)) return false;
            else return item.MarkAsCompleted();                
        }

        public bool Remove(Guid todoId)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
            if (item == default(TodoItem)) return false;
            else return item.Remove();
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoItem.Id).FirstOrDefault();
            if (item == default(TodoItem)) _inMemoryTodoDatabase.Add(todoItem);
            else item.Update(todoItem);
        }
    }
}
