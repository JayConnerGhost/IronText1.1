using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesPad.Data;
using NotesPad.Objects;
using NSubstitute;
using Xunit;

namespace NotesPad.Services.Tests
{
    public class IdeaSpecifications
    {
        [Fact]
        public void Repository_is_called_add_idea_is_called_from_service()
        {
            //Arrange 
            Guid Id = Guid.NewGuid();
            const string Name = "test";
            const string Description = "Test description";
            IIdeaRepository respository=NSubstitute.Substitute.For<IIdeaRepository>();
            //Act
            IIdeaService service=new IdeaService(respository);
            service.Add(Name,Description);

            //Assert
            respository.Received().Save(Arg.Is<Idea>(x => x.Name == Name));
        }

        [Fact]
        public void Repository_is_called_when_updating_an_idea()
        {
            //Arrange
            IIdeaRepository repository=Substitute.For<IIdeaRepository>();
            Guid Id=Guid.NewGuid();
            repository.GetById(Id).Returns(new Idea() {Name = "old", Description = "Old description", _id = Id});

            const string Name = "Test";
            const string Description = "Test Description";

            IIdeaService service=new IdeaService(repository);

            //Act
            service.Update(Id, Name, Description);

            //Assert
            repository.Received().Update(Arg.Is<Idea>(x=>x.Name==Name && x.Description==Description));
        }

        [Fact]
        public void Repository_is_called_when_getting_all_ideas()
        {
            //Arrange 
            IIdeaRepository repository=Substitute.For<IIdeaRepository>();
            IIdeaService service=new IdeaService(repository);

            //Act
            service.GetList();

            //Assert
            repository.Received().GetAll();
        }

        [Fact]
        public void Repository_is_called_when_deleting_all_ideas()
        {
            //Arrange 
            IIdeaRepository repository = Substitute.For<IIdeaRepository>();
            IIdeaService service = new IdeaService(repository);

            //Act
            service.DeleteAll();

            //Assert
            repository.Received().DeleteAll();

        }

        [Fact]
        public void Repository_is_called_when_deleting_an_idea()
        {
            //Arrange
            var id = Guid.NewGuid();
            IIdeaRepository repository = Substitute.For<IIdeaRepository>();
            IIdeaService service = new IdeaService(repository);

            //Act
            service.Delete(id);
            //Assert
            repository.Received().Delete(Arg.Any<Guid>());

        }
    }
}
