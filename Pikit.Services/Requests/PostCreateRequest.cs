using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Requests
{
    public class PostCreateRequest
        : BaseRequest
    {
        [Required]
        public Guid UserUniqueIdentifier { get; set; }
        [Required]
        public byte[] Resource1 { get; set; }
        [Required]
        public byte[] Resource2 { get; set; }
        [Required]
        public string Description { get; set; }

        public PostCreateRequest()
            : base()
        {
        }
    }
}