using System.Threading;
using System.Threading.Tasks;
using ASimpleBlogStarter.Server.Data;
using ASimpleBlogStarter.Shared.Post;
using MediatR;

namespace ASimpleBlogStarter.Server.Endpoints.Post
{
    public class AddHandler : AsyncRequestHandler<Add.Command>
    {
        readonly ApplicationDbContext _dbContext;

        public AddHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(Add.Command command, CancellationToken cancellationToken)
        {
            await _dbContext.Posts.AddAsync(new Models.Post
            {
                Body = command.Body,
                Slug = command.Slug,
                Title = command.Title
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}