using AutoFixture;
using CollaboratorRegisterApi.Interfaces.Repositories;
using CollaboratorRegisterApi.Interfaces.Services;
using CollaboratorRegisterApi.Models.Entities;
using CollaboratorRegisterApi.Services;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CollaboratorRegisterTest.Services
{
    public class CollaboratorPhoneServiceTest : BaseServiceTest
    {
        private readonly ICollaboratorPhoneRepository _collaboratorPhoneRepository;
        private readonly ICollaboratorPhoneService _collaboratorPhoneService;

        public CollaboratorPhoneServiceTest()
        {
            _collaboratorPhoneRepository = Substitute.For<ICollaboratorPhoneRepository>();
            _collaboratorPhoneService = new CollaboratorPhoneService(_collaboratorPhoneRepository);
        }

        [Fact]
        public async Task Test_Add()
        {
            var newCollaboratorId = _fixture.Create<int>();
            var phones = _fixture.Create<IEnumerable<CollaboratorPhone>>();

            await _collaboratorPhoneService.Add(newCollaboratorId, phones);
        }

        [Fact]
        public async Task Test_Get()
        {
            var collaboratorId = _fixture.Create<int>();
            var response = _fixture.Create<IEnumerable<CollaboratorPhone>>();

            _collaboratorPhoneService.Get(collaboratorId).Returns(response);

            var result = await _collaboratorPhoneService.Get(collaboratorId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Test_Delete()
        {
            var newCollaboratorId = _fixture.Create<int>();
            await _collaboratorPhoneService.Delete(newCollaboratorId);
        }
    }
}
