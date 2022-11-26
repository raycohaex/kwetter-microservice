using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Common.Entities
{
    public class KweetEntity : EntityBase
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string TweetBody { get; set; }
    }
}
