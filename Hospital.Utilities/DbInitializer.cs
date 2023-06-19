using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Utilities
{


    public class DbInitializer : IDbInitializer
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicatopnDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ApplicatopnDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize() {

            try
            {
                if (_context.Database.GetPendingMigrations().Count()>0)
                {

                    _context.Database.Migrate();
                }


            }

            catch (Exception)
            {
                throw;
            }

            if(!_roleManager.RoleExistsAsync(WebsiteRoles.Website_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Patient)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Doctor)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                    {
                    UserName  = "Ezdehar", Email="Ezdehar@hotmail.com"


                },"Ezdehar@123").GetAwaiter().GetResult();

                var Appuser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "Ezdehar@hotmail.com");
                if (Appuser != null) {

                    _userManager.AddToRoleAsync(Appuser, WebsiteRoles.Website_Admin).GetAwaiter().GetResult();
                }

            }

        }
    }
}
