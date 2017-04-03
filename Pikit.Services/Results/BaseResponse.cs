﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Responses
{
    public class BaseResponse
    {
        public DateTime ResponseTimestamp { get; private set; }

        public BaseResponse()
        {
            ResponseTimestamp = DateTime.Now;
        }
    }
}