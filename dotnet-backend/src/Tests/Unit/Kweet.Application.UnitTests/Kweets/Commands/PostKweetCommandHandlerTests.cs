using AutoMapper;
using Castle.Core.Logging;
using Kweet.Application.Contracts.Persistence;
using Kweet.Application.Features.Commands.PostKweet;
using Kweet.Application.Mapping;
using Kweet.Application.UnitTests.Mocks;
using Kweet.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.UnitTests.Kweets.Commands
{
    public class PostKweetCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IKweetRepository> _mockRepository;
        private readonly ILogger<PostKweetCommandHandler> _logger;

        public PostKweetCommandHandlerTests()
        {
            _mockRepository = MockKweetRepository.GetKweetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _logger = null;

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async void CreateKweet_ByUserEmma_SuccessfullyCreateAndReturnRightValues()
        {
            var _kweetEntity = new KweetEntity
            {
                UserName = "Emma",
                TweetBody = "Test Kweet"
            };

            var handler = new PostKweetCommandHandler(_mockRepository.Object, _mapper, _logger);

            var result = await handler.Handle(new PostKweetCommand( ) { TweetBody = _kweetEntity.TweetBody, UserName = _kweetEntity.UserName}, CancellationToken.None);

            var fakeResponse = Assert.IsType<KweetEntity>(result);

            Assert.NotNull(fakeResponse);
            Assert.IsType<Guid>(fakeResponse.Id);
            Assert.Equal("Emma", fakeResponse.UserName);
            Assert.Equal("Test Kweet", fakeResponse.TweetBody);
            Assert.Equal("TO_BE_IMPLEMENTED", fakeResponse.CreatedBy);
        }
    }

}
