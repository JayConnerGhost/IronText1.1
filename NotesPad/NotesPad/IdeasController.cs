using System.ComponentModel;
using System.Windows.Forms;
using NotesPad.Services;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    public class IdeasController : IIdeasController
    {
        private readonly IIdeaService _service;

        private Form _window;

        public IdeasController(IIdeaService service)
        {
            _service = service;
        }

        public Form Window
        {
            get => _window;
            set => _window = value;
        }

        public DockPanel DockingArea { get; set; }

        public void Setup()
        {
            //TODO:Code in here to get ideas 
            //TODO:Code in here to display Ideas Name
            //TODO:Code in here to display ideas description 
            //TODO:Code in here to wire up ideas edit event 
            //TODO:Code in here to wire up ideas add event 
            //TODO:Code in here to wire up ideas delete event 
            //TODO:Code in here to delete all Ideas 
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