using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace p1_encrypt_decrypt_app.ViewModels
{
    class ViewModelCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;
        //Constructors
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }
        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }
        //Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //Methods
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }
        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }

    //class ViewModelCommand<T> : ICommand
    //{
    //    private readonly Predicate<T> _canExecute;
    //    private readonly Action<T> _execute;

    //    public ViewModelCommand(Predicate<T> canExecute, Action<T> execute)
    //    {
    //        if (execute == null)
    //            throw new ArgumentNullException("execute");
    //        _canExecute = canExecute;
    //        _execute = execute;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        try
    //        {
    //            return _canExecute == null ? true : _canExecute((T)parameter);
    //        }
    //        catch
    //        {
    //            return true;
    //        }
    //    }

    //    public void Execute(object parameter)
    //    {
    //        _execute((T)parameter);
    //    }

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }
}
