using Pikit.Database;
using Pikit.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests
{
    public class DatabaseInitializer
        : DisposableBase
    {
        public void Go()
        {
            using (var context = new PikitContext())
            {
                if (!context.Database.Connection.Database.EndsWith("Test"))
                {
                    throw new Exception("Database name does not end with 'Test'.");
                }

                CreateOrUpgrade(context);

                context.Database.ExecuteSqlCommand("EXEC SP_CleanDatabase");
            }
        }

        private void CreateOrUpgrade(
            PikitContext context)
        {
            var configuration = new Pikit.Database.Migrations.Configuration();
            configuration.TargetDatabase = new DbConnectionInfo(PikitContext.ConnectionName);

            var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            if (context.Database.Exists())
            {
                if (migrator.GetPendingMigrations().Any())
                {
                    migrator.Update();
                }
            }
            else
            {
                migrator.Update();
            }
        }
    }
}