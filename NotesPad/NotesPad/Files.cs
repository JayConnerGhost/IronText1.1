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
    public partial class Files : DockContent,IFiles
    {
        private readonly IFilesController _controller;

        public Files(IFilesController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.Window = this;

        }

     
    }
}
