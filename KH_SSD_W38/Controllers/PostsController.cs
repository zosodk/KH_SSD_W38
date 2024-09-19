using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KH_SSD_W38.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PostsController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            var user = await _userManager.GetUserAsync(User);
            post.AuthorId = user.Id;
            post.CreatedAt = DateTime.UtcNow;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            var user = await _userManager.GetUserAsync(User);
            var existingPost = await _context.Posts.FindAsync(id);

            if (existingPost == null)
            {
                return NotFound();
            }

            if (existingPost.AuthorId != user.Id && !User.IsInRole(Roles.Editor))
            {
                return Forbid();
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.UpdatedAt = DateTime.UtcNow;

            _context.Posts.Update(existingPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Editor)]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    /*
    public class PostsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public PostsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.Editor)]
        public async Task<IActionResult> DeletePost(int postId)
        {
            // Logic for deleting a post
        }

        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> EditPost(int postId)
        {
            var user = await _userManager.GetUserAsync(User);
            var post = await _context.Posts.FindAsync(postId);

            if (post.AuthorId != user.Id)
            {
                return Forbid();
            }

            // Logic for editing the post
        }

        [Authorize(Roles = Roles.Subscriber)]
        public async Task<IActionResult> CommentOnPost(int postId, string comment)
        {
            // Logic for commenting on a post
        }

        [AllowAnonymous]
        public IActionResult ViewPost(int postId)
        {
            // Logic for viewing a post and displaying ads
        }
    }*/
}
