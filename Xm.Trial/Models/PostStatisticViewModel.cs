using Xm.Trial.Models.Data;
using System.Collections.Generic;

namespace Xm.Trial.Models
{
    public class PostStatisticViewModel
    {
        public int ID { get; set; }

        public ICollection<PostView> PostView { get; set; }

        public int TotalViews { get; set; }

        public int Likes { get; set; }
    }
}