using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infrastruttura.Data;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Context
{
    public partial class ItalTechContext : DbContext
    {
        public ItalTechContext()
        {
        }

        public ItalTechContext(DbContextOptions<ItalTechContext> options)
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
