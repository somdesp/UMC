﻿//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PFC.Hubs
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MeHelpChat : DbContext
    {
        public MeHelpChat()
            : base("name=MeHelpChat")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ChatMensDetal> ChatMensDetal { get; set; }
        public DbSet<ChatPrivMensDetal> ChatPrivMensDetal { get; set; }
        public DbSet<ChatPrivMensMaster> ChatPrivMensMaster { get; set; }
        public DbSet<ChatUsuDetal> ChatUsuDetal { get; set; }
    }
}
