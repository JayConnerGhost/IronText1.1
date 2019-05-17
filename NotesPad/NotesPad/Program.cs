using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

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
            Application.Run((Form)container);
        }

        private static UnityContainer BuildContainer()
        {
           var container=new UnityContainer();
           container.RegisterType<IIdeasController, IdeasController>();
           container.RegisterType<IFilesController, FilesController>();
           container.RegisterType<IEditorController, EditorController>();
           container.RegisterType<IMainController, MainController>();
           container.RegisterType<IIdeas, Ideas>();
           container.RegisterType<IFiles, Files>();
           container.RegisterType<IEditor, Editor>();
           container.RegisterType<IContainer, Container>();
           return container;
        }
    }
}
