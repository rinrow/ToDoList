using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public interface IToDoIterator
    {
        void First();
        ToDoComposite GetCurrent();
        bool IsDone();
        void Next();
    }
}
