using System;
using NotesPad.Data;
using NotesPad.Objects;

namespace NotesPad.Services
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaRepository _repository;

        public IdeaService(IIdeaRepository repository)
        {
            _repository = repository;
        }
        public void Add(Idea idea)
        {
            _repository.Save(idea);
        }
    }
}