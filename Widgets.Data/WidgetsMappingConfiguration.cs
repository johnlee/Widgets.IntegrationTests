using Highway.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Widgets.Data
{
    public class WidgetsMappingConfiguration : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_U_Email") { IsUnique = true }));

            modelBuilder.Entity<Widget>()
                .Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Widget>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);
        }
    }
}
