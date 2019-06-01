using System.ComponentModel;

namespace NotesPad
{
    public interface IEditorController: IController
    {
        void Show();
        void SpellCheck();
        void Save();
        void OpenFile(string path, string fileName);
        void SelectAllText();
        void Cut();
        void Copy();
    }
}