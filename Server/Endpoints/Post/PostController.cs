using System.Linq;
using System.Threading.Tasks;
using ASimpleBlogStarter.Shared.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ASimpleBlogStarter.Server.Endpoints.Post
{
    [ApiController]
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
        public async Task<IActionResult> Get([FromRoute]Get.Query query)
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
        public async Task<IActionResult> Add([FromBody] Add.Command command)
        {
           await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Update.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}