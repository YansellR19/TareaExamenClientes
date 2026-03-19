using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TareaExamenClientes.Data;

public static class DbInitializer {
    public static async Task SeedRolesAndAdminAsync(IServiceProvider services) {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // 1. Crear los 3 roles que pide la tarea
        string[] roles = { "Admin", "Editor", "Cliente" };
        foreach (var role in roles) {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2. Crear el usuario Admin por defecto
        var adminEmail = "admin@unicda.edu.do";
        if (await userManager.FindByEmailAsync(adminEmail) == null) {
            var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}