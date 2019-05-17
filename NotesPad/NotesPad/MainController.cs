namespace NotesPad
{
    internal class MainController : IMainController
    {
        private readonly IIdeasController _ideasController;
        private readonly IFilesController _filesController;
        private readonly IEditorController _editorController;

        internal MainController(IIdeasController ideasController, IFilesController filesController, IEditorController editorController)
        {
            _ideasController = ideasController;
            _filesController = filesController;
            _editorController = editorController;
        }
    }
}