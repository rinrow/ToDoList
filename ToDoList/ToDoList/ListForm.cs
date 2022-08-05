using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class ListForm : Form
    {
        private TableLayoutPanel _table;
        private ListModel _model;
        private ListBox _tasksBox;
        private ListBox _targetsBox;
        private TextBox _input;
        private Label _currentTargetLabel;
        private ProgressBar _progressBar;

        public ListForm(ListModel model)
        {
            ClientSize = new Size(600, 400);

            _table = new TableLayoutPanel();
            _model = model;

            #region Controls
            _input = new TextBox()
            {
                Dock = DockStyle.Fill
            };

            var addTaskButton = new Button()
            {
                Text = "AddTask",
                Dock = DockStyle.Fill
            };

            var addTargetButton = new Button()
            {
                Text = "AddTarget",
                Dock = DockStyle.Fill
            };

            var removeButton = new Button()
            {
                Text = "Remove",
                Dock = DockStyle.Fill,
                Enabled = false
            };

            var undoButton = new Button()
            {
                Text = "Undo",
                Dock = DockStyle.Fill,
                Enabled = false
            };

            var returnButton = new Button()
            {
                Text = "<-",
                Dock = DockStyle.Fill
            };

            _progressBar = new ProgressBar()
            {
                Value = 50,
                Dock = DockStyle.Fill
            };

            var markAsDoneButton = new Button()
            {
                Text = "MarkAsDone",
                Dock = DockStyle.Top,
                Enabled = false
            };

            _currentTargetLabel = new Label()
            {
                Text = _model.CurrentTarget.GetValue(),
                Dock = DockStyle.Bottom
            };

            _tasksBox = new ListBox()
            {
                Dock = DockStyle.Fill
            };

            _targetsBox = new ListBox()
            {
                Dock = DockStyle.Fill
            };
            #endregion

            #region buttonsIntertable
            var buttonsInterTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            buttonsInterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            buttonsInterTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            buttonsInterTable.Controls.Add(undoButton, 0, 0);
            buttonsInterTable.Controls.Add(removeButton, 1, 0);
            #endregion

            #region boxesIntertable
            var boxesIntertable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };

            boxesIntertable.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            boxesIntertable.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            boxesIntertable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            boxesIntertable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

            boxesIntertable.Controls.Add(_targetsBox, 0, 0);
            boxesIntertable.Controls.Add(returnButton, 0, 1);
            boxesIntertable.Controls.Add(_tasksBox, 1, 0);
            boxesIntertable.Controls.Add(_progressBar, 1, 1);
            #endregion

            #region MainTable
            _table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            _table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            _table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
            _table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            _table.Controls.Add(new Panel(), 0, 0);
            _table.Controls.Add(_input, 1, 1);
            _table.Controls.Add(buttonsInterTable, 1, 2);
            _table.Controls.Add(boxesIntertable, 1, 3);
            _table.Controls.Add(_currentTargetLabel, 2, 0);
            _table.Controls.Add(addTaskButton, 2, 1);
            _table.Controls.Add(addTargetButton, 2, 2);
            _table.Controls.Add(markAsDoneButton, 2, 3);
            _table.Dock = DockStyle.Fill;
            #endregion

            Controls.Add(_table);

            #region Events
            addTaskButton.Click += (sender, args) => model.AddTask(_input.Text);
            addTargetButton.Click += (sender, args) => model.AddTarget(_input.Text);

            model.Changed += UpdateState;

            _input.KeyDown += (sender, args) =>
            {
                string text = _input.Text;
                if (args.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(text))
                    model.AddTask(text);
            };

            removeButton.Click += (sender, args) =>
            {
                var value = _tasksBox.SelectedItem;
                model.Remove(value.ToString());
                UpdateState();
                removeButton.Enabled = false;
                undoButton.Enabled = true;
            };

            undoButton.Click += (sender, args) =>
            {
                _model.Undo();
                if (!_model.CanUndo)
                    undoButton.Enabled = false;
            };

            returnButton.Click += (sender, args) =>
            {
                UpdateState();
                if (_model.CanReturnToPrevious)
                    _model.ReturnToPreviousTarget();
            };

            int lastIndex = default;
            _targetsBox.SelectedIndexChanged += (sender, args) =>
            {
                if (lastIndex == _targetsBox.SelectedIndex && _targetsBox.SelectedItem != null)
                    model.OpenTarget(_targetsBox.SelectedItem.ToString());
                lastIndex = _targetsBox.SelectedIndex;
            };

            markAsDoneButton.Click += (sender, args) =>
            {
                _model.MarkAsDone(_tasksBox.SelectedItem.ToString());
                markAsDoneButton.Enabled = false;
            };

            _tasksBox.SelectedIndexChanged += (sender, args) =>
            {
                if (_tasksBox.SelectedItem == null)
                    return;
                removeButton.Enabled = true;
                markAsDoneButton.Enabled = !_model.IsDone(_tasksBox.SelectedItem.ToString());
            };
            #endregion
        }

        private void UpdateState()
        {
            _input.Text = null;

            _currentTargetLabel.Text = _model.CurrentTarget.GetValue();

            _tasksBox.Items.Clear();
            _tasksBox.Items.AddRange(_model.CurrentTasks.ToArray());

            _targetsBox.Items.Clear();
            _targetsBox.Items.AddRange(_model.CurrentTargets.ToArray());

            _progressBar.Value = _model.CalculateDonePercent();
        }
    }
}
