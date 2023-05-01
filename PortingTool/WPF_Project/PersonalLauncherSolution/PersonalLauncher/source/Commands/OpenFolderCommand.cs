using System;
using System.Windows.Input;

namespace PersonalLauncher.Commands
{
    internal class ActionCommand<TParam> : ICommand
    {
        Action<TParam> action;
        public ActionCommand(Action<TParam> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            action((TParam)parameter);
        }
    }
}
