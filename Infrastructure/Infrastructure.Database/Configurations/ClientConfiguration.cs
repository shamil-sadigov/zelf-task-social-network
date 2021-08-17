#region

using Domain;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Infrastructure.Database.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, guid => new ClientId(guid));

            builder.OwnsOne<ClientName>("_name", ops =>
                ops.Property(x => x.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(64));

            builder.OwnsOne<ClientPopularity>("_popularity", ops =>
                ops.Property(x => x.Value)
                    .HasColumnName("Popularity"));
        }
    }
}