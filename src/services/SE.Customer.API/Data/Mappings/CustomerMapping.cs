using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SE.Core.DomainObjects;
using SE.Customers.API.Models;

namespace SE.Customers.API.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.SocialSecurityNumber, tf =>
            {
                tf.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(SocialSecurityNumber.MaxLength)
                    .HasColumnName("SocialSecurityNumber")
                    .HasColumnType($"varchar({SocialSecurityNumber.MaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.EmailAddress)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.MaxLength})");
            });

            builder
                .HasOne(c => c.Address)
                .WithOne(c => c.Customer);

            builder.ToTable("Customers");
        }
    }
}