
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticaLogin.Models;

namespace PracticaLogin.DTContext
{
    public class ContextDb:IdentityDbContext
    {
        public ContextDb() { }
        public ContextDb(DbContextOptions<ContextDb>options):base(options) { }


        //Modelos

        public DbSet<Compania>Companias { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
    }
}
