#region

using System.Collections.Generic;
using Domain.BuildingBlocks;

#endregion

namespace Domain.ValueObjects
{
    public class ClientPopularity : ValueObject
    {
        public ClientPopularity(uint value)
        {
            Value = value;
        }

        public uint Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}