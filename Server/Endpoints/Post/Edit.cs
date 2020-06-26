using System.Threading;
using System.Threading.Tasks;
using ASimpleBlogStarter.Server.Data;
using ASimpleBlogStarter.Shared.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ASimpleBlogStarter.Server.Endpoints.Post
{
    public class EditQueryHandler : IRequestHandler<Get.Query, Get.Model>
    {
        private readonly ApplicationDbContext _dbContext;

        public EditQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Get.Model> Handle(Get.Query request, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Posts.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return new Get.Model
            {
                Id = existing.Id,
                Body = existing.Body,
                Slug = existing.Slug,
                Title = existing.Title
            };
        }
    }

    public class EditCommandHandler : AsyncRequestHandler<Update.Command>
    {
        private readonly ApplicationDbContext _dbContext;

        public EditCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(Update.Command command, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Posts.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            existing.Body = command.Body;
            existing.Title = command.Title;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}