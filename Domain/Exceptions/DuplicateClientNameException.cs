﻿#region

using System;
using Domain.ValueObjects;

#endregion

namespace Domain.Exceptions
{
    public class DuplicateClientNameException : ApplicationException
    {
        public DuplicateClientNameException(ClientName clientName)
            : base($"Client with name '{clientName} already exists'")
        {
            ClientName = clientName;
        }

        public ClientName ClientName { get; }
    }
}