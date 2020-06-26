using System.Collections.Generic;
using MediatR;

namespace ASimpleBlogStarter.Shared.Post
{
    public class ListAll
    {
        public class Query : IRequest<Model>
        {
        }

        public class Model
        {
            public List<Post> Posts { get; set; } = new List<Post>();

            public class Post
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string Slug { get; set; }
            }
        }
    }
}