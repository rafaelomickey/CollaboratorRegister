using AutoFixture;
using CollaboratorRegisterApi.Interfaces.Repositories;
using CollaboratorRegisterApi.Interfaces.Services;
using CollaboratorRegisterApi.Models.Entities;
using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Models.Responses;
using CollaboratorRegisterApi.Services;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CollaboratorRegisterTest.Services
{
    public class CollaboratorServiceTest : BaseServiceTest
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly ICollaboratorService _collaboratorService;
        private readonly ICollaboratorPhoneService _collaboratorPhoneService;

        public CollaboratorServiceTest()
        {
            _collaboratorRepository = Substitute.For<ICollaboratorRepository>();
            _collaboratorPhoneService = Substitute.For<ICollaboratorPhoneService>();
            _collaboratorService = new CollaboratorService(_mapper, _collaboratorPhoneService, _collaboratorRepository);
        }

        [Theory]
        [InlineData(true, null)]
        [InlineData(false, null)]
        [InlineData(false, 2)]
        public async Task Test_Add(bool plateInUse, int? leaderId)
        {
            var request = _fixture.Create<CollaboratorAddRequest>();

            _collaboratorRepository.Exists(Arg.Any<Collaborator>()).Returns(plateInUse);
            var responseGetLeader = leaderId.HasValue && leaderId.Value == 2 ? null : _fixture.Create<IEnumerable<Collaborator>>();
            _collaboratorRepository.Get(Arg.Any<CollaboratorGetRequest>()).Returns(responseGetLeader);
            _collaboratorRepository.Add(Arg.Any<Collaborator>()).Returns(_fixture.Create<int>());

            var result = await _collaboratorService.Add(request);

            if (plateInUse || leaderId.HasValue)
                Assert.Equal(result.StatusCode, StatusCodes.Status409Conflict);
            else
            {
                Assert.NotNull(result);
                Assert.IsType<BaseResponse>(result);
                Assert.Equal(result.StatusCode, StatusCodes.Status201Created);
            }
        }

        [Theory]
        [InlineData(true, null)]
        [InlineData(false, null)]
        [InlineData(false, 2)]
        public async Task Test_Update(bool plateInUse, int? leaderId)
        {
            var request = _fixture.Create<CollaboratorUpdateRequest>();

            _collaboratorRepository.Exists(Arg.Any<Collaborator>(), request.Id).Returns(plateInUse);
            var responseGetLeader = leaderId.HasValue && leaderId.Value == 2 ? null : _fixture.Create<IEnumerable<Collaborator>>();
            _collaboratorRepository.Get(Arg.Any<CollaboratorGetRequest>()).Returns(responseGetLeader);

            var result = await _collaboratorService.Update(request);

            if (plateInUse || leaderId.HasValue)
                Assert.Equal(result.StatusCode, StatusCodes.Status409Conflict);
            else
            {
                Assert.NotNull(result);
                Assert.IsType<BaseResponse>(result);
                Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Test_Delete(bool isLeader)
        {
            var request = _fixture.Create<int>();
            var responseGet = isLeader ? _fixture.Create<IEnumerable<Collaborator>>() : null;
            _collaboratorRepository.Get(Arg.Any<CollaboratorGetRequest>()).Returns(responseGet);

            var result = await _collaboratorService.Delete(request);

            if (isLeader)
                Assert.Equal(result.StatusCode, StatusCodes.Status409Conflict);
            else
            {
                Assert.NotNull(result);
                Assert.IsType<BaseResponse>(result);
                Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
            }
        }

        [Fact]
        public async Task Test_Get()
        {
            var request = _fixture.Create<CollaboratorGetRequest>();
            var responseGet = _fixture.Create<IEnumerable<Collaborator>>();
            _collaboratorRepository.Get(Arg.Any<CollaboratorGetRequest>()).Returns(responseGet);

            var result = await _collaboratorService.Get(request);

            Assert.NotNull(result);
            Assert.IsType<CollaboratorGetResponse>(result);
            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
        }
    }
}
