using DefyClinicInfastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DefyClinicMVC.Startup))]
namespace DefyClinicMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        {
            DefyClinicContxet context = new DefyClinicContxet();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                //create super admin role
                var role = new IdentityRole("Admin");
                roleManager.Create(role);

                //create default user
                var user = new ApplicationUser();
                user.UserName = "Admin@gmail.com";
                user.Email = "Admin@gmail.com";
                string pwd = "Admin@2019";

                var newuser = userManager.Create(user, pwd);

                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");

                }
            }
        }
    }
}
