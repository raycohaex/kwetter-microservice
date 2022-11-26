using AutoMapper;
using CQRS.Core.EventSourcing;
using Kweet.Cmd.Domain.Aggregates;
using Kweet.Cmd.Infrastructure.EventSourcing;
using Kweet.Common.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Application.Commands.PostKweet
{
    public class PostKweetCommandHandler : IRequestHandler<PostKweetCommand, KweetEntity>
    {
        private readonly IEventSourcingHandler<KweetAggregate> _handler;
        private readonly IMapper _mapper;
        private readonly ILogger<PostKweetCommandHandler> _logger;

        public PostKweetCommandHandler(IEventSourcingHandler<KweetAggregate> handler, IMapper mapper, ILogger<PostKweetCommandHandler> logger)
        {
            _handler = handler;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<KweetEntity> Handle(PostKweetCommand request, CancellationToken cancellationToken)
        {
            var aggregate = new KweetAggregate(request.Id, request.UserName, request.TweetBody);

            await _handler.SaveAsync(aggregate);

            return null;
        }
    }
}
