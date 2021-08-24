using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infrastruttura.Data;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Context
{
    public partial class ItalTechDbContext : DbContext
    {
        public ItalTechDbContext()
        {
        }

        public ItalTechDbContext(DbContextOptions<ItalTechDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
