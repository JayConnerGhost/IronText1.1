using System;
using System.Collections.Generic;
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

        public void Add( string name, string description)
        {
            _repository.Save(new Idea() {Name = name, Description = description});
        }

        public void Update(Guid id, string name, string description)
        {
            var target=_repository.GetById(id);
            target.Name = name;
            target.Description = description;
            _repository.Update(target);
        }

        public IList<Idea> GetList()
        {
            return _repository.GetAll();
        }

        public void DeleteAll()
        {
            _repository.DeleteAll();
        }

        public void Delete(Guid id)
        {
           _repository.Delete(id);
        }
    }
}