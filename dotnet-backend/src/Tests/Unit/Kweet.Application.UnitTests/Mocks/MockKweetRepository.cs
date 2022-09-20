using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kweet.Application.Contracts.Persistence;
using Kweet.Domain.Entities;

namespace Kweet.Application.UnitTests.Mocks
{
    public static class MockKweetRepository
    {
        public static Mock<IKweetRepository> GetKweetRepository()
        {
            var kweets = new List<KweetEntity>
            {
                new KweetEntity
                {
                    Id = new Guid("0ebc33bb-830d-498b-8bcb-b4e2e8b729a4"),
                    UserName = "David",
                    TweetBody = "David's first Kweet",
                    CreatedBy = "David",
                    CreatedOn = new DateTime(2022, 9, 20, 12, 00, 00)
                },
                new KweetEntity
                {
                    Id = new Guid("085144ae-2052-412b-821a-789da53402c5"),
                    UserName = "David",
                    TweetBody = "David's second Kweet",
                    CreatedBy = "David",
                    CreatedOn = new DateTime(2022, 9, 20, 12, 10, 00)
                },
                new KweetEntity
                {
                    Id = new Guid("ddf21924-fcc3-4e43-90e4-d4e39d0b22e6"),
                    UserName = "Emma",
                    TweetBody = "Emma's first Kweet",
                    CreatedBy = "Emma",
                    CreatedOn = new DateTime(2022, 9, 20, 11, 30, 00)
                },
            };

            var mockRepository = new Mock<IKweetRepository>();

            // I'm only mocking repository methods that I used one or more times.
            // Get 1 Kweet
            mockRepository.Setup(r => r.GetKweetById(It.IsAny<Guid>())).ReturnsAsync((Guid id) => kweets.Single(k => k.Id == id));

            // Post 1 kweet
            mockRepository.Setup(r => r.AddAsync(It.IsAny<KweetEntity>())).ReturnsAsync((KweetEntity kweet) =>
            {
                kweets.Add(kweet);
                return kweet;
            });

            return mockRepository;
        }
    }
}
