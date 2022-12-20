using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events.Integration.GetFollowers
{
    public class GetFollowersRequest
    {
        public Guid UserId { get; set; }
    }
}
