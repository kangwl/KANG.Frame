using System.Data.Entity.ModelConfiguration;
using KANG.MODEL;

namespace KANG.EFDAL.Repository.Mapping
{
    public class CourseMap : EntityTypeConfiguration<Course_MODEL>
    {
        public CourseMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Describe)
                .HasMaxLength(500);

            this.Property(t => t.Author)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Course");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Describe).HasColumnName("Describe");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.AddUser).HasColumnName("AddUser");
            this.Property(t => t.AddDateTime).HasColumnName("AddDateTime");
            this.Property(t => t.UpdateDateTime).HasColumnName("UpdateDateTime");
        }
    }
}
