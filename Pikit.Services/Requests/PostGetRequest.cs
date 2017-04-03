using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Requests
{
    public class PostGetRequest
        : BaseRequest
    {
        [Required]
        public Guid UserUniqueIdentifier { get; set; }

        public DateTime? StartFilter { get; set; }
        public DateTime? EndFilter { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public PostGetRequest()
            : base()
        {
        }
    }
}