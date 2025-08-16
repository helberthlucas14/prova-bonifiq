using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTestFixture : CustomerTestFixtureBase
    {
    }

    [CollectionDefinition(nameof(CustomerServiceTestFixture))]
    public class CustomerServiceTestFixtureCollection : ICollectionFixture<CustomerServiceTestFixture>
    {
    }

}
