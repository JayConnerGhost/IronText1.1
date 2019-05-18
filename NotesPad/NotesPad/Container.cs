using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    public partial class Container : Form, IContainer
    {
        private readonly IMainController _controller;

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
            
            ideas.Show(dockPanel, DockState.DockLeft);

            files.Show(dockPanel, DockState.DockRight);

            editor.Show(dockPanel, DockState.Document);
         
        }

    }
}
