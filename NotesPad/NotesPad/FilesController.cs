using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class FilesController : IFilesController
    {
        private Form _window;
        TreeView FolderView;
        ImageList Icons=new ImageList();

        private string initialPath = string.Empty;
        public Form Window

        {
            get => _window;
            set => _window = value;
        }

        public DockPanel DockingArea { get; set; }

        public void Setup()
        {
            var appSettingsReader = new System.Configuration.AppSettingsReader();
            initialPath = (string)appSettingsReader.GetValue("InitialFolderPath", typeof(string));
            SetupIcons(Icons);
            BuildFileBrowser();
            PopulateFolderView(initialPath, FolderView);
        }

        private void SetupIcons(ImageList icons)
        {
            var iconFolderBasePath = "icons/";
            //TODO set up imagelist
            icons.Images.Add("folder",Image.FromFile($"{iconFolderBasePath}/Hopstarter-Sleek-Xp-Basic-Folder-Open.ico"));
            icons.Images.Add("file", Image.FromFile($"{iconFolderBasePath}/icons8-document-52.png"));

        }

        private void BuildFileBrowser()
        {
            var outerContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 100,
                SplitterWidth = 6
            };

            outerContainer.Panel1.Name = "FolderBrowser";
            outerContainer.Panel2.Name = "FileBrowser";

            FolderView = new TreeView()
            {
                Dock = DockStyle.Fill,
                ImageList = Icons
            };

            var fileView = new ListView
            {
                Dock = DockStyle.Fill,
                GridLines = true,
                View = View.List
            };
            outerContainer.Panel1.Controls.Add(FolderView);
            outerContainer.Panel2.Controls.Add(fileView);
            ((Form)Window).Controls.Add(outerContainer);
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

        private void PopulateFolderView(string path,TreeView folderView)
        {
            var info = new DirectoryInfo(path);
            if (!info.Exists) return;
            var rootNode = new TreeNode(info.Name) { Tag = info };
            GetDirectories(info.GetDirectories(), rootNode);
            folderView.Nodes.Add(rootNode);
          
        }
        private void GetDirectories(DirectoryInfo[] getDirectories, TreeNode rootNode)
        {
            foreach (var subDir in getDirectories)
            {
                var aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                try
                {
                    var subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode);
                    }
                }
                catch (System.UnauthorizedAccessException uae)
                {
                    Console.WriteLine(uae.Message);
                }
                rootNode.Nodes.Add(aNode);
            }
        }
    }
}