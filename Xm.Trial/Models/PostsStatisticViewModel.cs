using System.Collections.Generic;

namespace Xm.Trial.Models
{
    public class PostsStatisticViewModel
    {
        public ICollection<PostStatisticViewModel> PostStatistic { get; set; }

        public int TotalPosts { get; set; }

        public int TotalViews { get; set; }

        public int TotalLikes { get; set; }
    }
}