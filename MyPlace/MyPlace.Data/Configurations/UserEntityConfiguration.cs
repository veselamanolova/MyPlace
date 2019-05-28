
namespace MyPlace.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyPlace.DataModels;

    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder
                .HasKey(ue => new { ue.EntityId, ue.UserId });

            builder
                .HasOne(ue => ue.Entity)
                .WithMany(e => e.UserEntities)
                .HasForeignKey(ue => ue.EntityId);

            builder
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserEntities)
                .HasForeignKey(ue => ue.UserId);
        }
    }
}


