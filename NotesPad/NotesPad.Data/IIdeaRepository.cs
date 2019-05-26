using System.Collections;
using System.Collections.Generic;
using NotesPad.Objects;

namespace NotesPad.Data
{
    public interface IIdeaRepository
    {
        void Save(IIdea idea);
        Idea FindbyName(string testIdea);
        void DeleteAll();
        IList<Idea> GetAll();
    }
}