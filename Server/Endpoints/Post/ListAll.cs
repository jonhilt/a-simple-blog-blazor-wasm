using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASimpleBlogStarter.Server.Data;
using ASimpleBlogStarter.Shared.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ASimpleBlogStarter.Server.Endpoints.Post
{
    public class ListAllQueryHandler : IRequestHandler<ListAll.Query, ListAll.Model>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAllQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ListAll.Model> Handle(ListAll.Query request,
            CancellationToken cancellationToken)
        {
            var posts = await _dbContext.Posts
                .Select(x => new Shared.Post.ListAll.Model.Post
                {
                    Id = x.Id,
                    Slug = x.Slug,
                    Title = x.Title
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return new ListAll.Model { Posts = posts };
        }
    }
}