using AutoFixture;
using CollaboratorRegisterApi.Controllers;
using CollaboratorRegisterApi.Interfaces.Services;
using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CollaboratorRegisterTest.Controller
{
    public class CollaboratorControllerTest : BaseControllerTest
    {
        private readonly ICollaboratorService _collaboratorService;
        private readonly CollaboratorController _collaboratorController;
        private readonly ILogger<CollaboratorController> _logger;

        public CollaboratorControllerTest()
        {
            _logger = Substitute.For<ILogger<CollaboratorController>>();
            _collaboratorService = Substitute.For<ICollaboratorService>();
            _collaboratorController = new CollaboratorController(_logger, _collaboratorService);
        }

        [Fact]
        public async Task Test_Get()
        {
            var request = _fixture.Create<CollaboratorGetRequest>();
            var response = _fixture.Create<CollaboratorGetResponse>();

            _collaboratorService.Get(request).Returns(response);

            var result = await _collaboratorController.Get(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Test_Add()
        {
            var request = _fixture.Create<CollaboratorAddRequest>();
            var response = _fixture.Create<BaseResponse>();

            _collaboratorService.Add(request).Returns(response);

            var result = await _collaboratorController.Add(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Test_Update()
        {
            var request = _fixture.Create<CollaboratorUpdateRequest>();
            var response = _fixture.Create<BaseResponse>();

            _collaboratorService.Update(request).Returns(response);

            var result = await _collaboratorController.Update(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Test_Delete()
        {
            var request = _fixture.Create<int>();
            var response = _fixture.Create<BaseResponse>();

            _collaboratorService.Delete(request).Returns(response);

            var result = await _collaboratorController.Delete(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }
    }
}
