using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Dto
{
    public class RelationDto
    {
        public Guid? Id { get; set; }
        public Guid? Follower { get; set; }
        public Guid? Followee { get; set; }
    }
}
