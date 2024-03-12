using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Boxfusion.LMS_Backend.Authorization.Roles;
using Boxfusion.LMS_Backend.Authorization.Users;
using Boxfusion.LMS_Backend.MultiTenancy;
using Boxfusion.LMS_Backend.Domain;

namespace Boxfusion.LMS_Backend.EntityFrameworkCore
{
    public class LMS_BackendDbContext : AbpZeroDbContext<Tenant, Role, User, LMS_BackendDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        
        public LMS_BackendDbContext(DbContextOptions<LMS_BackendDbContext> options)
            : base(options)
        {
        }
    }
}
