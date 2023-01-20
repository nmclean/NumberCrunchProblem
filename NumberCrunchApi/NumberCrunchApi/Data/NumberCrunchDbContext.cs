using Microsoft.EntityFrameworkCore;

namespace NumberCrunchApi.Data {
    public class NumberCrunchDbContext : DbContext {
        public DbSet<Sample> Samples { get; set; }
        public DbSet<SampleGroup> SampleGroups { get; set; }

        public NumberCrunchDbContext(DbContextOptions<NumberCrunchDbContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
