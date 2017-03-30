namespace Pikit.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Pikit.Database.PikitContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Pikit.Database.PikitContext context)
        {
            LoadStoredProcedures(context);
        }

        private void LoadStoredProcedures(
            PikitContext context)
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Pikit.Database\\StoredProcedures");
            if (!Directory.Exists(dir))
            {
                throw new Exception("Pikit.Database.Migrations.Configuration.LoadStoredProcedures() - Stored procedures folder not found!");
            }

            var exceptionFile = dir + "\\Exceptions.txt";
            if (File.Exists(exceptionFile))
            {
                File.Delete(exceptionFile);
            }

            foreach (var filePath in Directory.EnumerateFiles(dir, "*.sql"))
            {
                try
                {
                    var command = File.ReadAllText(filePath);
                    if (command.StartsWith("-- EF Seed"))
                    {
                        Debug.WriteLine(string.Format("Executing SQL file: {0}", filePath));
                        var output = context.Database.ExecuteSqlCommand(command);
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(exceptionFile, ex.Message);
                }
            }
        }
    }
}