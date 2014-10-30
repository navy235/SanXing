
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class TimeCostMap : EntityTypeConfiguration<TimeCost>
    {
        public TimeCostMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.Hour).IsRequired();

            this.HasRequired(a => a.User).WithMany(x => x.TimeCost)

                .HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.TimeCostType).WithMany(x => x.TimeCost)

                .HasForeignKey(x => x.TimeCostTypeID).WillCascadeOnDelete(false);
        }
    }
}