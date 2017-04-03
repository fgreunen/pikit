using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Responses
{
    public class PostCreateResponse
        : BaseResponse
    {
        public Guid PostUniqueIdentifier { get; set; }

        public PostCreateResponse()
            : base()
        {
        }
    }
}