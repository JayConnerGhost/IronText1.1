using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    public partial class Container : Form, IContainer
    {
        private readonly IMainController _controller;
        private UnityContainer _dependencyContainer;

        public Container(IIdeas ideas, IFiles files, IEditor editor, IMainController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.Window = this;
             SetupInitialDocking(ideas,files,editor);
            _controller.Setup();
        }

        private void SetupInitialDocking(IIdeas ideas, IFiles files, IEditor editor)
        {
            dockPanel.Theme = vS2015DarkTheme1;
            dockPanel.DockLeftPortion = 225.0;
            dockPanel.DockRightPortion = 225.0;


            ideas.Show(dockPanel, DockState.DockLeft);

            files.Show(dockPanel, DockState.DockLeft);

            editor.Show(dockPanel, DockState.Document);
         
        }

        public UnityContainer DependencyContainer
        {
            get => _dependencyContainer;
            set
            {
                _dependencyContainer = value;
                _controller.DependencyContainer = _dependencyContainer;
            }
        }
    }
}
