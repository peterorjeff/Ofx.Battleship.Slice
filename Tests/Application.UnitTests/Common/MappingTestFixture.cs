using AutoMapper;
using Ofx.Battleship.Application.Common.Mappings;

namespace Ofx.Battleship.Application.UnitTests.Common
{
    public class MappingTestFixture
    {
        public MappingTestFixture()
        {
            ConfigurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
                configuration.AddProfile<API.Features.Boards.Create.MappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
