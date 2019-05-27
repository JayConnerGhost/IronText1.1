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
           // BuildDevelopmentData();//remove when no longer needed 
            //TODO:Code in here to get ideas 
            var ideasCollection = _service.GetList();
            //TODO:Code in here to display Ideas Name
            BuildIdeasComponent(ideasCollection);
        }

        private void BuildDevelopmentData()
        {
            _service.Add("test 1","test description 1");
            _service.Add("test 2","test description 1");
            _service.Add("test 3","test description 1");
            _service.Add("test 4","test description 1");
            _service.Add("test 5","test description 1");
            _service.Add("test 6","test description 1");
            _service.Add("test 7","test description 1");
            _service.Add("test 8","test description 1");
        }

        private void BuildIdeasComponent(IList<Idea> ideasCollection)
        {
            SplitContainer splitContainer = new SplitContainer
            {
                Orientation = Orientation.Horizontal, Dock = DockStyle.Fill
            };
            splitContainer.Panel1.Name = "Names";
            splitContainer.Panel2.Name = "Description";

            var ideasListView = new ListView
            {
                Dock = DockStyle.Fill,
                GridLines = true,
                View = View.Details,
                CheckBoxes = true,
                AllowColumnReorder = true,
                FullRowSelect = true,
                Sorting = SortOrder.Ascending
            };
            ideasListView.ItemSelectionChanged += IdeasListView_ItemSelectionChanged;

            ideasListView.Columns.Add("Ideas", -2);
            var ideaText = new TextBox {Multiline = true, Dock = DockStyle.Fill};

            splitContainer.Panel1.Controls.Add(ideasListView);
            splitContainer.Panel2.Controls.Add(ideaText);

            _window.Controls.Add(splitContainer);

            foreach (var idea in ideasCollection)
            {
                ideasListView.Items.Add((string) idea._id.ToString(), idea.Name, null);

            }

            //TODO:Code in here to wire up ideas edit event 
            //TODO:Code in here to wire up ideas add event 
            //TODO:Code in here to wire up ideas delete event 
            //TODO:Code in here to delete all Ideas 
        }

        private void IdeasListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
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