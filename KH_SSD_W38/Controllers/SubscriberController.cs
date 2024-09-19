using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KH_SSD_W38.Controllers
{
    [Authorize(Roles = Roles.Subscriber)]
    public class SubscriberController : Controller
    {
        // Actions for reading and commenting on posts
    }
}
