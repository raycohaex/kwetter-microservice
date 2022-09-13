using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Domain;

namespace UserTimeline.Application.Features.Commands.UpdateTimeline
{
    public class UpdateTimelineCommand : IRequest
    {
        public string UserName { get; set; }
        // Create list that we store in Redis
        public List<KweetEntity> Items { get; set; } = new List<KweetEntity>();
        public UpdateTimelineCommand()
        {

        }

        public UpdateTimelineCommand(string userName)
        {
            UserName = userName;
        }
    }
}
