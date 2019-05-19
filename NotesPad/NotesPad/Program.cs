using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace NotesPad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var dependencyContainer = BuildContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = dependencyContainer.Resolve<IContainer>();
            container.DependencyContainer = dependencyContainer;
            Application.Run((Form)container);
        }

        private static UnityContainer BuildContainer()
        {
           var container=new UnityContainer();
           container.RegisterType<IIdeasController, IdeasController>(new ContainerControlledLifetimeManager());
           container.RegisterType<IFilesController, FilesController>(new ContainerControlledLifetimeManager());
           container.RegisterType<IEditorController, EditorController>(new ContainerControlledLifetimeManager());
           container.RegisterType<IMainController, MainController>(new ContainerControlledLifetimeManager());
           container.RegisterType<IIdeas, Ideas>(new ContainerControlledLifetimeManager());
           container.RegisterType<IFiles, Files>(new ContainerControlledLifetimeManager());
           container.RegisterType<IEditor, Editor>();
           container.RegisterType<IContainer, Container>(new ContainerControlledLifetimeManager());
           return container;
        }
    }
}
