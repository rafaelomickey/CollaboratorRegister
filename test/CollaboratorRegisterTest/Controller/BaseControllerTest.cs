using AutoFixture;
using AutoMapper;
using CollaboratorRegisterApi.Profiles;

namespace CollaboratorRegisterTest.Controller
{
    public class BaseControllerTest
    {
        public readonly Fixture _fixture;
        public readonly IMapper _mapper;

        public BaseControllerTest()
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
