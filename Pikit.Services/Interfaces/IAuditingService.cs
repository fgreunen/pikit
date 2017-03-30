using Pikit.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Interfaces
{
    public interface IAuditingService
    {
        IQueryable<AuditRecord> GetAuditRecords();
        void Create(AuditRecord auditRecord);
    }
}
