using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EFCore.Mapping
{
    public class CustomerDiscountMapping : IEntityTypeConfiguration<DiscountManagement.Domain.CustomerDiscount.CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<DiscountManagement.Domain.CustomerDiscount.CustomerDiscount> builder)
        {
            builder.ToTable("CustomerDiscounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Reason).HasMaxLength(500);

        }
    }
}
