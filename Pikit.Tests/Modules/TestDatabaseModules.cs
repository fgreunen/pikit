using Ninject.Modules;
using Pikit.Database;
using Pikit.Database.Procedures;
using Pikit.Shared.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.Modules
{
    public class TestDatabaseModules
        : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork<PikitContext>>();
            Bind<IProcedures>().To<DatabaseProcedures>().InSingletonScope();
        }
    }
}
