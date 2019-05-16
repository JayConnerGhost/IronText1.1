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
    public partial class Container : Form,IContainer
    {
        public Container(IIdeas ideas, IFiles files, IEditor editor)
        {
            InitializeComponent();
           
            ideas.Show(dockPanel, DockState.DockLeft);
       
            files.Show(dockPanel, DockState.DockRight);
          
            editor.Show(dockPanel, DockState.Document);
        }
    }
}
