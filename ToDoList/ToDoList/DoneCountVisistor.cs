using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class DoneCountVisistor : ToDoVisitor
    {
        private int _count = 0;
        private int _doneCount = 0;

        private int _tasksCount = 0;
        private int _doneTasksCount = 0;
        public override void VisistTarget(Target target)
        {
            _count++;
            if (target.IsDone())
                _doneCount++;
        }

        public override void VisistTask(Task task)
        {
            _count++;
            _tasksCount++;
            if (task.IsDone())
            {
                _doneTasksCount++;
                _doneCount++;
            }
        }

        public int GetDoneTasksPercent()
        {
            if (_tasksCount == 0)
                return 0;

            float result = _doneTasksCount / (float)_tasksCount * 100;
            return (int)result;
        }

        public int GetDonePercent()
        {
            if (_count == 0)
                return 0;

            float result = _doneCount / (float)_count * 100;
            return (int)result;
        }
    }
}
