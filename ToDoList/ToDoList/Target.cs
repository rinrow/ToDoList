using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Target : ToDoComposite
    {
        private List<ToDoComposite> _childs;
        private string _name;

        public override List<ToDoComposite> Childs => _childs;
        public override bool HasChild => true;
        public override int Count => _childs.Count;

        public event Action<ToDoComposite> ChildAdded;

        public Target(string name)
        {
            _childs = new List<ToDoComposite>();
            _name = name;
        }

        public override bool IsDone()
        {
            foreach (var child in _childs)
                if (!child.IsDone())
                    return false;
            return true;
        }

        public override void MarkAsDone()
        {
            throw new InvalidOperationException();
        }

        public override string GetValue()
        {
            return _name;
        }

        public override ToDoComposite GetChild(string value)
        {
            var task = _childs
                .Where(t => t.GetValue() == value)
                .FirstOrDefault();
            return task;
        }

        public override void Add(ToDoComposite child)
        {
            _childs.Add(child);
            ChildAdded?.Invoke(child);
        }

        public override void Insert(int index, ToDoComposite child)
        {
            _childs.Insert(index, child);
        }

        public override void Remove(int index)
        {
            _childs.RemoveAt(index);
        }

        public override void Remove(ToDoComposite child)
        {
            _childs.Remove(child);
        }

        public override IToDoIterator CreateIterator()
        {
            return new TargetIterator(this);
        }

        public override void Accept(ToDoVisitor visitor)
        {
            visitor.VisistTarget(this);
        }
    }
}
