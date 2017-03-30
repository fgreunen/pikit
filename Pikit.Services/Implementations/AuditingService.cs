using Pikit.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Implementations
{
    public class AuditingService
        : ServiceBase, IAuditingService
    {
        public IQueryable<Entities.Entities.AuditRecord> GetAuditRecords()
        {
            return _unitOfWork.GetRepository<Entities.Entities.AuditRecord>().GetAll();
        }

        public void Create(Entities.Entities.AuditRecord auditRecord)
        {
            _unitOfWork.GetRepository<Entities.Entities.AuditRecord>().Create(auditRecord);
            _unitOfWork.Save();
        }
    }
}