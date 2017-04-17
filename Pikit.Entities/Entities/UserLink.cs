using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Entities.Entities
{
    public class UserLink
        : Entity
    {
        public int Id { get; set; }

        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime Created { get; set; }

        public virtual User From { get; set; }
        public virtual User To { get; set; }
    }
}