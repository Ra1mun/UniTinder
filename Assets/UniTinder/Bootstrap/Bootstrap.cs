using UniTinder.Bootstrap.Interfaces;
using UnityEngine;

namespace UniTinder.Bootstrap
{
    public class Bootstrap : ProcessingCommand
    {
        private ICommand _currentCommand;
        private bool _canExecute = true;

        public void StartExecute()
        {
            Execute();
        }

        private void Execute()
        {
            if (!_canExecute)
            {
                return;
            }

            IsExecuting = true;
            _canExecute = false;

            _currentCommand = Dequeue();
            if (_currentCommand == null)
            {
                IsExecuting = false;
                _canExecute = true;
                OnComplete();
            }
            else
            {
                _currentCommand.Done += CurrentCommandDone;
                _currentCommand.Execute();
            }
        }

        private void CurrentCommandDone()
        {
            _currentCommand.Done -= CurrentCommandDone;
            _canExecute = true;
        }
    }
}
