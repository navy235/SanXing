//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SanXing.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeCostType
    {
        public TimeCostType()
        {
            this.TimeCost = new HashSet<TimeCost>();
        }
    
        public int ID { get; set; }
        public string CateName { get; set; }
        public string PID { get; set; }
        public int Code { get; set; }
        public string Level { get; set; }
    
        public virtual ICollection<TimeCost> TimeCost { get; set; }
    }
}
