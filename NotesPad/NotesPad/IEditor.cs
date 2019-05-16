using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    public interface IEditor
    {
        void Show(DockPanel dockPanel, DockState dockState);
    }
}