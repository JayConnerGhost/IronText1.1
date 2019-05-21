using System;
using System.IO;
using System.Windows.Forms;
using Unity;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class MainController : IMainController
    {
        private readonly IIdeasController _ideasController;
        private readonly IFilesController _filesController;
        private readonly IEditorController _editorController;

        public Container Window { get; set; }
        public UnityContainer DependencyContainer { get; set; }

        internal MainController(IIdeasController ideasController, IFilesController filesController, IEditorController editorController)
        {
         
            _ideasController = ideasController;
          
            _filesController = filesController;
        
            _editorController = editorController;
           

        }

        public void Setup()
        {
            var dockingArea = Window.dockPanel;
            _ideasController.DockingArea = dockingArea;
            _filesController.DockingArea = dockingArea;
            _editorController.DockingArea = dockingArea;
            SetupMenu(Window);
        }

        private void SetupMenu(Container window)
        {
            var mnuFile = new ToolStripMenuItem("File");
            var mnuEdit = new ToolStripMenuItem("Edit");
            var mnuView = new ToolStripMenuItem("View");
            var mnuTools = new ToolStripMenuItem("Tools");
            SetupToolsMenu(mnuTools);
            SetupFileMenu(mnuFile);
            window.MenuStrip.Items.Add(mnuFile);
            window.MenuStrip.Items.Add(mnuEdit);
            window.MenuStrip.Items.Add(mnuView);
            window.MenuStrip.Items.Add(mnuTools);
        }

        private void SetupFileMenu(ToolStripMenuItem mnuFile)
        {
            mnuFile.DropDownItems.Add("New", null, NewFileOnClick);
            mnuFile.DropDownItems.Add("Save", null, SaveFileOnClick);
        }

        private void SaveFileOnClick(object sender, EventArgs e)
        {
            //bug here to explore 
            var activeDocument = Window.dockPanel.ActiveDocument;
            var activeController = ((IEditor) activeDocument).Controller;
            activeController.Save();
        }

        private void NewFileOnClick(object sender, EventArgs e)
        {
            var editor = DependencyContainer.Resolve<IEditor>();
            editor.Show(Window.dockPanel, DockState.Document);
        }

        private void SetupToolsMenu(ToolStripMenuItem mnuTools)
        {
            mnuTools.DropDownItems.Add("Ideas",null,IdeasOnClick);
            mnuTools.DropDownItems.Add("Files",null,FilesOnClick);
            mnuTools.DropDownItems.Add("Spelling",null,SpellingOnClick);
        }

        private void SpellingOnClick(object sender, EventArgs e)
        {
            var activeDocument = Window.dockPanel.ActiveDocument;
            var activeEditor = ((Editor) activeDocument);
            activeEditor.Controller.SpellCheck();
        }

        private void FilesOnClick(object sender, EventArgs e)
        {
          _filesController.Show();
        }

        private void IdeasOnClick(object sender, EventArgs e)
        {
            _ideasController.Show();
        }
    }
}