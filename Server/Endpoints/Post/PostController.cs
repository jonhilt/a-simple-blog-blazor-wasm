using System.Threading.Tasks;
using ASimpleBlogStarter.Shared.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ASimpleBlogStarter.Server.Endpoints.Post
{
    [Route("api/post")]
    public class PostController : Controller
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> List()
        {
            var posts = await _mediator.Send(new ListAll.Query());
            return Ok(posts);
        }

        [Route("{id}")]
        public async Task<IActionResult> Find(Get.Query query)
        {
            var post = await _mediator.Send(query);
            return Ok(post);
        }

        [Route("search")]
        public async Task<IActionResult> Find([FromQuery] Search.Query query)
        {
            var post = await _mediator.Send(query);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Add.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Update.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}