using AutoMapper;

namespace Ofx.Battleship.Application.UnitTests.Common
{
    public class MappingTestFixture
    {
        public MappingTestFixture()
        {
            ConfigurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<API.Features.Boards.Create.MappingProfile>();
                configuration.AddProfile<API.Features.Ships.Attack.MappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
