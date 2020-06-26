using MediatR;

namespace ASimpleBlogStarter.Shared.Post
{
    public class Search
    {
        public class Query : IRequest<Model>
        {
            public string Slug { get; set; }
        }

        public class Model
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Body { get; set; }
        }
    }
}