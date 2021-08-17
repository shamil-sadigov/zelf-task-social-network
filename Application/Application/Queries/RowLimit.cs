#region

using System;

#endregion

namespace Application.Queries
{
    public class RowLimit
    {
        public const ushort MinValue = 1;
        public const ushort MaxValue = 100;

        public RowLimit(ushort value)
            => Value = Math.Clamp(value, MinValue, MaxValue);

        public RowLimit()
            => Value = MaxValue;

        public ushort Value { get; private set; }

        public void ChangeLimit(ushort newValue)
            => Value = Math.Clamp(newValue, MinValue, MaxValue);
    }
}