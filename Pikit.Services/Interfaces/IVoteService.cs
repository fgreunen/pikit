using Pikit.Services.Requests;
using Pikit.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Interfaces
{
    public interface IVoteService
    {
        BaseResponse Vote(VoteRequest request);
    }
}