using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KH_SSD_W38.Controllers;

[Authorize(Roles = Roles.Editor)]
public class EditorController : Controller
{
    // Actions for reading, writing, editing, and deleting posts
}