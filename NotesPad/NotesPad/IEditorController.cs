using System.ComponentModel;

namespace NotesPad
{
    public interface IEditorController: IController
    {
        void Show();
        void SpellCheck();
    }
}