using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Requests
{
    public abstract class BaseRequest
    {
        public DateTime RequestTimestamp { get; private set; }

        public BaseRequest()
        {
            RequestTimestamp = DateTime.Now;
        }
    }
}
