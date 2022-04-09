using ArticleManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleManagement.Infrastructure.EFCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCagetories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Picture).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Slug).HasMaxLength(500);
            builder.Property(x => x.KeyWords).HasMaxLength(800);
            builder.Property(x => x.MetaDescription).HasMaxLength(1000);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(500);

            builder.HasMany(x=>x.Articles).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId);


        }
    }
}
