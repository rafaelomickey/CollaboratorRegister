using AutoFixture;
using AutoMapper;
using CollaboratorRegisterApi.Profiles;

namespace CollaboratorRegisterTest.Services
{
    public class BaseServiceTest
    {
        public readonly Fixture _fixture;
        public readonly IMapper _mapper;

        public BaseServiceTest()
        {
            _fixture = new Fixture();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CollaboratorProfile>();
                cfg.ConstructServicesUsing(x => _mapper);
            });

            _mapper = config.CreateMapper();
        }
    }
}
