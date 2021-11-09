using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly ISlugValidator _slugValidator;

            public CommandValidator(ISlugValidator slugValidator)
            {
                _slugValidator = slugValidator;

                RuleFor(c => c.Body).NotEmpty();

                RuleFor(c => c.Slug).NotEmpty()
                    .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                    .WithMessage(
                    "Please enter a valid slug, using only alphanumeric characters and hyphens e.g. test-post")
                    .MustAsync(BeUniqueSlug)
                    .WithMessage("Slug must be unique, another post exists which uses this slug");

                RuleFor(c => c.Title).NotEmpty();
            }

            private async Task<bool> BeUniqueSlug(string slug, CancellationToken token)
            {
                return await _slugValidator.IsUnique(slug);
            }
        }

    }
}