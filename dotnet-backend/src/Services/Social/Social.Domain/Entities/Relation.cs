using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    public class Relation
    {
        public string Follower { get; set; } = null!;
        public string Followee { get; set; } = null!;
    }
}
