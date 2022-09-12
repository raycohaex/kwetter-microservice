using Kweet.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Domain.Entities
{
    public class Kweet : EntityBase
    {
        public string UserName { get; set; }
        public string TweetBody
    }
}
