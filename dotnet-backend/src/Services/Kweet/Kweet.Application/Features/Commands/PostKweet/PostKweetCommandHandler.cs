using AutoMapper;
using Eventbus.Messages.Events;
using Kweet.Application.Contracts.Persistence;
using Kweet.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Features.Commands.PostKweet
{
    public class PostKweetCommandHandler : IRequestHandler<PostKweetCommand, KweetEntity>
    {
        private readonly IKweetRepository _kweetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostKweetCommandHandler> _logger;
        private readonly IPublishEndpoint _pubEndpoint;

        public PostKweetCommandHandler(IKweetRepository kweetRepository, IMapper mapper, ILogger<PostKweetCommandHandler> logger, IPublishEndpoint bus)
        {
            _kweetRepository = kweetRepository;
            _mapper = mapper;
            _pubEndpoint = bus;
            _logger = logger;
        }

        public async Task<KweetEntity> Handle(PostKweetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var kweetEntity = _mapper.Map<KweetEntity>(request);
                // TODO: replace with user ID
                kweetEntity.CreatedBy = "TO_BE_IMPLEMENTED";
                kweetEntity.CreatedOn = DateTime.Now;
                var newKweet = await _kweetRepository.AddAsync(kweetEntity);

                if (newKweet != null)
                {
                    var eventMessage = _mapper.Map<KweetPostedEvent>(newKweet);
                    await _pubEndpoint.Publish(eventMessage);
                }

                return newKweet;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to persist Kweet", ex);
            }
        }
    }
}
