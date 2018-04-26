using System.ComponentModel.DataAnnotations;

namespace MongoDB.DotNet.Models.Blog
{
    public class NewPostModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Tags { get; set; }
    }
}