using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Requests
{
    public class VoteRequest
        : BaseRequest
    {
        [Required]
        public Guid UserUniqueIdentifier { get; set; }
        [Required]
        public Guid PostUniqueIdentifier { get; set; }
        [Required]
        public string ResourceUrl { get; set; }

        public VoteRequest()
            : base()
        {
        }
    }
}