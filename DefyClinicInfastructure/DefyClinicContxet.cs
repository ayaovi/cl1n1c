using DefyClinicModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
  public class ApplicationUser : IdentityUser
  {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
    public string FullName { get; set; }
  }
  public class DefyClinicContxet : IdentityDbContext<ApplicationUser>
  {
    public DefyClinicContxet() : base("MyConnetion", false) {}
    public static DefyClinicContxet Create()
    {
      return new DefyClinicContxet();
    }
    public DbSet<PreSlot> Slots { get; set; }
    public DbSet<Branch> Branchs { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Visit> Visits { get; set; }
  }
}