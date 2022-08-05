using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class DeleteTaskCommand : ICommand
    {
        private ListModel _listModel;
        private string _value;
        private int _copmositeIndex;
        private ToDoComposite _task;
        private List<ToDoComposite> _composites;
        public DeleteTaskCommand(ListModel model, string value)
        {
            _listModel = model;
            _value = value;
        }

        public void Execute()
        {
            _composites = _listModel.CurrentTarget.Childs;
            _task = _composites
                .Where(a => a.GetValue() == _value)
                .FirstOrDefault();
            if (_task == null)
                throw new IndexOutOfRangeException();
            _copmositeIndex = _composites.IndexOf(_task);
            _composites.Remove(_task);
        }

        public void Undo()
        {
            if (_task == null)
                throw new NullReferenceException();
            _composites.Insert(_copmositeIndex, _task);
        }
    }
}
