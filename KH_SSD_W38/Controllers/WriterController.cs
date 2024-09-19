using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KH_SSD_W38.Controllers
{
    [Authorize(Roles = Roles.Writer)]
    public class WriterController : Controller
    {
        // Actions for reading and writing posts, but only editing own posts
    }
}
