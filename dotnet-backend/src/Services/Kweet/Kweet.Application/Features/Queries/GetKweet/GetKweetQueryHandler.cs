using AutoMapper;
using Kweet.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Features.Queries.GetKweet
{
    public class GetKweetQueryHandler : IRequestHandler<GetKweetQuery, KweetViewModel>
    {
        private readonly IKweetRepository _kweetRepository;
        private readonly IMapper _mapper;

        public GetKweetQueryHandler(IKweetRepository kweetRepository, IMapper mapper)
        {
            _kweetRepository = kweetRepository;
            _mapper = mapper;
        }

        public async Task<KweetViewModel> Handle(GetKweetQuery request, CancellationToken cancellationToken)
        {
            var orders = await _kweetRepository.GetKweetById(request.Id);
            return _mapper.Map<KweetViewModel>(orders);
        }
    }
}
