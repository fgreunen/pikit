using Moq;
using NUnit.Framework;
using Pikit.Shared;
using Pikit.Tests.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests
{
    [TestFixture]
    public class TestBase
    {
        public virtual bool UseDatabase { get { return false; } }
        public TestModules TestModules { get; set; }

        [SetUp]
        public void BaseSetUp()
        {
            Kernel.Initialize();

            Kernel.Load(new TestModules());

            if (UseDatabase)
            {
                Kernel.Load(new TestDatabaseModules());

                new DatabaseInitializer().Go();
            }
            else
            {
                Kernel.Load(new TestInMemoryModules());
            }
        }
    }
}