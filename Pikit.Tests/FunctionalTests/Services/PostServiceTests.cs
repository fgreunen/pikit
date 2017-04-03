using NUnit.Framework;
using Pikit.Entities.Entities;
using Pikit.Services.Interfaces;
using Pikit.Services.Requests;
using Pikit.Shared;
using Pikit.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.FunctionalTests.Services
{
    [TestFixture]
    public class PostServiceTests
        : FunctionalTestBase
    {
        private User CreateUser()
        {
            var user = new User
            {
                UniqueIdentifier = Guid.NewGuid()
            };
            UnitOfWork.GetRepository<User>().Create(user);
            UnitOfWork.Save();
            RefreshDbContext();
            return user;
        }

        [Test]
        public void Create()
        {
            var user = CreateUser();

            var request = new PostCreateRequest
            {
                Description = "My Description",
                Resource1 = new byte[] { },
                Resource2 = new byte[] { },
                UserUniqueIdentifier = user.UniqueIdentifier
            };

            var response = Kernel.Get<IPostService>().Create(request);

            RefreshDbContext();
            Assert.That(UnitOfWork.GetRepository<Post>().GetAll().Count() == 1);
            var post = UnitOfWork.GetRepository<Post>().GetAll().Single();
            Assert.That(post.Description == request.Description);
            Assert.That(post.UserId == user.Id);
            Assert.That(post.UniqueIdentifier == response.PostUniqueIdentifier);
        }

        [Test]
        public void Get()
        {
            var user = CreateUser();

            var request = new PostGetRequest
            {
                UserUniqueIdentifier = user.UniqueIdentifier
            };

            var post = new Post
            {
                ResourceUrl1 = string.Empty,
                ResourceUrl2 = string.Empty,
                UniqueIdentifier = Guid.NewGuid(),
                Description = string.Empty,
                UserId = user.Id,
                Created = request.RequestTimestamp
            };

            UnitOfWork.GetRepository<Post>().Create(post);
            UnitOfWork.Save();

            var response = Kernel.Get<IPostService>().Get(request);

            RefreshDbContext();
            Assert.That(response.Posts.Count == 1);
            Assert.That(response.Posts[0].Id == post.Id);
        }

        [Test]
        public void GetFriends()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();

            var userLink = new UserLink
            {
                FromId = user1.Id,
                ToId = user2.Id,
                Created = DateTime.Now
            };

            UnitOfWork.GetRepository<UserLink>().Create(userLink);
            UnitOfWork.Save();

            var request = new PostGetRequest
            {
                UserUniqueIdentifier = user1.UniqueIdentifier
            };

            var post = new Post
            {
                ResourceUrl1 = string.Empty,
                ResourceUrl2 = string.Empty,
                UniqueIdentifier = Guid.NewGuid(),
                Description = string.Empty,
                UserId = user2.Id,
                Created = request.RequestTimestamp
            };

            UnitOfWork.GetRepository<Post>().Create(post);
            UnitOfWork.Save();

            var response = Kernel.Get<IPostService>().GetFriends(request);

            RefreshDbContext();
            Assert.That(response.Posts.Count == 1);
            Assert.That(response.Posts[0].Id == post.Id);
        }
    }
}