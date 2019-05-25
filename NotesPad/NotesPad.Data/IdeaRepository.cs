using System;
using NotesPad.Objects;

namespace NotesPad.Data
{
    public class IdeaRepository : IIdeaRepository
    {
        public void Save(IIdea idea)
        {
            throw new NotImplementedException();
        }

        public IIdea FindbyName(string testIdea)
        {
            throw new NotImplementedException();
        }
    }
}