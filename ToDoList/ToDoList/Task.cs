using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Task : ToDoComposite
    {
        private string _task;
        private bool _isDone;
            
        public Task(string task)
        {
            _task = task;
            _isDone = false;
        }

        public override void MarkAsDone()
        {
            _isDone = true;
        }

        public override string GetValue()
        {
            return _task;
        }

        public override bool IsDone()
        {
            return _isDone;
        }

        public override IToDoIterator CreateIterator()
        {
            return new NullIterator(this);
        }

        public override void Accept(ToDoVisitor visitor)
        {
            visitor.VisistTask(this);
        }
    }
}
