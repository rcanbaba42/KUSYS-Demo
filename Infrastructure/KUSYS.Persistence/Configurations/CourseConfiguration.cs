using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYS.Persistence.Configurations
{
    public class CourseConfiguration : BaseConfiguration<Course>
    {
        /// <summary>
        /// base sınıftan türetildi. ekstra configrutaion tanımlanmadı
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            base.Configure(builder);
        }
    }
}
