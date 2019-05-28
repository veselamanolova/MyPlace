
namespace MyPlace.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyPlace.DataModels;

    class EntityCategoryConfiguration : IEntityTypeConfiguration<EntityCategory>
    {
        public void Configure(EntityTypeBuilder<EntityCategory> builder)
        {
            builder
                .HasKey(ec => new { ec.EntityId, ec.CategoryId });

            builder
                .HasOne(ec => ec.Entity)
                .WithMany(e => e.EntityCategories)
                .HasForeignKey(ec => ec.EntityId);

            builder
                .HasOne(ec => ec.Category)
                .WithMany(c => c.EntityCategories)
                .HasForeignKey(ec => ec.CategoryId);
        }
    }
}


