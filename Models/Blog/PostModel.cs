using M101N.Blog;

namespace MongoDB.DotNet.Models.Blog
{
    public class PostModel
    {
        public Post Post { get; set; }

        public NewCommentModel NewComment { get; set; }
    }
}