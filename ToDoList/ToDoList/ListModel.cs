using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class ListModel
    {
        private Target _currentTarget;
        private Stack<ICommand> _taskCommands;
        private Stack<ICommand> _enterToTargetCommands;
        private ListStrategy _listStrategy = new CachedListStrategy();

        public Target CurrentTarget
        {
            get => _currentTarget;
            set
            {
                _currentTarget = value;
                _listStrategy.SetCurrentTarget(_currentTarget);
            }
        }

        public bool CanUndo => _taskCommands.Count > 0;
        public bool CanReturnToPrevious => _enterToTargetCommands.Count > 0;

        public List<string> CurrentTasks => _listStrategy.GetTasks();
        public List<string> CurrentTargets => _listStrategy.GetTargets();

        public event Action Changed;

        public ListModel()
        {
            CurrentTarget = new Target("Найти счастье");
            _taskCommands = new Stack<ICommand>();
            _enterToTargetCommands = new Stack<ICommand>();
        }

        #region OperationsWithTasksAndTArgets
        public void OpenTarget(string name)
        {
            var command = new OpenTargetCommand(this, name);
            command.Execute();
            _enterToTargetCommands.Push(command);
            Changed?.Invoke();
        }

        public void ReturnToPreviousTarget()
        {
            _enterToTargetCommands.Pop().Undo();
            Changed?.Invoke();
        }

        public void AddTarget(string targetName)
        {
            //if (CurrentTarget != null)
                CurrentTarget.Add(new Target(targetName));
            //else
            //    CurrentTarget = new Target(targetName);
            Changed?.Invoke();
        }

        public void AddTask(string text)
        {
            CurrentTarget.Add(new Task(text));
            Changed?.Invoke();
        }

        public void AddTask(int index, string text)
        {
            CurrentTarget.Insert(index, new Task(text));
            Changed?.Invoke();
        }

        public void Remove(string value)
        {
            var command = new DeleteTaskCommand(this, value);
            command.Execute();
            _taskCommands.Push(command);
            Changed?.Invoke();
        }

        public void Undo()
        {
            _taskCommands.Pop().Undo();
            Changed?.Invoke();
        }
        #endregion

        public void MarkAsDone(string targetValue)
        {
            var result = _currentTarget.Childs
                .Where(a => a.GetValue() == targetValue)
                .FirstOrDefault();
            if (result == null)
                throw new ArgumentOutOfRangeException();

            result.MarkAsDone();
            Changed?.Invoke();
        }

        public bool IsDone(string targetVvalue)
        {
            var result = _currentTarget.Childs
                .Where(a => a.GetValue() == targetVvalue)
                .FirstOrDefault();
            if (result == null)
                throw new ArgumentOutOfRangeException();

            return result.IsDone();
        }

        public int CalculateDonePercent()
        {
            var i = _currentTarget.CreateIterator();
            var visitor = new DoneCountVisistor();
            for (i.First(); !i.IsDone(); i.Next())
            {
                i.GetCurrent().Accept(visitor);
            }

            return visitor.GetDoneTasksPercent();
        }
    }
}
