using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    public interface IController
    {
        Form Window { get; set; }
        DockPanel DockingArea { get; set; }
        void Setup();
    }
}