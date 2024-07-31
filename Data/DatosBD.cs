using Microsoft.EntityFrameworkCore;
using WEB_API_MIN.Models;

namespace WEB_API_MIN.Data
{
    public class DatosBD : DbContext
    {
        public DatosBD(DbContextOptions<DatosBD> options) : base(options)       
        {
            
        }
        public DbSet<Informacion> Informaciones => Set<Informacion>();
    }
}
