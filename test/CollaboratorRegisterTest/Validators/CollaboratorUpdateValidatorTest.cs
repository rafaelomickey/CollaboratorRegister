using AutoFixture;
using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Validators;
using FluentAssertions;
using Xunit;

namespace CollaboratorRegisterTest.Validators
{
    public class CollaboratorUpdateValidatorTest : BaseValidatorTest<CollaboratorUpdateValidator>
    {
        private CollaboratorUpdateRequest _request;
        public CollaboratorUpdateValidatorTest()
        {
            _validator = new CollaboratorUpdateValidator();
        }

        [Theory]
        [InlineData(false, 0, "", "", "", "")]
        [InlineData(false, 0, "abc", "abc", "abc", "abc")]
        [InlineData(false, 1, "", "abc", "abc", "abc")]
        [InlineData(false, 1, "abc", "", "abc", "abc")]
        [InlineData(false, 1, "abc", "abc", "", "abc")]
        [InlineData(false, 1, "abc", "abc", "abc", "")]
        [InlineData(true, 1, "abc", "abc", "abc", "abc")]
        public void Test_Collaborator_Update_Validator(bool expectedResult, int id, string name, string mail, string plateNumber, string password)
        {
            _request = _fixture.Create<CollaboratorUpdateRequest>();
            _request.Id = id;
            _request.Name = name;
            _request.Mail = mail;
            _request.PlateNumber = plateNumber;
            _request.Password = password;

            var validationResult = _validator.Validate(_request);

            if (expectedResult)
                validationResult.IsValid.Should().BeTrue();
            else
                validationResult.IsValid.Should().BeFalse();
        }
    }
}
