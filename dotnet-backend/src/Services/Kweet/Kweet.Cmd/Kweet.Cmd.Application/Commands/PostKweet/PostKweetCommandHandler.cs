using AutoMapper;
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
        private readonly IKweetRepository _kweetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostKweetCommandHandler> _logger;

        public PostKweetCommandHandler(IKweetRepository kweetRepository, IMapper mapper, ILogger<PostKweetCommandHandler> logger)
        {
            _kweetRepository = kweetRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<KweetEntity> Handle(PostKweetCommand request, CancellationToken cancellationToken)
        {
            
        }
    }
}
