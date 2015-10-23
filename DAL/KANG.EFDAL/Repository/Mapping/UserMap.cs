using System.Data.Entity.ModelConfiguration;
using KANG.MODEL;

namespace KANG.EFDAL.Repository.Mapping
{
    public class UserMap : EntityTypeConfiguration<User_MODEL>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(20);

            this.Property(t => t.UserID)
                .HasMaxLength(20);

            this.Property(t => t.Password)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.UserType).HasColumnName("UserType");
        }
    }
}
