using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class FilesController : IFilesController
    {
        private Form _window;
        private string initialPath = string.Empty;
        public Form Window

        {
            get => _window;
            set => _window = value;
        }

        public DockPanel DockingArea { get; set; }

        public void Setup()
        {
            //read initial path
            initialPath = ConfigurationManager.AppSettings["InitialFolderPath"];
            BuildFolderBrowser();
        }

        private void BuildFolderBrowser()
        {
            var splitterPanel = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 100,
                SplitterWidth = 6
            };

            ((Form)Window).Controls.Add(splitterPanel);
        }

        public void Show()
        {
            //HACK till i have a better way of dealing with this 
            if (this.Window.IsDisposed)
            {
                this.Window = new Files(this);
            }

            ((DockContent)this._window).Show(DockingArea, DockState.DockLeft);
        }
    }
}