using Microsoft.EntityFrameworkCore;
 
namespace BrightIdeas.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> Users{ get; set; } //reviewer = the table name
        //<Reviewer> is the class model that will link to the database
        public DbSet<Idea> Ideas{ get; set; }
        public DbSet<Like> Likes{ get; set; }



    }
}