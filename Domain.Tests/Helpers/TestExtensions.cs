#region

using System;
using FluentAssertions.Specialized;

#endregion

namespace Domain.Tests.Helpers
{
    public static class TestExtensions
    {
        public static void WithMessageLike<T>(
            this ExceptionAssertions<T> assertion,
            string assertionMessage) where T : Exception
        {
            assertion.WithMessage($"*{assertionMessage}*");
        }
    }
}