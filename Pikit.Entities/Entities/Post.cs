using Pikit.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Entities.Entities
{
    public class Post
        : IAuditable
    {
        public int Id { get; set; }
        public Guid UniqueIdentifier { get; set; }

        public string ResourceUrl1 { get; set; }
        public string ResourceUrl2 { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}