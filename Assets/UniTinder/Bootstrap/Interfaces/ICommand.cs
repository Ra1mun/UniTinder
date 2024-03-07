using System;

namespace UniTinder.Bootstrap.Interfaces
{
    public interface ICommand
    {
        Action Done { get; set; }
        void Execute();
    }
}