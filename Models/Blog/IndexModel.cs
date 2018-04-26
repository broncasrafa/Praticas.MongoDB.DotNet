using System.Collections.Generic;
using M101N.Blog;

namespace MongoDB.DotNet.Models.Blog
{
    public class IndexModel
    {
        public List<Post> RecentPosts { get; set; }
    }
}