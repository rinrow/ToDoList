using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class SimpleListStrategy : ListStrategy
    {
        private Target _current;

        public override List<string> GetTargets()
        {
            CheckReference();

            return _current.Childs
            .Where(a => a.HasChild)
            .Select(a => a.GetValue())
            .ToList();
        }

        public override List<string> GetTasks()
        {
            CheckReference();

            return _current.Childs
            .Where(a => !a.HasChild)
            .Select(a => a.GetValue())
            .ToList();
        }

        private void CheckReference()
        {
            if (_current == null)
                throw new NullReferenceException();
        }

        public override void SetCurrentTarget(Target composite)
        {
            _current = composite;
        }
    }
}
