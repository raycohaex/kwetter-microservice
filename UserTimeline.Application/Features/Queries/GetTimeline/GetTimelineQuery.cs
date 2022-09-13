using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTimeline.Application.Features.Queries.GetTimeline
{
    public class GetTimelineQuery : IRequest<TimelineViewModel>
    {
        public string Username { get; set; }

        public GetTimelineQuery(string username)
        {
            Username = username;
        }
    }
}
