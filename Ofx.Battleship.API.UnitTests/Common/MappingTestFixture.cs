using AutoMapper;

namespace Ofx.Battleship.API.UnitTests.Common
{
    public class MappingTestFixture
    {
        public MappingTestFixture()
        {
            ConfigurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<Features.Boards.Create.MappingProfile>();
                configuration.AddProfile<Features.Ships.Attack.MappingProfile>();
                configuration.AddProfile<Features.Players.Details.MappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }
        public IMapper Mapper { get; }
    }
}
