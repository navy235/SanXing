
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class ContactRecordMap : EntityTypeConfiguration<ContactRecord>
    {
        public ContactRecordMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.Problem).IsRequired().HasMaxLength(500);

            this.Property(x => x.Description).IsRequired().HasMaxLength(500);

            this.Property(x => x.Sentiment).IsRequired().HasMaxLength(500);

            this.HasRequired(a => a.User).WithMany(x => x.ContactRecord)

                .HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.Contact).WithMany(x => x.ContactRecord)

                .HasForeignKey(x => x.ContactID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.ContactWay).WithMany(x => x.ContactRecord)

                .HasForeignKey(x => x.ContactWayID).WillCascadeOnDelete(false);
        }
    }
}