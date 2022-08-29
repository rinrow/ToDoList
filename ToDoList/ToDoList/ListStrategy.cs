using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public abstract class ListStrategy
    {
        public abstract List<string> GetTasks();
        public abstract List<string> GetTargets();
        public abstract void SetCurrentTarget(Target composite);
    }
}
