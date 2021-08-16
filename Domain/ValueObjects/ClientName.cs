#region

using System;
using System.Collections.Generic;
using System.Linq;
using Domain.BuildingBlocks.BuildingBlocks;
using Domain.Extensions;

#endregion

namespace Domain.ValueObjects
{
    public class ClientName : ValueObject
    {
        public ClientName(string name)
        {
            NameMustHaveValue(name);

            var trimmedName = name.Trim();

            NameMustHaveValidLength(trimmedName);

            NameMustContainValidCharacters(trimmedName);

            Value = trimmedName;
        }

        public string Value { get; }

        private static void NameMustHaveValue(string name)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));
        }

        private static void NameMustContainValidCharacters(string name)
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

        private static void NameMustHaveValidLength(string name)
        {
            if (name.Length is < 2 or > 64)
                throw new ArgumentException("Name length should be between 2 and 64", nameof(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}