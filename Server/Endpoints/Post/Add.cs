using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASimpleBlogStarter.Server.Data;
using ASimpleBlogStarter.Shared;
using ASimpleBlogStarter.Shared.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ASimpleBlogStarter.Server.Endpoints.Post
{
    public class AddHandler : IRequestHandler<Add.Command, CommandResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public AddHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(Add.Command command, CancellationToken cancellationToken)
        {
            var result = new CommandResult();

            if (await _dbContext.Posts.AnyAsync(x => x.Slug == command.Slug, cancellationToken: cancellationToken))
            {
                result.AddError(nameof(command.Slug), "Slug already used, please try again");
                return result;
            }

            // only proceed if the slug isn't already in the system
            await _dbContext.Posts.AddAsync(new Models.Post
            {
                Body = command.Body,
                Slug = command.Slug,
                Title = command.Title
            }, cancellationToken);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}