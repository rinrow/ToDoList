using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public abstract class ToDoVisitor
    {
        public abstract void VisistTask(Task task);
        public abstract void VisistTarget(Target target);
    }
}
