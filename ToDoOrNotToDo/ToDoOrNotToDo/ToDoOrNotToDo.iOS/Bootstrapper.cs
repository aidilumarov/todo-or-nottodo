using System;

namespace ToDoOrNotToDo.iOS
{
    public class Bootstrapper : ToDoOrNotToDo.Bootstrapper
    {
        public static void Init()
        {
            var instance = new Bootstrapper();
        }
    }
}