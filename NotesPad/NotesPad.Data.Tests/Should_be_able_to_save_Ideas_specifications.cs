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
            repository.DeleteAll();

            //Act
            repository.Save(Idea);

            //https://github.com/mbdavid/LiteDB/wiki/Repository-Pattern
            //Assert
            var result =repository.FindbyName(name);
            Assert.Equal(description,result.Description);
            repository.DeleteAll();
        }

        [Fact]
        public void Can_delete_all_record_in_collection()
        {
            //Arrange
            const string name = "Test Idea";
            const string description = "Test idea description";
            IIdea Idea = new Idea() { Name = name, Description = description };

            const string name2 = "Test Idea";
            const string description2 = "Test idea description";
            IIdea Idea2 = new Idea() { Name = name, Description = description };
            IIdeaRepository repository = new IdeaRepository();
            repository.DeleteAll();
            repository.Save(Idea);
            repository.Save(Idea2);

            //Act
            repository.DeleteAll();

            //Assert
            var result = repository.GetAll();
            Assert.Equal(0,result.Count);
        }

        [Fact]
        public void Can_retrieve_a_collection_of_ideas()
        {
            //Arrange
            const string name = "Test Idea";
            const string description = "Test idea description";
            IIdea Idea = new Idea() { Name = name, Description = description };

            const string name2 = "Test Idea";
            const string description2 = "Test idea description";
            IIdea Idea2 = new Idea() { Name = name, Description = description };
            IIdeaRepository repository = new IdeaRepository();
            repository.DeleteAll();
            repository.Save(Idea);
            repository.Save(Idea2);
            //Act
            var result = repository.GetAll();

            //Assert
            Assert.Equal(2, result.Count);
        }

    }
}
