using NUnit.Framework;
using Pikit.Entities.Entities;
using Pikit.Services.Interfaces;
using Pikit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.UnitTests.Services
{
    [TestFixture]
    public class AuditingService
        : UnitTestBase
    {
        [Test]
        public void CreateAndGet()
        {
            var service = Kernel.Get<IAuditingService>();
            Assert.That(service.GetAuditRecords().Count() == 0);
            service.Create(new AuditRecord
            {
                AuditDate = DateTime.Now,
                AuditType = "Test",
                AuditUser = "Test",
                RecordData = "Data",
                RecordId = 1,
                RecordType = "RecordType"
            });
            Assert.That(service.GetAuditRecords().Count() == 1);
        }
    }
}
