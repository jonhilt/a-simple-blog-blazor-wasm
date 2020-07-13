using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ASimpleBlogStarter.Server.Data;
using ASimpleBlogStarter.Server.Plumbing;
using ASimpleBlogStarter.Shared.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ASimpleBlogStarter.Server.Endpoints.Post
{
    public class SearchHandler : IRequestHandler<Search.Query, Search.Model>
    {
        private readonly ApplicationDbContext _dbContext;

        public SearchHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Search.Model> Handle(Search.Query request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(x => x.Slug == request.Slug,
                cancellationToken: cancellationToken);

            if (post == null)
                throw new RestException(HttpStatusCode.NotFound, "Post not found");

            return new Search.Model
            {
                Id = post.Id,
                Body = post.Body,
                Slug = post.Slug,
                Title = post.Title
            };
        }
    }
}