
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using SanXing.Data.Models;

namespace SanXing.Data.Mapping
{
    public partial class PlanMap : EntityTypeConfiguration<Plan>
    {
        public PlanMap()
        {
            this.HasKey(a => a.ID);

            this.Property(x => x.Name).IsRequired().HasMaxLength(50);

         
        }
    }
}