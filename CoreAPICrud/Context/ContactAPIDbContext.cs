using CoreAPICrud.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreAPICrud.Context
{
    public class ContactAPIDbContext:DbContext
    {
        public ContactAPIDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
