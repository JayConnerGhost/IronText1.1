namespace NotesPad
{
    public interface IController
    {
        Container Window { get; set; }
        void Setup();
    }
}