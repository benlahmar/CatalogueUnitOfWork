using Catalogue.Core.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalogue.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Categorie> categories { get; set; }
        public DbSet<Produit> produits { get; set; }
    }
}
