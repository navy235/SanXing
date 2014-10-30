
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class BlogTypeMap : EntityTypeConfiguration<BlogType>
    {
        public BlogTypeMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.CateName).IsRequired().HasMaxLength(50);

            this.Property(x => x.Code).IsRequired().HasMaxLength(50);

            this.HasOptional(a => a.PCate).WithMany(x => x.ChildCates)

                .HasForeignKey(x => x.PID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.User).WithMany(x => x.BlogType)

                .HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);
        }
    }
}