using System.Collections.Generic;
using System.ComponentModel;
using NotesPad.Objects;

namespace NotesPad
{
    public interface IIdeasController: IController
    {
        void Show();
        IList<Idea> GetIdeas();
    }
}