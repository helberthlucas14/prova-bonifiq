using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;
using Xunit;

namespace ProvaPub.Tests
{

    [CollectionDefinition(nameof(CustomerRepositoryTestsFixture))]
    public class CustomerRepositoryTestFixtureCollection : ICollectionFixture<CustomerRepositoryTestsFixture> { }

    public class CustomerRepositoryTestsFixture : CustomerTestFixtureBase
    {
        public CustomerRepositoryTestsFixture() : base()
        {
        }
        public TestDbContext CreateDbContext(bool preserveData = false)
        {

            var context = new TestDbContext
                      (
                          new DbContextOptionsBuilder<TestDbContext>()
                              .UseInMemoryDatabase("service-testes-db")
                              .Options
                      );
            if (!preserveData)
                context.Database.EnsureDeleted();
            return context;
        }
    }
}
