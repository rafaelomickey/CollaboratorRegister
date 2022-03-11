using AutoMapper;
using CollaboratorRegisterApi.Profiles;
using Xunit;

namespace CollaboratorRegisterTest.Profiles
{
    public class ProfileTest
    {
        protected readonly IConfigurationProvider _mapperConfig;

        public ProfileTest()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CollaboratorProfile>();
            });
        }

        [Fact]
        public void TestConfigMapper()
        {
            _mapperConfig.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData("Teste@123")]
        public void Criptografy_Test(string password)
        {
            var profile = new CollaboratorProfile();
            var result = profile.Criptografy(password);
            Assert.Equal("1791962eadeadcd9001ce88815698370", result);
        }
    }
}
