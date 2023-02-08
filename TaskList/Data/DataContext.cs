using Microsoft.EntityFrameworkCore;
using TaskList.Models;

namespace TaskList.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }
        
        public DbSet<ToDo> taskLists { get; set; }
    }
}
