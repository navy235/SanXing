
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.UserName).IsRequired().HasMaxLength(50);

            this.Property(x => x.Email).IsRequired().HasMaxLength(50);

            this.Property(x => x.Password).IsRequired().HasMaxLength(50);

            this.Property(x => x.Phone).HasMaxLength(20);

            this.Property(x => x.Mobile).HasMaxLength(20);
        }
    }
}