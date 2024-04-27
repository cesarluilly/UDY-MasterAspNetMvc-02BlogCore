using BlogCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Categoria> Categoria { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
