using System;

namespace Domain.Exceptions
{
    public class DomainExceptionBase:Exception
    {
        public DomainExceptionBase(string message):base(message)
        {
            
        }   
    }
}