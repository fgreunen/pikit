using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Entities.Entities
{
    public class User
        : Entity
    {
        public int Id { get; set; }
        public Guid UniqueIdentifier { get; set; }
    }
}