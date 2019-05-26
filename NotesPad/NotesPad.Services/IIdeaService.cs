using System;
using System.Collections;
using System.Collections.Generic;
using NotesPad.Objects;

namespace NotesPad.Services
{
    public interface IIdeaService
    {
        void Add(string name, string description);
        void Update(Guid id, string name, string description);
        IList<Idea> GetList();
        void DeleteAll();
    }
}