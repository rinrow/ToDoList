using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class TargetIterator : IToDoIterator
    {
        private ToDoComposite _target;
        private ToDoComposite _current;

        private Stack<ToDoComposite> _compositions;
        private HashSet<ToDoComposite> _visited;

        private bool _isDone = false;

        public TargetIterator(ToDoComposite taskComposite)
        {
            _target = taskComposite;
            _compositions = new Stack<ToDoComposite>();
            _visited = new HashSet<ToDoComposite>();
            _current = _target;
            _isDone = false;
        }

        public void First()
        {
            var childs = _target.Childs;
            for (int i = childs.Count - 1; i >= 0; i--)
            {
                _compositions.Push(childs[i]);
            }
            Next();
        }

        public ToDoComposite GetCurrent()
        {
            return _current;
        }

        public bool IsDone()
        {
            return _isDone;
        }

        public void Next()
        {
            if (_compositions.Count == 0)
            {
                _isDone = true;
                return;
            }

            _current = _compositions.Pop();

            while (_visited.Contains(_current) && _compositions.Count > 0)
                _current = _compositions.Pop();
            _visited.Add(_current);
            if (_current.Count != -1)
                for (int i = _current.Childs.Count - 1; i >= 0; i--)
                {
                    var child = _current.Childs[i];
                    _compositions.Push(child);
                }
        }
    }
}
