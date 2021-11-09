using System.Threading.Tasks;

namespace ASimpleBlogStarter.Shared.Post
{
    public interface ISlugValidator
    {
        Task<bool> IsUnique(string slug);
    }
}