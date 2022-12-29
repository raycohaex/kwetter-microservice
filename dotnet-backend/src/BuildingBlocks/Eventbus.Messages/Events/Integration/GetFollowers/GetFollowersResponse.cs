using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events.Integration.GetFollowers
{
    public class GetFollowersResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<string>? Followers { get; set; }
        public bool IsCeleb { get; set; }

    }
}
