using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NotesPad.Data;
using NotesPad.Objects;
namespace NotesPad.Data.Tests
{
    public class ShouldBeAbleToSaveIdeasSpecifications
    {

        [Fact]
        public void Can_save_an_idea_with_a_name()
        {
            //Arrange 
            const string name = "Test Idea";
            const string description = "Test idea description";
            IIdea Idea = new Idea() {Name = name, Description = description};
            IIdeaRepository repository=new IdeaRepository();

            //Act
            repository.Save(Idea);

            //https://github.com/mbdavid/LiteDB/wiki/Repository-Pattern
            //Assert
            var result =repository.FindbyName(name);
            Assert.Equal(description,result.Description);
        }

    }
}
