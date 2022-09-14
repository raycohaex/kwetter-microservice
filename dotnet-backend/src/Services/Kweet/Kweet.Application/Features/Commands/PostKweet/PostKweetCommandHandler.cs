using AutoMapper;
using Kweet.Application.Contracts.Persistence;
using Kweet.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Features.Commands.PostKweet
{
    public class PostKweetCommandHandler : IRequestHandler<PostKweetCommand, Guid>
    {
        private readonly IKweetRepository _kweetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostKweetCommandHandler> _logger;

        public PostKweetCommandHandler(IKweetRepository kweetRepository, IMapper mapper, ILogger<PostKweetCommandHandler> logger)
        {
            _kweetRepository = kweetRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(PostKweetCommand request, CancellationToken cancellationToken)
        {
            var kweetEntity = _mapper.Map<KweetEntity>(request);
            // TODO: replace with user ID
            kweetEntity.CreatedBy = "TO_BE_IMPLEMENTED";
            kweetEntity.CreatedOn = DateTime.Now;

            var newKweet = await _kweetRepository.AddAsync(kweetEntity);

            return newKweet.Id;
        }
    }
}
