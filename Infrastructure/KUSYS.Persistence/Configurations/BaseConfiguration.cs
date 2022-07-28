using KUSYS.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYS.Persistence.Configurations
{
    /// <summary>
    /// Entityler için gerekli configurasyonlar(Key,Required,Relationship) yapılması için
    /// base configuration objecti
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).ValueGeneratedOnAdd();
        }
    }
}
