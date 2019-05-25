using System;

namespace NotesPad.Objects
{
    public class Idea : IIdea
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}