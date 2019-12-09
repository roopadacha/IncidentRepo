using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public class IncidentDbContext : DbContext
    {
        public IncidentDbContext(DbContextOptions<IncidentDbContext> options): base(options)
        {

        }

        public DbSet<IncidentType> IncidentTypes { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            var currentUsername = "Anonymous";
            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        ((BaseEntity)entity.Entity).CreatedDate = DateTime.UtcNow;
                        ((BaseEntity)entity.Entity).CreatedBy = currentUsername;
                        break;
                    case EntityState.Modified:
                        ((BaseEntity)entity.Entity).LastUpdatedDate = DateTime.UtcNow;
                        ((BaseEntity)entity.Entity).LastUpdatedBy = currentUsername;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }     
}
