using Kweet.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Application.Commands.PostKweet
{
    public class PostKweetCommand : IRequest<KweetEntity>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string TweetBody { get; set; }
    }
}
