using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesPad
{
    public class DialogForm : Form
    {
        public DialogForm(FormInfo info) : base()
        {
            this.Height = info.FormHeight;
            this.Width = info.FormWidth;
            this.Text = info.FormCaption;
        }
    }
}
