using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        private ListView _ideasListView;
        private ImageList _icons=new ImageList();
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
            SetupIcons();
            BuildOuterContainer();
            AddToolBar();
            BuildIdeasComponent(_ideasCollection);
        }

        private void SetupIcons()
        {
            var basePath = "icons/";
            _icons.Images.Add("deleteIdea", Image.FromFile($"{basePath}Hopstarter-Sleek-Xp-Basic-Close-2.ico"));
            _icons.Images.Add("addIdea", Image.FromFile($"{basePath}Hopstarter-Soft-Scraps-Button-Add.ico"));
            _icons.Images.Add("checkAllIdeas", Image.FromFile($"{basePath}Iconsmind-Outline-Cursor-Select.ico"));
            _icons.Images.Add("editIdea", Image.FromFile($"{basePath}Oxygen-Icons.org-Oxygen-Actions-document-edit.ico"));
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
            var toolBar = new ToolBar
            {
                Dock = DockStyle.Top,
                ImageList = _icons,
                Appearance = ToolBarAppearance.Flat, Height = 15
            };
            toolBar.ButtonClick += ToolBar_ButtonClick;
            toolBar.Buttons.Add(new ToolBarButton{ToolTipText = "Delete",ImageIndex = 0,Tag = "Delete"});
            toolBar.Buttons.Add(new ToolBarButton{ToolTipText = "Add Idea", ImageIndex = 1, Tag="Add"});
            toolBar.Buttons.Add(new ToolBarButton(){ToolTipText = "Select all", ImageIndex = 2,Tag="SelectAll"});
            toolBar.Buttons.Add(new ToolBarButton(){ToolTipText = "Edit idea", ImageIndex = 3, Tag="Edit"});
           _outerContainer.Controls.Add(toolBar,0,0);
        }

        private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Tag)
            {
                case "Delete":
                    DeleteIdea();
                        
                    break;
                case "Add":
                    AddIdea();
                    break;
                case "SelectAll":
                    SelectAllIdeas();
                    break;
                case "Edit":
                    EditIdea();
                    break;
            }

        }

        private void EditIdea()
        {
            throw new NotImplementedException();
        }

        private void SelectAllIdeas()
        {
            foreach (var idea in _ideasListView.Items)
            {
                ((ListViewItem) idea).Checked = true;
            }
        }

        private void AddIdea()
        {
            var (item1, item2) = new AddIdeaDialog().ShowDialog();
            var itemId=_service.Add((string)item1, (string)item2);
            _ideasListView.Items.Add((string)itemId.ToString(), item1, null);
            _ideasCollection.Add(new Idea(){_id=itemId, Name=item1, Description = item2});
        }

        private void DeleteIdea()
        {
            if (_ideasListView.CheckedItems.Count== 0)
            {
                return;
            }

            foreach (var idea in _ideasListView.CheckedItems)
            {
                var listViewItem = (ListViewItem) idea;
                var workingItemName=listViewItem.Name;
                _service.Delete(Guid.Parse(workingItemName));
                listViewItem.Remove();
            }

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

           
            _ideasListView = new ListView
            {
                Scrollable = true,
                Dock = DockStyle.Fill,
                View = View.Details,
                CheckBoxes = true,
            };
            _ideasListView.Columns.Add("Ideas", -2);
            _ideasListView.Columns[0].Width = _ideasListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;
            _ideasListView.ItemSelectionChanged += IdeasListView_ItemSelectionChanged;


            _ideaDescriptionText = new TextBox {Multiline = true,ReadOnly = true, Dock = DockStyle.Fill};

            splitContainer.Panel1.Controls.Add(_ideasListView);
            splitContainer.Panel2.Controls.Add(_ideaDescriptionText);

            _outerContainer.Controls.Add(splitContainer,0,1);
            foreach (var idea in ideasCollection)
            {
                _ideasListView.Items.Add((string) idea._id.ToString(), idea.Name, null);
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