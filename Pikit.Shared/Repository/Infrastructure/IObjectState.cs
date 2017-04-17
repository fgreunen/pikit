
using System.ComponentModel.DataAnnotations.Schema;

namespace Pikit.Shared.Repository.Infrastructure
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}