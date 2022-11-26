using Kweet.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Features.Commands.PostKweet
{
    public class PostKweetCommand: IRequest<KweetEntity>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string TweetBody { get; set; } = null!;
    }
}
