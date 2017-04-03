using Pikit.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Responses
{
    public class PostGetResponse
        : BaseResponse
    {
        public List<Post> Posts { get; set; }

        public PostGetResponse()
            : base()
        {
        }
    }
}