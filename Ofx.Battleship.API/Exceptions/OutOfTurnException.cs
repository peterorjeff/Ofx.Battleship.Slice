using System;

namespace Ofx.Battleship.API.Exceptions
{
    public class OutOfTurnException : Exception
    {
        public OutOfTurnException(string playerName) : base($"{playerName}, it is not your turn!")
        { 
        }
    }
}
