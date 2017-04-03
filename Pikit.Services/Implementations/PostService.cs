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
    public class PostService
        : ServiceBase,
        IPostService
    {
        public PostCreateResponse Create(
            PostCreateRequest request)
        {
            request.Validate();

            var user = _unitOfWork.GetRepository<User>().GetAll().Single(x => x.UniqueIdentifier.ToString() == request.UserUniqueIdentifier.ToString());

            var resourceUrl1 = Kernel.Get<IImageUploadService>().UploadImage(new ImageUploadRequest
            {
                ImageData = new byte[] { }
            }).ResourceUrl;

            var resourceUrl2 = Kernel.Get<IImageUploadService>().UploadImage(new ImageUploadRequest
            {
                ImageData = new byte[] { }
            }).ResourceUrl;

            var post = new Post
            {
                ResourceUrl1 = resourceUrl1,
                ResourceUrl2 = resourceUrl2,
                UniqueIdentifier = Guid.NewGuid(),
                Description = request.Description,
                UserId = user.Id,
                Created = request.RequestTimestamp
            };

            _unitOfWork.GetRepository<Post>().Create(post);
            _unitOfWork.Save();

            var response = new PostCreateResponse
            {
                PostUniqueIdentifier = post.UniqueIdentifier
            };

            return response;
        }

        public PostGetResponse Get(
            PostGetRequest request)
        {
            request.Validate();

            var user = _unitOfWork.GetRepository<User>().GetAll().Single(x => x.UniqueIdentifier.ToString() == request.UserUniqueIdentifier.ToString());

            var where = _unitOfWork.GetRepository<Post>().GetAll().Where(x =>
                 x.UserId == user.Id
            );
            where = where.OrderByDescending(x => x.Id);
            if (request.StartFilter.HasValue)
            {
                var value = request.StartFilter.Value;
                where = where.Where(x => x.Created >= value);
            }
            if (request.EndFilter.HasValue)
            {
                var value = request.EndFilter.Value;
                where = where.Where(x => x.Created <= value);
            }
            if (request.Skip > 0)
            {
                where = where.Skip(request.Skip);
            }
            if (request.Take > 0)
            {
                where = where.Take(request.Take);
            }

            var posts = where.ToList();

            var response = new PostGetResponse
            {
                Posts = posts
            };

            return response;
        }

        public PostGetResponse GetFriends(
            PostGetRequest request)
        {
            request.Validate();

            var user = _unitOfWork.GetRepository<User>().GetAll().Single(x => x.UniqueIdentifier.ToString() == request.UserUniqueIdentifier.ToString());
            var friendIds = _unitOfWork.GetRepository<UserLink>().GetAll().Where(x =>
                x.FromId == user.Id
            ).Select(x => x.ToId);

            var where = _unitOfWork.GetRepository<Post>().GetAll().Where(x =>
                 friendIds.Contains(x.UserId)
            );
            where = where.OrderByDescending(x => x.Id);
            if (request.StartFilter.HasValue)
            {
                var value = request.StartFilter.Value;
                where = where.Where(x => x.Created >= value);
            }
            if (request.EndFilter.HasValue)
            {
                var value = request.EndFilter.Value;
                where = where.Where(x => x.Created <= value);
            }
            if (request.Skip > 0)
            {
                where = where.Skip(request.Skip);
            }
            if (request.Take > 0)
            {
                where = where.Take(request.Take);
            }

            var posts = where.ToList();

            var response = new PostGetResponse
            {
                Posts = posts
            };

            return response;
        }
    }
}