using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetCustomerWithOrdersAsync(int id);
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}