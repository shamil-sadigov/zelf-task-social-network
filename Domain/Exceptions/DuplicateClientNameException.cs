using System;
using Domain.ValueObjects;

namespace Domain.Exceptions
{
    public class DuplicateClientNameException:ApplicationException
    {
        public ClientName ClientName { get; }

        public DuplicateClientNameException(ClientName clientName)
            :base($"Client with name '{clientName} already exists'")
        {
            ClientName = clientName;
        }
    }
}