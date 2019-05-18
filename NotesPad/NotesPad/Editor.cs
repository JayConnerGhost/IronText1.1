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
    public partial class Editor : DockContent,IEditor
    {
        private readonly IEditorController _controller;

        public Editor(IEditorController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.Window = this;
        }
    }
}
