using AutoMapper;
using Kweet.Application.Contracts.Persistence;
using Kweet.Application.Features.Queries.GetKweet;
using Kweet.Application.Mapping;
using Kweet.Application.UnitTests.Mocks;
using Kweet.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.UnitTests.Kweets.Queries
{
    public class GetKweetQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IKweetRepository> _mockRepository;

        public GetKweetQueryHandlerTests()
        {
            _mockRepository = MockKweetRepository.GetKweetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async void GetKweetById_ReturnsOneRecord()
        {
            var handler = new GetKweetQueryHandler(_mockRepository.Object, _mapper);

            var result = await handler.Handle(new GetKweetQuery(new Guid("0ebc33bb-830d-498b-8bcb-b4e2e8b729a4")), CancellationToken.None);

            var fakeResponse = Assert.IsType<KweetViewModel>(result);

            Assert.NotNull(fakeResponse);
        }
    }
}
