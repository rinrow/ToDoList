using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class OpenTargetCommand : ICommand
    {
        private ListModel _model;
        private string _targetToOpenName;
        private Target _previousTarget;

        public OpenTargetCommand(ListModel model, string targetToOpenName)
        {
            _model = model;
            _targetToOpenName = targetToOpenName;
        }

        public void Execute()
        {
            var target = _model.CurrentTarget.Childs
                .Where(a => a.GetValue() == _targetToOpenName && a.Count >= 0)
                .FirstOrDefault();

            if (target == null)
                throw new IndexOutOfRangeException();
            _previousTarget = _model.CurrentTarget;
            //Плохая реализация as Target(зацепка на оссобенностях яп
            _model.CurrentTarget = target as Target;
        }

        public void Undo()
        {
            _model.CurrentTarget = _previousTarget;
        }
    }
}
