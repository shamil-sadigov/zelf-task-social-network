#region

using System;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

#endregion

namespace Domain.Tests
{
    public class ClientNameTest
    {
        [Theory]
        [InlineData("a")]
        [InlineData("a ")]
        [InlineData("ab")]
        [InlineData("ab    ")]
        public void Cannot_create_clientName_when_value_contains_less_than_3_characters(string name)
        {
            Action clientNameCreation = () =>
            {
                var clientName = new ClientName(name);
            };

            clientNameCreation
                .Should()
                .Throw<ArgumentException>()
                .WithMessageLike("Must have at least 3 characters");
        }

        [Theory]
        [InlineData("Firstname#", '#')]
        [InlineData("Firstname-", '-')]
        [InlineData("Firstname Lastname!", '!')]
        [InlineData("Firs@tname Lastname-3", '@')]
        public void Cannot_create_clientName_when_value_contains_invalid_character(string name, char invalidChar)
        {
            Action clientNameCreation = () =>
            {
                var clientName = new ClientName(name);
            };

            clientNameCreation
                .Should()
                .Throw<ArgumentException>()
                .WithMessageLike($"Must not contain '{invalidChar}' character");
        }

        [Theory]
        [InlineData("FirstName")]
        [InlineData("FirstName LastName")]
        [InlineData(" FirstName LastName  ")]
        [InlineData("Firstname Middlename Lastname")]
        public void Can_create_clientName_when_value_is_valid(string name)
        {
            var clientName = new ClientName(name);

            clientName.Value
                .Should()
                .Be(name.Trim());
        }
    }
}