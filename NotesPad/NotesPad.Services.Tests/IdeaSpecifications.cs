using System;
using System.Collections.Generic;
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
            var idea = new Idea {Name = "test idea", Description = "test idea description"};
            IIdeaRepository respository=NSubstitute.Substitute.For<IIdeaRepository>();
            //Act
            IIdeaService service=new IdeaService();
            service.Add(idea);

            //Assert
            respository.Received().Save(idea);
        }
    }
}
