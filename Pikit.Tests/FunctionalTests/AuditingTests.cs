using Newtonsoft.Json;
using NUnit.Framework;
using Pikit.Entities.Entities;
using Pikit.Shared;
using Pikit.Shared.Configuration;
using Pikit.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.FunctionalTests
{
    [TestFixture]
    public class AuditingTests
        : FunctionalTestBase
    {
        public IRepository<TestEntity> TestEntities
        {
            get
            {
                return UnitOfWork.GetRepository<TestEntity>();
            }
        }

        public IRepository<AuditRecord> AuditRecordEntities
        {
            get
            {
                return UnitOfWork.GetRepository<AuditRecord>();
            }
        }

        [Test]
        public void CreateAndUpdateAndDelete()
        {
            Kernel.Get<IConfigurationProperties>().DoAuditing = true;
            Assert.That(Kernel.Get<IConfigurationProperties>().DoAuditing);

            var testEntity = new TestEntity();

            // Create TestEntity
            RefreshDbContext();
            TestEntities.Create(testEntity);
            UnitOfWork.Save();
            Assert.That(testEntity.Id > 0);
            Assert.That(TestEntities.GetAll().Count() == 1);

            // Check audit log
            RefreshDbContext();
            var auditRecords = AuditRecordEntities.GetAll();
            var auditRecord = auditRecords.Single();
            Assert.That(auditRecord.AuditDate.Date == DateTime.Now.Date);
            Assert.That(auditRecord.AuditType == "Create");
            Assert.That(!string.IsNullOrEmpty(auditRecord.AuditUser));
            Assert.That(auditRecord.Id > 0);
            Assert.That(auditRecord.RecordId == testEntity.Id);
            Assert.That(auditRecord.RecordType == testEntity.GetType().AssemblyQualifiedName);
            Assert.That(auditRecord.RecordData == JsonConvert.SerializeObject(testEntity));

            // Update TestEntity
            RefreshDbContext();
            testEntity.Field1 = "Hello World";
            testEntity.Field2 = "Hello World";
            testEntity.Field3 = "Hello World";
            TestEntities.Update(testEntity);
            UnitOfWork.Save();
            Assert.That(testEntity.Id > 0);
            Assert.That(testEntity.Field1 == testEntity.Field1);
            Assert.That(testEntity.Field2 == testEntity.Field2);
            Assert.That(testEntity.Field3 == testEntity.Field3);
            Assert.That(TestEntities.GetAll().Count() == 1);

            // Check audit log
            RefreshDbContext();
            auditRecords = AuditRecordEntities.GetAll();
            Assert.That(auditRecords.Count() == 2);
            auditRecord = auditRecords.ToList()[1];
            Assert.That(auditRecord.AuditDate.Date == DateTime.Now.Date);
            Assert.That(auditRecord.AuditType == "Update");
            Assert.That(!string.IsNullOrEmpty(auditRecord.AuditUser));
            Assert.That(auditRecord.Id > 0);
            Assert.That(auditRecord.RecordId == testEntity.Id);
            Assert.That(auditRecord.RecordType == testEntity.GetType().AssemblyQualifiedName);
            Assert.That(auditRecord.RecordData == JsonConvert.SerializeObject(testEntity));
            Assert.That(auditRecord.RecordData.Contains("Hello World"));

            // Delete TestEntity
            RefreshDbContext();
            TestEntities.Delete(testEntity);
            UnitOfWork.Save();
            Assert.That(TestEntities.GetAll().Count() == 0);

            // Check audit log
            RefreshDbContext();
            auditRecords = AuditRecordEntities.GetAll();
            Assert.That(auditRecords.Count() == 3);
            auditRecord = auditRecords.ToList()[2];
            Assert.That(auditRecord.AuditDate.Date == DateTime.Now.Date);
            Assert.That(auditRecord.AuditType == "Delete");
            Assert.That(!string.IsNullOrEmpty(auditRecord.AuditUser));
            Assert.That(auditRecord.Id > 0);
            Assert.That(auditRecord.RecordId == testEntity.Id);
            Assert.That(auditRecord.RecordType == testEntity.GetType().AssemblyQualifiedName);
            Assert.That(auditRecord.RecordData == JsonConvert.SerializeObject(testEntity));
            Assert.That(auditRecord.RecordData.Contains("Hello World"));
        }
    }
}
