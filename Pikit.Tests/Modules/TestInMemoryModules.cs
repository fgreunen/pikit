using Moq;
using Ninject.Modules;
using Pikit.Shared.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pikit.Tests.Modules;
using Pikit.Database.Procedures;

namespace Pikit.Tests.Modules
{
    public class TestInMemoryModules
        : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<InMemoryUnitOfWork>().InSingletonScope();
            Bind<IProcedures>().ToConstant(new Mock<IProcedures>().Object);
        }
    }
}