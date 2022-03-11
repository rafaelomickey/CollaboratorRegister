using AutoFixture;
using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Validators;
using FluentAssertions;
using Xunit;

namespace CollaboratorRegisterTest.Validators
{
    public class CollaboratorAddValidatorTest : BaseValidatorTest<CollaboratorAddValidator>
    {
        private CollaboratorAddRequest _request;
        public CollaboratorAddValidatorTest()
        {
            _validator = new CollaboratorAddValidator();
        }

        [Theory]
        [InlineData(false, "", "", "", "")]
        [InlineData(false, "", "abc", "abc", "abc")]
        [InlineData(false, "abc", "", "abc", "abc")]
        [InlineData(false, "abc", "abc", "", "abc")]
        [InlineData(false, "abc", "abc", "abc", "")]
        [InlineData(true, "abc", "abc", "abc", "abc")]
        public void Test_Collaborator_Add_Validator(bool expectedResult, string name, string mail, string plateNumber, string password)
        {
            _request = _fixture.Create<CollaboratorAddRequest>();
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
