using TodoList;
using Xamarin.Forms;
[assembly: Dependency(typeof(Logger))]
namespace TodoList
{
    using System;

    public class Logger : ILogger
    {
        public void LogException(Exception exception)
        {
            //Use some tool like Raygun 
        }
    }
}