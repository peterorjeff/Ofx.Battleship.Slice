namespace Ofx.Battleship.API.Entities
{
    public class Shot
    {
        public int ShotId { get; set; }
        public int AttackX { get; set; }
        public int AttackY { get; set; }
        public bool Hit { get; set; }

        public Board Board { get; set; }

        public Player Player { get; set; }

        public ShipPart ShipPartHit { get; set; }
    }
}
