using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Entities.Entities
{
    public class Vote
        : Entity
    {
        public int Id { get; set; }
        public Guid UniqueIdentifier { get; set; }
        public DateTime Created { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool VoteBit { get; set; }
    }
}