﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelContainer : DbContext
    {
        public ModelContainer()
            : base("name=ModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<User> User集 { get; set; }
        public DbSet<Cost> Cost集 { get; set; }
        public DbSet<Plan> Plan集 { get; set; }
        public DbSet<Contact> Contact集 { get; set; }
        public DbSet<RichType> RichType集 { get; set; }
        public DbSet<CostType> CostType集 { get; set; }
        public DbSet<ContactRecord> ContactRecord集 { get; set; }
        public DbSet<ContactWay> ContactWay集 { get; set; }
        public DbSet<ContactType> ContactType集 { get; set; }
        public DbSet<Blog> Blog集 { get; set; }
        public DbSet<BlogType> BlogType集 { get; set; }
        public DbSet<TimeCost> TimeCost集 { get; set; }
        public DbSet<TimeCostType> TimeCostType集 { get; set; }
    }
}
