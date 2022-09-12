using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Features.Commands.PostKweet
{
    internal class PostKweetCommand: IRequest<Guid>
    {
        public string UserName { get; set; }
        public string TweetBody { get; set; }
    }
}
