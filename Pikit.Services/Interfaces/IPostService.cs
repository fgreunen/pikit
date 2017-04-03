using Pikit.Services.Requests;
using Pikit.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Interfaces
{
    public interface IPostService
    {
        PostCreateResponse Create(PostCreateRequest request);
        PostGetResponse Get(PostGetRequest request);
        PostGetResponse GetFriends(PostGetRequest request);
    }
}