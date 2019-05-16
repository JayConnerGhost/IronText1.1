using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    public interface IFiles
    {
        void Show(DockPanel dockPanel, DockState dockState);
    }
}