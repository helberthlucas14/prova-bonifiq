using ProvaPub.Dtos;
using ProvaPub.Exceptions;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;
using ProvaPub.Validators.DomainValidations;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;
        public CustomerService(
            ICustomerRepository repository,
            IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<PagedList<Customer>> PaginedListAsync(CancellationToken cancellationToken, QueryStringParameters parameters)
        {
            var pagedList = await _repository.GetPagedListAsync(parameters, cancellationToken);
            return pagedList;
        }

        public async Task<bool> CanPurchaseAsync(int customerId, decimal purchaseValue, CancellationToken cancellation)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _repository.GetByIdCustomerWithOrder(customerId, cancellation);

            var validationHandler = new NotificationValidationHandler();

            customer.CanPurchase(validationHandler, purchaseValue, _dateTimeProvider.UtcNow);

            if (validationHandler.HasErrors())
            {
                throw new EntityValidationException("There are validation errors", validationHandler.Errors);
            }
            return true;
        }
    }
}
