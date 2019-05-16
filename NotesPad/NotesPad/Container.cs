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
    public partial class Container : Form
    {
        public Container()
        {
            InitializeComponent();
            Ideas ideas = new Ideas();
            ideas.Show(dockPanel, DockState.DockLeft);
            Files files = new Files();
            files.Show(dockPanel, DockState.DockRight);
            Editor editor = new Editor();
            editor.Show(dockPanel, DockState.Document);
        }
    }
}
