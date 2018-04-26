using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using M101N.Blog;
using MongoDB.Bson;
using MongoDB.DotNet.Models.Blog;
using MongoDB.Driver;

namespace MongoDB.DotNet.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var blogContext = new BlogContext();

            // find the most recent 10 posts and order them from newest to oldest
            var recentPosts = await blogContext.Posts
                                               .Find(new BsonDocument())
                                               .Limit(10)
                                               .Sort(Builders<Post>.Sort.Descending("CreatedAtUtc"))
                                               .ToListAsync();

            var viewModel = new IndexModel
            {
                RecentPosts = recentPosts
            };

            return View(viewModel);
        }

        [HttpGet]        
        public ActionResult NewPost()
        {
            return View(new NewPostModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> NewPost(NewPostModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var blogContext = new BlogContext();

            // Insert the post into the posts collection
            var post = new Post
            {
                Author = User.Identity.Name,
                CreatedAtUtc = DateTime.UtcNow,
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags.Split(' ', ',', ';'),
                Comments = new List<Comment>()
            };

            await blogContext.Posts.InsertOneAsync(post);

            return RedirectToAction("Post", new { id = post.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Post(string id)
        {
            var blogContext = new BlogContext();

            // Find the post with the given identifier
            var post = await blogContext.Posts
                                        .Find(c => c.Id == id)
                                        .SingleOrDefaultAsync();

            if(post == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new PostModel
            {
                Post = post,
                NewComment = new NewCommentModel
                {
                    PostId = id
                }
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Posts(string tag = null)
        {
            var blogContext = new BlogContext();

            Expression<Func<Post, bool>> filter = x => true;

            if(tag != null)
            {
                filter = x => x.Tags.Contains(tag);
            }

            var posts = await blogContext.Posts
                                         .Find(filter)
                                         .SortByDescending(x => x.CreatedAtUtc)
                                         .Limit(10)
                                         .ToListAsync();

            #region Meu teste
            //    var blogContext = new BlogContext();

            //    // XXX WORK HERE
            //    // Find all the posts with the given tag if it exists.
            //    // Otherwise, return all the posts.
            //    // Each of these results should be in descending order.

            //    var posts = new List<Post>();

            //    if (!String.IsNullOrWhiteSpace(tag))
            //    {                
            //        string pattern = $"/{tag}/";

            //        var filter = Builders<Post>.Filter.Regex("Tags", new BsonRegularExpression(pattern));

            //        posts = await blogContext.Posts
            //                   .Find(filter)
            //                   .Sort(Builders<Post>.Sort.Descending("CreatedAtUtc"))
            //                   .ToListAsync();
            //    }
            //    else
            //    {
            //        posts = await blogContext.Posts
            //                                   .Find(new BsonDocument())
            //                                   .Sort(Builders<Post>.Sort.Descending("CreatedAtUtc"))
            //                                   .ToListAsync();
            //    }
            #endregion

            return View(posts);
        }

        [HttpPost]
        public async Task<ActionResult> NewComment(NewCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = model.PostId });
            }

            var blogContext = new BlogContext();

            var comment = new Comment
            {
                Author = User.Identity.Name,
                Content = model.Content,
                CreatedAtUtc = DateTime.UtcNow
            };

            //var filter = Builders<Post>.Filter.Eq(c => c.Id, model.PostId);

            //var update = Builders<Post>.Update.Push(c => c.Comments, comment);

            //await blogContext.Posts.FindOneAndUpdateAsync(filter, update);

            await blogContext.Posts
                             .UpdateOneAsync(c => c.Id == model.PostId, Builders<Post>.Update.Push(c => c.Comments, comment));

            return RedirectToAction("Post", new { id = model.PostId });
        }
    }
}