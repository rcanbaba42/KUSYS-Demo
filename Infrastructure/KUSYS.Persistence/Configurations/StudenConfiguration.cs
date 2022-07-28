using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYS.Persistence.Configurations
{
    public class StudenConfiguration : BaseConfiguration<Student>
    {
        /// <summary>
        /// base sınıftan türetildi. ekstra configrutaion tanımlanmadı
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder);
        }
    }
}
