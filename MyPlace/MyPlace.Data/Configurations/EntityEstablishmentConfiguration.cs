
namespace MyPlace.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyPlace.DataModels;

    class EntityEstablishmentConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder                
                .HasOne(e => e.Establishment)
                .WithMany(es => es.LogBooks)       
                .HasForeignKey(e=> e.EstablishmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}


