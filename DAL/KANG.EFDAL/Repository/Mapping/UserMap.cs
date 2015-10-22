using System.Data.Entity.ModelConfiguration;

namespace KANG.EFDAL.Repository.Mapping
{
    public class UserMap : EntityTypeConfiguration<MODEL.User_MODEL>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Age).HasColumnName("Age");
        }
    }
}
