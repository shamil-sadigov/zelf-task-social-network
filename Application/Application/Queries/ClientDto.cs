#region

using System;

#endregion

namespace Application.Queries
{
    public record ClientDto(Guid Id, string Name, ushort Popularity);
}