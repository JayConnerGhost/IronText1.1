using System;
using System.Collections;
using System.Collections.Generic;
using NotesPad.Objects;

namespace NotesPad.Data
{
    public interface IIdeaRepository
    {
        Guid Save(IIdea idea);
        Idea FindbyName(string testIdea);
        void DeleteAll();
        IList<Idea> GetAll();
        void Update(Idea targetIdea);
        Idea GetById(Guid ideaId);
        void Delete(Guid ideaId);
    }
}