using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Boxfusion.LMS_Backend.Authorization.Roles;
using Boxfusion.LMS_Backend.Authorization.Users;
using Boxfusion.LMS_Backend.MultiTenancy;

namespace Boxfusion.LMS_Backend.EntityFrameworkCore
{
    public class LMS_BackendDbContext : AbpZeroDbContext<Tenant, Role, User, LMS_BackendDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public LMS_BackendDbContext(DbContextOptions<LMS_BackendDbContext> options)
            : base(options)
        {
        }
    }
}
