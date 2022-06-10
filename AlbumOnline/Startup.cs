using Microsoft.Owin;
using Owin;
using AlbumOnline.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(AlbumOnline.Startup))]
namespace AlbumOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoleAndUser();
        }

        private void CreateRoleAndUser()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var rolManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if(!rolManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                rolManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "rsolis@unan.edu.ni";
                user.Email= "rsolis@unan.edu.ni";
                string PDW = "123456";

                var chkUser = userManager.Create(user, PDW);
                if(chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id,"Admin");
                }
            }

            if (!rolManager.RoleExists("freemium"))
            {
                var role = new IdentityRole();
                role.Name = "freemium";
                rolManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "rsolis@yahoo.es";
                user.Email = "rsolis@yahoo.es";
                string PDW = "123456";

                var chkUser = userManager.Create(user, PDW);
                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "freemium");
                }
            }
        }
    }
}
