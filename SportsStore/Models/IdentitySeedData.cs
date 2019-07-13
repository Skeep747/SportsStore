using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByNameAsync(adminUser);
            if(user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
