using System;
using System.ComponentModel;

namespace NotesPad
{
    public interface IFilesController: IController
    {
        void Show();
        event EventHandler OpenFile;
        void LoadFolderViewFromPath(string path);
    }
}