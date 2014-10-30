
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class CostMap : EntityTypeConfiguration<Cost>
    {
        public CostMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.Money).IsRequired();

            this.Property(x => x.Description).IsRequired();

            this.HasRequired(a => a.User).WithMany(x => x.Cost)

                .HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.CostType).WithMany(x => x.Cost)

                .HasForeignKey(x => x.CostTypeID).WillCascadeOnDelete(false);
        }
    }
}