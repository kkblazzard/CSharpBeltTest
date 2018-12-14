using Microsoft.EntityFrameworkCore;
 
namespace CSharpBeltTest.Models
{
    public class BeltContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltContext(DbContextOptions<BeltContext> options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Activity> Activity {get;set;}
        public DbSet<Participant> participate{get;set;}
    }
}
