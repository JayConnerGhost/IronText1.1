using System.Windows.Forms;

namespace NotesPad
{
    public interface IMainController
    {
        Container Window { get; set; }
        void Setup();
    }
}