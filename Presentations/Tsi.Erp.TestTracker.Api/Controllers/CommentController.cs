using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly UserAzureADManager<ApplicationUser, TsiIdentityPermission> _userAzureAdManager;

        public CommentsController(ICommentRepository commentRepository,
                                  IMapper mapper,
                                  UserAzureADManager<ApplicationUser, TsiIdentityPermission> userAzureAdManager)
        {
            _commentRepository = commentRepository;

            _mapper = mapper;
            _userAzureAdManager = userAzureAdManager;
        }


        // GET: api/<CommentController>    
        [HttpGet]
        [TsiAuthorize(Permissions.Comment_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync()
        {
            var comments = (await _commentRepository.GetAsync()).ToList();
            var commentResults = _mapper.Map<List<CommentDto>>(comments);
            return Ok(commentResults);
        }
        // GET api/<CommentController>/5

        [HttpGet("{id}")]
        [TsiAuthorize(Permissions.Comment_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var comment = await Task.FromResult(_commentRepository.Find(id));
            if (comment is null)
            {
                return NotFound();
            }
            var commentResult = _mapper.Map<Comment, CommentDto>(comment);
            return Ok(commentResult);

        }

        // POST api/<CommentController>
        [HttpPost]
        [TsiAuthorize(Permissions.Comment_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync([FromBody] CommentModel comment)
        {
            var commentResult = _mapper.Map<Comment>(comment);

            commentResult.Date = DateTime.Now;
            commentResult.UserId = _userAzureAdManager.GetUserId(HttpContext.User);

            _commentRepository.Create(commentResult);

            await _commentRepository.SaveAsync();

            return CreatedAtAction(nameof(GetAsync),
                new { id = commentResult.Id },
                _mapper.Map<CommentDto>(commentResult));
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        [TsiAuthorize(Permissions.Comment_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CommentModel commentDto)
        {
            var comment = _commentRepository.Find(id);
            if (comment is null)
            {
                return NotFound("module not exist ");
            }
            var moduleResult = _mapper.Map(commentDto,comment);
            _commentRepository.Update(moduleResult);
            await _commentRepository.SaveAsync();
            return NoContent();

        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        [TsiAuthorize(Permissions.Comment_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var comment = _commentRepository.Find(id);
            if (comment is null)
            {
                return NotFound();
            }
            _commentRepository.Delete(id);
            await _commentRepository.SaveAsync();
            return Ok();
        }
     
    }
}
