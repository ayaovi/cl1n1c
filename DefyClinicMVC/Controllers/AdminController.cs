using DefyClinicInfastructure;
using DefyClinicMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DefyClinicMVC.Controllers
{
  [Authorize(Roles = "Admin")]
  public class AdminController : Controller
  {
    // GET: Admin
    readonly DefyClinicContxet _context = new DefyClinicContxet();
    // GET: Admin
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult CreateUser()
    {
      return View();
    }

    [HttpPost]
    public ActionResult CreateUser(FormCollection form)
    {
      var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
      var username = form["txtEmail"];
      var email = form["txtEmail"];
      var pwd = form["txtPassword"];

      //create default user
      var user = new ApplicationUser { UserName = username, Email = email };
      var newuser = userManager.Create(user, pwd);
      return View();
    }

    public ActionResult CreateRole()
    {
      return View();
    }

    [HttpPost]
    public ActionResult NewRole(FormCollection form)
    {
      var rolename = form["RoleName"];
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

      if (!roleManager.RoleExists(rolename))
      {
        //create super admin role
        var role = new IdentityRole(rolename);
        roleManager.Create(role);
      }
      return View("Index");
    }

    public ActionResult AssignRole()
    {
      ViewBag.Roles = _context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
      return View();
    }

    [HttpPost]
    public ActionResult AssignRole(FormCollection form)
    {
      var usrname = form["txtUserName"];
      var rolname = form["RoleName"];
      var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(usrname, StringComparison.CurrentCultureIgnoreCase));
      var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
      userManager.AddToRole(user.Id, rolname);

      return View("Index");
    }

    [HttpGet]
    public ActionResult Delete()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Delete(string roleName)
    {
      var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

      _context.Roles.Remove(thisRole);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult UsersAssigned()
    {
      var usersRoles = (from user in _context.Users
                        from userRole in user.Roles
                        join role in _context.Roles on userRole.RoleId equals
                        role.Id
                        select new RegisterViewModel
                        {
                          Email = user.UserName,
                          rolename = role.Name
                        })
                        .ToList();
      return View(usersRoles);
    }

    public ActionResult Roles()
    {
      var roles = _context.Roles.ToList();
      return View(roles);
    }

    public ActionResult Users()
    {
      var users = (from user in _context.Users
                   select new RegisterViewModel
                   {
                     Email = user.Email
                   })
                   .ToList();
      return View(users);
    }

    public ActionResult DashBoard()
    {
      return PartialView("DashBoard");
    }
  }
}