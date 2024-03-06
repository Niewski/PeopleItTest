using Microsoft.EntityFrameworkCore;
using PeopleItTest.Models;

namespace PeopleItTest.Data
{
    public class MockCrmDbContext : DbContext
    {
        public MockCrmDbContext(DbContextOptions<MockCrmDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectQuote> ProjectQuotes { get; set; }
    }
}
