using System;

namespace ServerING.Exceptions {
    public class ServerException : Exception
    {
        public ServerException() : base() { }
        public ServerException(string message) : base(message) { }
    }
    
    public class ServerAlreadyExistsException : ServerException
    {
        public ServerAlreadyExistsException() : base() { }
        public ServerAlreadyExistsException(string message) : base(message) { }
    }

    public class ServerNotExistsException : ServerException
    {
        public ServerNotExistsException() : base() { }
        public ServerNotExistsException(string message) : base(message) { }
    }

    public class ServerConflictException: ServerException {
        public ServerConflictException(int id) : 
            base(string.Format("Server conflict with id = {0}", id)) {}
    }
}
