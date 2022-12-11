using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RpgGame.Models;

namespace RpgGame.Data
{
    public class RpgContext: IdentityDbContext<Account>
    {
       public RpgContext(DbContextOptions options) : base(options){ }


        public DbSet<Player> players { get; set; }
        public DbSet<Classes> classes { get; set; }
        public DbSet<Items> items { get; set; }
        public DbSet<Account> playerAccounts { get; set; }

      

    }
}
