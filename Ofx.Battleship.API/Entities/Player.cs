namespace Ofx.Battleship.API.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }

        public Game Game { get; set; }
    }
}
