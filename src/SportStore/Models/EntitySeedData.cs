using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportStore.Models
{
    public class EntitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();
            context.Database.Migrate();
            UserManager<IdentityUser> userManager = 
            app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(adminUser);

            if(user == null)
            {
                user = new IdentityUser("admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}