using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Entities
{
    public class Shot
    {
        public int ShotId { get; set; }
        public int AttackX { get; set; }
        public int AttackY { get; set; }
        public bool Hit { get; set; }

        public Board Board { get; set; }
    }
}
