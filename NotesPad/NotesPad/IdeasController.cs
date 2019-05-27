using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using NotesPad.Objects;
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
            var ideasCollection = _service.GetList();
            //TODO:Code in here to display Ideas Name
            BuildIdeasComponent(ideasCollection);
            
        }

        private void BuildIdeasComponent(IList<Idea> ideasCollection)
        {
            SplitContainer splitContainer = new SplitContainer
            {
                Orientation = Orientation.Horizontal, Dock = DockStyle.Fill
            };
            splitContainer.Panel1.Name = "Names";
            splitContainer.Panel2.Name = "Description";

            var ideasListView = new ListView {Dock = DockStyle.Fill};
            var ideaText = new TextBox {Multiline = true, Dock = DockStyle.Fill};

            splitContainer.Panel1.Controls.Add(ideasListView);
            splitContainer.Panel2.Controls.Add(ideaText);

            _window.Controls.Add(splitContainer);
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