using System;
using System.Collections.Generic;
using UniTinder.Bootstrap.Interfaces;

namespace UniTinder.Bootstrap
{
    public class ProcessingCommand
    {
        public event Action AllCommandsDone;
        public bool IsExecuting { get; protected set; }
        protected Queue<ICommand> Queue => _queue;

        private readonly Queue<ICommand> _queue = new Queue<ICommand>();

        protected int Count => _queue.Count;

        public void AddCommand(ICommand command)
        {
            if (command == null)
            {
                return;
            }
            
            _queue.Enqueue(command);
        }

        protected ICommand Dequeue()
        {
            return Count > 0 ? _queue.Dequeue() : null;
        }

        protected void OnComplete()
        {
            AllCommandsDone?.Invoke();
        }
    }
}
