using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using LiteDB;
using NotesPad.Objects;
using Xunit;

namespace NotesPad.Data
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly LiteDatabase _database;
        private string _connectionString = "";
        private LiteCollection<Idea> _ideas;

        public IdeaRepository()
        {
            var appSettingsReader = new System.Configuration.AppSettingsReader();
            _connectionString = (string) appSettingsReader.GetValue("ConnectionString", typeof(string));
            _database = new LiteDatabase(_connectionString);

            _ideas = _database.GetCollection<Idea>("Ideas");
            _ideas.EnsureIndex("Name");
        }

        public void Save(IIdea idea)
        {
            var ideaToStore = (Idea) idea;
            ideaToStore._id = Guid.NewGuid();
            _ideas.Insert(ideaToStore);
        }

        public Idea FindbyName(string testIdea)
        {
            var results = _ideas.Find(x => x.Name == testIdea);
            return results.ToList().FirstOrDefault();
        }

        public void DeleteAll()
        {
            var records = _ideas.Find(Query.All());
            foreach (var record in records)
            {
                _ideas.Delete(record._id);
            }
        }

        public IList<Idea> GetAll()
        {
            var results = _ideas.FindAll();
            return results.ToList();
        }
    }
}