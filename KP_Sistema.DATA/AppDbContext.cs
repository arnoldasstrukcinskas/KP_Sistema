using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KP_Sistema.DATA
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
    }
}
