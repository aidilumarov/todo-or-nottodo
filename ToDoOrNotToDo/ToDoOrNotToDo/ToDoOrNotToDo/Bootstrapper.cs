using Autofac;
using System;
using System.Linq;
using System.Reflection;
using ToDoOrNotToDo.Repositories;
using ToDoOrNotToDo.ViewModels;
using Xamarin.Forms;

namespace ToDoOrNotToDo
{
    public abstract class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder { get; private set; }

        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();

            foreach (var type in currentAssembly.DefinedTypes
                .Where(e => 
                        e.IsSubclassOf(typeof(Page)) ||
                        e.IsSubclassOf(typeof(BaseViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            ContainerBuilder.RegisterType<TodoItemRepository>().SingleInstance();
        }

        private void FinishInitialization()
        {
            Resolver.Initialize(ContainerBuilder.Build());
        }
    }
}
