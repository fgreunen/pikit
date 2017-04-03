using Pikit.Database;
using Pikit.Shared;
using Pikit.Shared.Configuration;
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
            configuration.TargetDatabase = new DbConnectionInfo(Kernel.Get<IConfigurationProperties>().DatabaseContext, "System.Data.SqlClient");

            var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            if (context.Database.Exists())
            {
                if (migrator.GetPendingMigrations().Any())
                {
                    migrator.Update();
                }
                else
                {
                    try
                    {
                        context.AuditRecords.Any();
                    }
                    catch (Exception ex)
                    {
                        if (ex is InvalidOperationException
                            && ex.Message.StartsWith("The model backing the"))
                        {
                            context.Database.Delete();
                            context.Database.Create();
                            migrator.Update();
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                migrator.Update();
            }
        }
    }
}