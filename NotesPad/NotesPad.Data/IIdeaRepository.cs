using NotesPad.Objects;

namespace NotesPad.Data
{
    public interface IIdeaRepository
    {
        void Save(IIdea idea);
        IIdea FindbyName(string testIdea);
    }
}