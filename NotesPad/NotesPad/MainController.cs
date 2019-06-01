using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Unity;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class MainController : IMainController
    {
        private readonly IIdeasController _ideasController;
        private readonly IFilesController _filesController;
        private readonly IEditorController _editorController;
        private readonly ImageList _icons=new ImageList();
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
            SetupIcons(_icons);
            SetupMenu(Window);
            SetupEvents();
        }

        private void SetupEvents()
        {
            _filesController.OpenFile += filesController_OpenFile;
        }
        private void LoadEditerWithFileFromPath(string filePath)
        {
            if (IsFileAlreadyOpen(filePath))
            {
                return;
            }
            var editor = DependencyContainer.Resolve<IEditor>();
            editor.Show(Window.dockPanel, DockState.Document);
            ((DockContent)editor).Tag = (string)filePath;
            var fileName = Path.GetFileName(filePath);
            editor.Controller.OpenFile(filePath, fileName);
        }

        private void filesController_OpenFile(object sender, EventArgs e)
        {
            var listViewItemSelectionChangedEventArgs = (ListViewItemSelectionChangedEventArgs)e;
            var listViewItem = listViewItemSelectionChangedEventArgs.Item;
            var path = (string)listViewItem.Tag;
            if (IsFileAlreadyOpen(path))
            {
                return;
            }
            var editor = DependencyContainer.Resolve<IEditor>();
            editor.Show(Window.dockPanel, DockState.Document);
            ((DockContent) editor).Tag = (string) path;
            editor.Controller.OpenFile(path, listViewItem.Name);
            
        }

        private bool IsFileAlreadyOpen(string path)
        {
            var documents = this.Window.dockPanel.Documents;
            var matchingDocuments=documents.FirstOrDefault(d => ((string)((Editor)d).Tag) == path);
            return matchingDocuments != null;
        }

        private void SetupIcons(ImageList icons)
        {
            //TODO code in here to load icons for menus
            var IconBasePath="icons/";
            icons.Images.Add("newFile", Image.FromFile($"{IconBasePath}icons8-document-52.png"));
            icons.Images.Add("saveFile", Image.FromFile($"{IconBasePath}Oxygen-Icons.org-Oxygen-Actions-document-save.ico"));
            icons.Images.Add("saveAsFile", Image.FromFile($"{IconBasePath}Oxygen-Icons.org-Oxygen-Actions-document-save-as.ico"));
            icons.Images.Add("saveAllFile", Image.FromFile($"{IconBasePath}Actions-document-save-all-icon.png"));
            icons.Images.Add("openFile", Image.FromFile($"{IconBasePath}Custom-Icon-Design-Pretty-Office-9-Open-file.ico"));
            icons.Images.Add("exitFile", Image.FromFile($"{IconBasePath}Hopstarter-Soft-Scraps-Button-Close.ico"));
            icons.Images.Add("cutEdit", Image.FromFile($"{IconBasePath}Alecive-Flatwoken-Apps-Actions-Cut.ico"));
            icons.Images.Add("pasteEdit", Image.FromFile($"{IconBasePath}Everaldo-Kids-Icons-Edit-paste.ico"));
            icons.Images.Add("copyEdit", Image.FromFile($"{IconBasePath}Hopstarter-Soft-Scraps-Document-Copy.ico"));
            icons.Images.Add("selectAllEdit", Image.FromFile($"{IconBasePath}Saki-NuoveXT-2-Actions-select-all.ico"));
            icons.Images.Add("toolsFileBrowser", Image.FromFile($"{IconBasePath}Custom-Icon-Design-Flatastic-8-File-explorer.ico"));
            icons.Images.Add("toolsIdeas", Image.FromFile($"{IconBasePath}Iconsmind-Outline-Idea-2.ico"));
            icons.Images.Add("toolsSpelling", Image.FromFile($"{IconBasePath}Oxygen-Icons.org-Oxygen-Actions-tools-check-spelling.ico"));
            icons.Images.Add("fileOpenFolder", Image.FromFile($"{IconBasePath}Avosoft-Warm-Toolbar-Folder-open.ico"));
            icons.Images.Add("CopyIdeasToDocuments", Image.FromFile($"{IconBasePath}Custom-Icon-Design-Flatastic-2-Data-add.ico"));
        }

        private void SetupMenu(Container window)
        {
            //TODO Icons for menu actions
            var mnuFile = new ToolStripMenuItem("File");
            var mnuEdit = new ToolStripMenuItem("Edit");
            var mnuView = new ToolStripMenuItem("View");
            var mnuTools = new ToolStripMenuItem("Tools");
            SetupToolsMenu(mnuTools);
            SetupFileMenu(mnuFile);
            SetupEditMenu(mnuEdit);
            window.MenuStrip.ImageList = _icons;
            window.MenuStrip.Items.Add(mnuFile);
            window.MenuStrip.Items.Add(mnuEdit);
            window.MenuStrip.Items.Add(mnuView);
            window.MenuStrip.Items.Add(mnuTools);
        }

        private void SetupEditMenu(ToolStripMenuItem mnuEdit)
        {
            var selectAllMenuItem = new ToolStripMenuItem("Select All", null, SelectAllEditOnClick, Keys.Alt | Keys.A)
            {
                Image = _icons.Images[9]
            };
            mnuEdit.DropDownItems.Add(selectAllMenuItem);
            var cutMenuItem = new ToolStripMenuItem("Cut", null, CutEditOnClick, Keys.Alt | Keys.U)
            {
                Image = _icons.Images[7]
            };
            mnuEdit.DropDownItems.Add(cutMenuItem);
            var copyMenuItems=new ToolStripMenuItem("Copy",null,CopyEditOnClick,Keys.Alt | Keys.C);
        }

        private void CopyEditOnClick(object sender, EventArgs e)
        {
            var activeDocument = Window.dockPanel.ActiveDocument;
            var editor = (Editor)activeDocument;
            editor.Controller.Copy();
        }

        private void CutEditOnClick(object sender, EventArgs e)
        {
            var activeDocument = Window.dockPanel.ActiveDocument;
            var editor = (Editor)activeDocument;
            editor.Controller.Cut();
        }

        private void SelectAllEditOnClick(object sender, EventArgs e)
        {
            var activeDocument = Window.dockPanel.ActiveDocument;
            var editor = (Editor) activeDocument;
            editor.Controller.SelectAllText();
        }

        private void SetupFileMenu(ToolStripMenuItem mnuFile)
        {
            var newMenuItem = new ToolStripMenuItem("New", null, NewFileOnClick, Keys.Control | Keys.N)
            {
                Image = _icons.Images[0]
            };
            mnuFile.DropDownItems.Add(newMenuItem);
            var saveMenuItem = new ToolStripMenuItem("Save", null, SaveFileOnClick, Keys.Control | Keys.S)
            {
                Image = _icons.Images[1]
            };
            mnuFile.DropDownItems.Add(saveMenuItem);

            var openFolderMenuItem=new ToolStripMenuItem("Open Folder",null,OpenFolderOnClick, Keys.Control | Keys.F)
            {
                Image=_icons.Images[13]
            };
            mnuFile.DropDownItems.Add(openFolderMenuItem);

            var openFileMenuItem = new ToolStripMenuItem("Open File", null, OpenFileOnClick, Keys.Control | Keys.I)
            {
                Image=_icons.Images[4]
            };
            mnuFile.DropDownItems.Add(openFileMenuItem);

            var closeApplicationMenuItem=new ToolStripMenuItem("Exit", null, CloseApplicationFileOnClick, Keys.Control | Keys.Q)
            {
                Image = _icons.Images[5]
            };

            mnuFile.DropDownItems.Add(closeApplicationMenuItem);
        }

        private void CloseApplicationFileOnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenFileOnClick(object sender, EventArgs e)
        {
            using (var filePicker = new CommonOpenFileDialog())
            {
                var result = filePicker.ShowDialog();
                if (result == CommonFileDialogResult.Cancel)
                {
                    return;
                }

                var filePath = filePicker.FileName;
                LoadEditerWithFileFromPath(filePath);
            }
        }
        
        private void OpenFolderOnClick(object sender, EventArgs e)
        {
            var folderPicker = new CommonOpenFileDialog { IsFolderPicker = true };
            var result=folderPicker.ShowDialog();
            if (result == CommonFileDialogResult.Cancel)
            {
                return;
            }
            var path = folderPicker.FileName;
            
            _filesController.LoadFolderViewFromPath(path);
        }

        private void SaveFileOnClick(object sender, EventArgs e)
        {
          
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
            var ideasMenuItem = new ToolStripMenuItem("Ideas", null, IdeasOnClick, Keys.Alt | Keys.I)
            {
                Image = _icons.Images[11]
            };
            mnuTools.DropDownItems.Add(ideasMenuItem);
            var fileBrowserMenuItem =
                new ToolStripMenuItem("Files", null, FilesOnClick, Keys.Alt | Keys.F)
                    { Image = _icons.Images[10]};
            mnuTools.DropDownItems.Add(fileBrowserMenuItem);
            var spellingMenuItem =new ToolStripMenuItem("Spelling", null, SpellingOnClick, Keys.Alt | Keys.S) {Image = _icons.Images[12]};
            var copyIdeasMenuItem =new ToolStripMenuItem("Ideas to Document", null, IdeasToDocumentOnClick, Keys.Alt | Keys.S) {Image = _icons.Images[14]};
            mnuTools.DropDownItems.Add(spellingMenuItem);
            mnuTools.DropDownItems.Add(copyIdeasMenuItem);
        }

        private void IdeasToDocumentOnClick(object sender, EventArgs e)
        {
            var ideas=_ideasController.GetIdeas();
           
            var activeDocument=Window.dockPanel.ActiveDocument;
            var editor = (Editor) activeDocument;
            var text=(RichTextBox)editor.Controls[0];
            foreach (var idea in ideas)
            {
                text.Text = text.Text + idea.Description + Environment.NewLine;
            }
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