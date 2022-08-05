using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public abstract class ToDoComposite
    {
        public abstract string GetValue();
        public abstract bool IsDone();
        public abstract void MarkAsDone();     

        public virtual int Count  => -1;
        public virtual List<ToDoComposite> Childs=> throw new NotImplementedException();

        //Visitor
        public abstract void Accept(ToDoVisitor visitor);

        public abstract IToDoIterator CreateIterator();

        public virtual ToDoComposite GetChild(string name)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(int index, ToDoComposite child)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(ToDoComposite child)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(ToDoComposite child)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(int index)
        {
            throw new NotImplementedException();
        }
    }
}
