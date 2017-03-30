using Pikit.Database.Procedures;
using Pikit.Shared;
using Pikit.Shared.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Interfaces
{
    public abstract class ServiceBase
        : DisposableBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IProcedures _procedures;

        public ServiceBase()
        {
            _unitOfWork = Kernel.Get<IUnitOfWork>();
            _procedures = Kernel.Get<IProcedures>();
        }
    }
}
