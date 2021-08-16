#region

using System;
using System.Collections.Generic;
using Domain.BuildingBlocks.BuildingBlocks;

#endregion

namespace Domain.ValueObjects
{
    public class ClientId : ValueObject
    {
        public ClientId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}