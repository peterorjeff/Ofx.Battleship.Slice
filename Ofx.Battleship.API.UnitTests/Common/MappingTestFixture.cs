using AutoMapper;

namespace Ofx.Battleship.API.UnitTests.Common
{
    public class MappingTestFixture
    {
        public MappingTestFixture()
        {
            ConfigurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<Features.Games.Details.MappingProfile>();
                configuration.AddProfile<Features.Boards.Create.MappingProfile>();
                configuration.AddProfile<Features.Ships.Attack.MappingProfile>();
                configuration.AddProfile<Features.Players.Details.MappingProfile>();
                configuration.AddProfile<Features.Players.List.MappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }
        public IMapper Mapper { get; }
    }
}
