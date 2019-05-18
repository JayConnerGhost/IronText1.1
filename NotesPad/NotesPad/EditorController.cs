using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class EditorController : IEditorController
    {
        private Form _window;

        public Form Window
        {
            get => _window;
            set => _window = value;
        }

        public DockPanel DockingArea { get; set; }

        public void Setup()
        {
            BuildRTFControl();
        }

        private void BuildRTFControl()
        {
            var richTextBox = new RichTextBox {Dock = DockStyle.Fill};
            Window.Controls.Add(richTextBox);
        }

        public void Show()
        {
            //HACK till i have a better way of dealing with this 
            if (this.Window.IsDisposed)
            {
                this.Window = new Editor(this);
            }

            ((DockContent)this._window).Show(DockingArea, DockState.Document);
        }
    }
}