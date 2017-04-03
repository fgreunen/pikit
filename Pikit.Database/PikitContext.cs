using Pikit.Entities.Entities;
using Pikit.Shared;
using Pikit.Shared.Configuration;
using Pikit.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;

namespace Pikit.Database
{
    public class PikitContext
        : DbContext, IDbContext
    {

        private readonly bool _doAuditing;

        public PikitContext()
            : base(Kernel.Get<IConfigurationProperties>().DatabaseContext)
        {
            // enable-migrations
            // update-database -TargetMigration:0 | update-database -force | update-database -force

            _doAuditing = Kernel.Get<IConfigurationProperties>().DoAuditing;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            if (_doAuditing)
            {
                using (var scope = new TransactionScope())
                {
                    var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
                    var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToList();

                    foreach (var entry in modifiedEntries)
                    {
                        ApplyAuditLog(entry);
                    }

                    int changes = base.SaveChanges();
                    foreach (var entry in addedEntries)
                    {
                        ApplyAuditLog(entry, EntityState.Added);
                    }

                    base.SaveChanges();
                    scope.Complete();

                    return changes;
                }
            }
            else
            {
                return base.SaveChanges();
            }
        }

        private void ApplyAuditLog(DbEntityEntry entry, EntityState? overrideState = null)
        {
            string auditType;
            overrideState = overrideState ?? entry.State;
            switch (overrideState.GetValueOrDefault())
            {
                case EntityState.Added:
                    auditType = "Create";
                    break;
                case EntityState.Deleted:
                    auditType = "Delete";
                    break;
                case EntityState.Modified:
                    auditType = "Update";
                    break;
                default:
                    return;
            }

            IAuditable auditable = entry.Entity as IAuditable;
            if (auditable != null)
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                var user = Thread.CurrentPrincipal;
                AuditRecord record = new AuditRecord
                {
                    AuditType = auditType,
                    AuditUser = user != null ? ((user.Identity.Name.Trim() == "") ? "Unknown" : user.Identity.Name) : "Unknown",
                    AuditDate = DateTime.Now,
                    RecordId = auditable.Id,
                    RecordType = auditable.GetType().AssemblyQualifiedName,
                    RecordData = Newtonsoft.Json.JsonConvert.SerializeObject(auditable, Newtonsoft.Json.Formatting.None, settings),
                };
                AuditRecords.Add(record);
            }
        }

        public virtual DbSet<AuditRecord> AuditRecords { get; set; }
        public virtual DbSet<TestEntity> TestEntities { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLink> UserLinks { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
    }
}