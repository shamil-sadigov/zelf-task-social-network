#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Domain.ValueObjects
{
    public class ClientName : ValueObject
    {
        public ClientName(string name)
        {
            EnsureHasValue(name);

            var trimmedName = name.Trim();

            EnsureNameHasValidLength(trimmedName);

            EnsureNameContainsValidSymbols(trimmedName);

            Value = trimmedName;
        }

        public string Value { get; }

        private static void EnsureHasValue(string name)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));
        }

        private static void EnsureNameContainsValidSymbols(string name)
        {
            var nameContainsLetter = false;

            foreach (var @char in name.AsEnumerable())
            {
                if (@char.IsLetter())
                {
                    nameContainsLetter = true;
                    continue;
                }

                if (@char.IsWhiteSpace())
                    continue;

                throw new ArgumentException($"Must not contain '{@char}' character", nameof(name));
            }

            if (!nameContainsLetter)
                throw new ArgumentException("Must contain at least one letter", nameof(name));
        }

        private static void EnsureNameHasValidLength(string name)
        {
            if (name.Length < 3)
                throw new ArgumentException("Must have at least 3 characters", nameof(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}