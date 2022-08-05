using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class NullIterator : IToDoIterator
    {
        private ToDoComposite _toDoComposite;

        public NullIterator(ToDoComposite composite)
        {
            _toDoComposite = composite;
        }

        public void First()
        {
            
        }

        public ToDoComposite GetCurrent()
        {
            return _toDoComposite;
        }

        public bool IsDone()
        {
            return true;
        }

        public void Next()
        {
            
        }
    }
}
