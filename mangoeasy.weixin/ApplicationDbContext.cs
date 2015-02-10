using System.Data.Entity;
using mangoeasy.weixin.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mangoeasy.weixin
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        public IDbSet<Customer> Customers { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}