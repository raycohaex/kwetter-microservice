using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Features.Queries.GetKweet
{
    public class KweetViewModel
    {
        // Metadata
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Kweet content
        public string UserName { get; set; }
        public string TweetBody { get; set; }
    }
}
