using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Models;

namespace TodoWebAPI.Data
{
    public class ToDoListApiDbContext : DbContext
    {
        public ToDoListApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDoAPI> ToDoAPIs { get; set; }
    }
}
