/*
		  __ _/| _/. _  ._/__ /
		_\/_// /_///_// / /_|/
				   _/
		sof digital 2020 (mit license)
		written by michael rinderle <michael@sofdigital.net>
*/

using System;
using System.Windows.Input;

namespace BenfordsElection.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter ?? "<N/A>");
        }
    }
}
