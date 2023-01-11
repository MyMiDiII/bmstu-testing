using System;

namespace ServerING.Exceptions
{
    public class CountryException : Exception
    {
        public CountryException() : base() { }
        public CountryException(string message) : base(message) { }
    }
    
    public class CountryAlreadyExistsException : CountryException
    {
        public CountryAlreadyExistsException() : base() { }
        public CountryAlreadyExistsException(string message) : base(message) { }
    }

    public class CountryNotExistsException : CountryException
    {
        public CountryNotExistsException() : base() { }
        public CountryNotExistsException(string message) : base(message) { }
    }
}