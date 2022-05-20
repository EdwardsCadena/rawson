using Microsoft.EntityFrameworkCore;

namespace Prueba.Models
{
    public class PruebaDbContext:DbContext
    {
        public PruebaDbContext(DbContextOptions<PruebaDbContext> options) : base(options)
        {
           
        }
        public DbSet<People> Peoples { get; set; }
        public DbSet<InfoPeople> infoPeoples { get; set; }
    }
}
