using Microsoft.AspNetCore.Identity;

namespace KH_SSD_W38;

public class UserSeeder
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserSeeder(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedUsersAsync()
    {
        var editor = new IdentityUser { UserName = "editor@easvproject.com", Email = "editor@easvproject" };
        await _userManager.CreateAsync(editor, "K0de1234!");
        await _userManager.AddToRoleAsync(editor, Roles.Editor);

        var writer = new IdentityUser { UserName = "writer@easvproject", Email = "writer@easvproject" };
        await _userManager.CreateAsync(writer, "K0de1234!");
        await _userManager.AddToRoleAsync(writer, Roles.Writer);

        var anotherwriter = new IdentityUser { UserName = "anotherwriter@easvproject", Email = "anotherwriter@easvproject" };
        await _userManager.CreateAsync(anotherwriter, "K0de1234!");
        await _userManager.AddToRoleAsync(anotherwriter, Roles.Writer);

        var subscriber = new IdentityUser { UserName = "subscriber@google.com", Email = "subscriber@google.com" };
        await _userManager.CreateAsync(subscriber, "K0de1234!");
        await _userManager.AddToRoleAsync(subscriber, Roles.Subscriber);
    }
}