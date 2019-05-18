using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class IdeasController : IIdeasController
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
        }

        public void Show()
        {
            //HACK till i have a better way of dealing with this 
            if (this.Window.IsDisposed)
            {
                this.Window=new Ideas(this);
            }

            ((DockContent)this._window).Show(DockingArea,DockState.DockLeft);
        }
    }
}