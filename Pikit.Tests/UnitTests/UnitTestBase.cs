using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.UnitTests
{
    [TestFixture]
    public abstract class UnitTestBase
        : TestBase
    {
        public override bool UseDatabase { get { return false; } }
    }
}