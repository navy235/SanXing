﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SanXing.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Mt.Core;
    using System.ComponentModel;

    [Description("费用")]
    public partial class Cost : BaseEntity
    {
   
        public int Money { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTime { get; set; }
        public int CostTypeID { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public bool Deleted { get; set; }

        public DateTime CostDate { get; set; }


        public virtual CostType CostType { get; set; }
        public virtual User User { get; set; }

    }
}