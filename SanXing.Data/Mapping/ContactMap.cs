
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.Name).IsRequired().HasMaxLength(50);

            this.Property(x => x.Sex).IsRequired();

            this.Property(x => x.Avtar).HasMaxLength(500);

            this.Property(x => x.Advantages).HasMaxLength(500);

            this.Property(x => x.Weakness).HasMaxLength(500);

            this.Property(x => x.Mettle).HasMaxLength(500);

            this.Property(x => x.Description).HasMaxLength(500);

            this.HasRequired(a => a.User).WithMany(x => x.Contact)

                .HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.RichType).WithMany(x => x.Contact)

                .HasForeignKey(x => x.RichTypeID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.ContactType).WithMany(x => x.Contact)

                .HasForeignKey(x => x.ContactTypeID).WillCascadeOnDelete(false);
        }
    }
}