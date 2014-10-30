
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class BlogMap : EntityTypeConfiguration<Blog>
    {
        public BlogMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.Name).IsRequired().HasMaxLength(50);

            this.Property(x => x.Content).IsRequired();

            this.Property(x => x.Sentiment).IsRequired();

            this.Property(x => x.Url).IsRequired().HasMaxLength(20);

            this.HasRequired(a => a.User).WithMany(x => x.Blog)

                .HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.BlogType).WithMany(x => x.Blog)

                .HasForeignKey(x => x.BlogTypeID).WillCascadeOnDelete(false);
        }
    }
}