using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Database.Initializers
{
    public interface IInitializer
    {
        void Initialize(PikitContext context);
    }
}