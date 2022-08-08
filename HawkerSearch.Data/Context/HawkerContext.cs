using Microsoft.EntityFrameworkCore;

namespace HawkerSearch.Domain
{
    public class HawkerContext : DbContext
    {
        public HawkerContext(DbContextOptions<HawkerContext> options) : base(options)
        {
        }

        public DbSet<Hawker> Hawkers { get; set; }
    }
}
