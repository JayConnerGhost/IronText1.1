using System.Windows.Forms;

namespace NotesPad
{
    internal class MainController : IMainController
    {
        private readonly IIdeasController _ideasController;
        private readonly IFilesController _filesController;
        private readonly IEditorController _editorController;

        public Container Window { get; set; }

        internal MainController(IIdeasController ideasController, IFilesController filesController, IEditorController editorController)
        {
            _ideasController = ideasController;
            _filesController = filesController;
            _editorController = editorController;
            
        }

        public void Setup()
        {
            SetupMenu(Window);
        }

        private void SetupMenu(Container window)
        {
            var mnuFile = new ToolStripMenuItem("File");
            var mnuEdit = new ToolStripMenuItem("Edit");
            var mnuView = new ToolStripMenuItem("View");
            var mnuTools = new ToolStripMenuItem("Tools");
            SetupToolsMenu(mnuTools);
            window.MenuStrip.Items.Add(mnuFile);
            window.MenuStrip.Items.Add(mnuEdit);
            window.MenuStrip.Items.Add(mnuView);
            window.MenuStrip.Items.Add(mnuTools);
        }

        private void SetupToolsMenu(ToolStripMenuItem mnuTools)
        {
            mnuTools.DropDownItems.Add("Ideas");
        }
    }
}