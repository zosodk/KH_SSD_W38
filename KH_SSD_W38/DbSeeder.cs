using Microsoft.AspNetCore.Identity;

namespace KH_SSD_W38;

public class DbSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public DbSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedRolesAsync()
    {
        var roles = new[] { Roles.Editor, Roles.Writer, Roles.Subscriber };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public async Task SeedUsersAsync()
    {
        var editor = new IdentityUser { UserName = "editor@example.com", Email = "editor@example.com" };
        await _userManager.CreateAsync(editor, "Password123!");
        await _userManager.AddToRoleAsync(editor, Roles.Editor);

        var writer = new IdentityUser { UserName = "writer@example.com", Email = "writer@example.com" };
        await _userManager.CreateAsync(writer, "Password123!");
        await _userManager.AddToRoleAsync(writer, Roles.Writer);

        var subscriber = new IdentityUser { UserName = "subscriber@example.com", Email = "subscriber@example.com" };
        await _userManager.CreateAsync(subscriber, "Password123!");
        await _userManager.AddToRoleAsync(subscriber, Roles.Subscriber);
    }
}