namespace Blog.ApplicationCore.Features.Post.Commands.CreatePost
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Lead { get; set; }
        public string Body { get; set; }
    }
}