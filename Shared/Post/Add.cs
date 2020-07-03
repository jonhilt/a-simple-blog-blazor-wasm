using MediatR;

namespace ASimpleBlogStarter.Shared.Post
{
    public class Add
    {
        public class Command : IRequest<CommandResult>
        {
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Body { get; set; }
        }

    }
}