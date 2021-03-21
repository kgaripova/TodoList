namespace TodoList
{
    using System;

    public interface ILogger
    {
        void LogException(Exception exception);
    }
}