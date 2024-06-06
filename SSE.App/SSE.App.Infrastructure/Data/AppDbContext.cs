using Microsoft.EntityFrameworkCore;
using SSE.App.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SSE.App.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppEntity> Apps { get; set; }
         public DbSet<ResourceEntity> Resources { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        private void CreateIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppEntity>()
                          .HasKey(e => e.Id);
            modelBuilder.Entity<AppEntity>()
                .HasIndex(e => e.Id);
            modelBuilder.Entity<AppEntity>()
                .HasIndex(e => e.UserId);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateIndexes(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
