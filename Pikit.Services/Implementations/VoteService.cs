using Pikit.Entities.Entities;
using Pikit.Imaging.FileSystem;
using Pikit.Services.Interfaces;
using Pikit.Services.Requests;
using Pikit.Services.Responses;
using Pikit.Shared;
using Pikit.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Implementations
{
    public class VoteService
        : ServiceBase,
        IVoteService
    {
        public BaseResponse Vote(
            VoteRequest request)
        {
            request.Validate();
            var post = _unitOfWork.Repository<Post>().Queryable().Single(x => x.UniqueIdentifier.ToString() == request.PostUniqueIdentifier.ToString());
            var user = _unitOfWork.Repository<User>().Queryable().Single(x => x.UniqueIdentifier.ToString() == request.UserUniqueIdentifier.ToString());

            bool voteBit = post.ResourceUrl1 == request.ResourceUrl;

            var vote = _unitOfWork.Repository<Vote>().Queryable().FirstOrDefault(x => x.UserId == user.Id && x.PostId == post.Id);
            if (vote != null && vote.VoteBit != voteBit)
            {
                vote.VoteBit = voteBit;
                vote.Created = DateTime.Now;
                _unitOfWork.Repository<Vote>().Update(vote);
                _unitOfWork.SaveChanges();
                return new BaseResponse();
            }
            else
            {
                vote = new Vote
                {
                    Created = DateTime.Now,
                    PostId = post.Id,
                    UserId = user.Id,
                    UniqueIdentifier = Guid.NewGuid(),
                    VoteBit = voteBit
                };
                _unitOfWork.Repository<Vote>().Insert(vote);
                _unitOfWork.SaveChanges();

                return new BaseResponse();
            }
        }
    }
}