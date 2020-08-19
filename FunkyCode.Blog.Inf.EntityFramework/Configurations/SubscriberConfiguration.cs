using FunkyCode.Blog.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FunkyCode.Blog.Inf.EntityFramework.Configurations
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.HasKey(e => e.Email);
            builder.Property(e => e.Email)
                .HasMaxLength(100);
           
            builder.Property(e => e.Status)
                .HasConversion(new EnumToStringConverter<SubscriptionStatusTypeEnum>());

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
