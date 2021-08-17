using Domain;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
    public class ClientSubscriberConfiguration : IEntityTypeConfiguration<ClientSubscriber>
    {
        public void Configure(EntityTypeBuilder<ClientSubscriber> builder)
        {
            builder.ToTable("ClientSubscribers");
            
            builder.HasKey(x => new {x.ClientId, x.SubscriberId});
            
            builder.HasOne<Client>()
                .WithMany("_subscribers")
                .HasForeignKey(x => x.ClientId);
            
            builder.HasOne<Client>()
                .WithMany()
                .HasForeignKey(x => x.SubscriberId);
        }
    }
}