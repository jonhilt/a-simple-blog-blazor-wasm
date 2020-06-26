using MediatR;

namespace ASimpleBlogStarter.Shared.Post
{
    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }
    }
}