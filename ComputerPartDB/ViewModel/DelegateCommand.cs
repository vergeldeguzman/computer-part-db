using System;
using System.Windows.Input;

namespace ComputerPartDb.ViewModel
{
    class DelegateCommand<T> : ICommand
    {
        Action<T> _execute;
        Predicate<T> _canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        /// <summary>
        /// Called when the command is invoked
        /// </summary>
        public void Execute(object parameter) => _execute((T)parameter);

        /// <summary>
        /// This method determines whether the command can execute in its current state.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute((T)parameter);
        }

        /// <summary>
        /// This method to notify that changes occur that affect whether or not the command should execute.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
