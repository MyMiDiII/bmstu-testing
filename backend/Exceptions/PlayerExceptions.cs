using System;

namespace ServerING.Exceptions
{
    public class PlayerException : Exception
    {
        public PlayerException() : base() { }
        public PlayerException(string message) : base(message) { }
    }
    
    public class PlayerAlreadyExistsException : PlayerException
    {
        public PlayerAlreadyExistsException() : base() { }
        public PlayerAlreadyExistsException(string message) : base(message) { }
    }

    public class PlayerNotExistsException : PlayerException
    {
        public PlayerNotExistsException() : base() { }
        public PlayerNotExistsException(string message) : base(message) { }
    }
}