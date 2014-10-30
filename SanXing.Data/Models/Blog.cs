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

    [Description("博客")]
    public partial class Blog : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Sentiment { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTime { get; set; }
        public int BlogTypeID { get; set; }
        public int UserID { get; set; }
        public string Url { get; set; }

        public bool Deleted { get; set; }

        public virtual BlogType BlogType { get; set; }
        public virtual User User { get; set; }
    
    }
}
