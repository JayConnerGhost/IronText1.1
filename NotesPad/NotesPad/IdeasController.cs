using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NotesPad.Objects;
using NotesPad.Services;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    public class IdeasController : IIdeasController
    {
        private readonly IIdeaService _service;
        private IList<Idea> _ideasCollection;
        private TextBox _ideaDescriptionText;
        private TableLayoutPanel _outerContainer;
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
            //BuildDevelopmentData();//remove when no longer needed 
            _ideasCollection = _service.GetList();
            BuildOuterContainer();
            AddToolBar();
            BuildIdeasComponent(_ideasCollection);
        }

        private void BuildOuterContainer()
        {
            _outerContainer = new TableLayoutPanel {Dock = DockStyle.Fill};
            _outerContainer.ColumnStyles.Add(new ColumnStyle());
            _outerContainer.RowStyles.Add(new RowStyle());
            _outerContainer.RowStyles.Add(new RowStyle());
            _window.Controls.Add(_outerContainer);
        }

        private void AddToolBar()
        {
            var toolBar = new ToolBar {Dock = DockStyle.Top};
            toolBar.Appearance = ToolBarAppearance.Flat;
            toolBar.Buttons.Add(new ToolBarButton("Delete"));
           // Window.Controls.Add(toolBar);
           _outerContainer.Controls.Add(toolBar,0,0);
        }

        private void BuildDevelopmentData()
        {
            _service.Add("test 1","test description 1");
            _service.Add("test 2","test description 2");
            _service.Add("test 3","test description 3");
            _service.Add("test 4","test description 4");
            _service.Add("test 5","test description 5");
            _service.Add("test 6","test description 6");
            _service.Add("test 7","test description 7");
            _service.Add("test 8","test description 8");
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
                Scrollable = true,
                Dock = DockStyle.Fill,
                View = View.Details,
                CheckBoxes = true,
            };
            ideasListView.Columns.Add("Ideas", -2);
            ideasListView.ItemSelectionChanged += IdeasListView_ItemSelectionChanged;


            _ideaDescriptionText = new TextBox {Multiline = true, Dock = DockStyle.Fill};

            splitContainer.Panel1.Controls.Add(ideasListView);
            splitContainer.Panel2.Controls.Add(_ideaDescriptionText);

            //_window.Controls.Add(splitContainer);
            _outerContainer.Controls.Add(splitContainer,0,1);
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
            if (!e.IsSelected) return;
            var selectedItem= _ideasCollection.Where(x => x._id == Guid.Parse(e.Item.Name)).First();
            _ideaDescriptionText.Text = selectedItem.Description;
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