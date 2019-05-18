using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class EditorController : IEditorController
    {
        public Form Window { get; set; }
        public DockPanel DockingArea { get; set; }

        public void Setup()
        {
            throw new System.NotImplementedException();
        }
    }
}