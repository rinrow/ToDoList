using System.Collections.Generic;

namespace ToDoList
{
    public class CachedListStrategy : ListStrategy
    {
        private List<string> _tasks;
        private List<string> _targets;
        private Target _currentTarget;

        public CachedListStrategy()
        {
            _tasks = new List<string>();
            _targets = new List<string>();
        }

        public override void SetCurrentTarget(Target target)
        {
            _currentTarget = target;
            var simpleStrategy = new SimpleListStrategy();
            simpleStrategy.SetCurrentTarget(_currentTarget);
            _tasks = simpleStrategy.GetTasks();
            _targets = simpleStrategy.GetTargets();

            _currentTarget.ChildAdded += c =>
            {
                if (c.HasChild)
                    _targets.Add(c.GetValue());
                else
                    _tasks.Add(c.GetValue());
            };
        }

        public override List<string> GetTargets() => _targets;

        public override List<string> GetTasks() => _tasks;
    }
}
