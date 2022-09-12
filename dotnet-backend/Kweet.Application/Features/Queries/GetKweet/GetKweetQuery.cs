using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Features.Queries.GetKweet
{
    public class GetKweetQuery : IRequest<KweetViewModel>
    {
        public Guid Id { get; set; }

        public GetKweetQuery(Guid id)
        {
            Id = id;
        }
    }
}
