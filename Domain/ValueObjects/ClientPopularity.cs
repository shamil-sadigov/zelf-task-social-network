using System.Collections.Generic;
using Domain.BuildingBlocks.BuildingBlocks;

namespace Domain.ValueObjects
{
    public class ClientPopularity:ValueObject
    {
        public uint Value { get; }

        public ClientPopularity(uint value)
        {
            Value = value;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}