using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return ServiceResult<IEnumerable<CustomerDto>>.Success(dtos);
        }

        public async Task<ServiceResult<CustomerDto>> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return ServiceResult<CustomerDto>.Failure("Customer not found");

            var dto = _mapper.Map<CustomerDto>(customer);
            return ServiceResult<CustomerDto>.Success(dto);
        }

        public async Task<ServiceResult<CustomerDto>> CreateAsync(CreateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return ServiceResult<CustomerDto>.Failure("Customer name is required");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return ServiceResult<CustomerDto>.Failure("Customer email is required");

            var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(dto.Email);

            if (existingCustomer != null)
                return ServiceResult<CustomerDto>.Failure("Email already exists");

            var customer = _mapper.Map<Customer>(dto);

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();

            var resultDto = _mapper.Map<CustomerDto>(customer);

            return ServiceResult<CustomerDto>.Success(
                resultDto,
                "Customer created successfully"
            );
        }

        public async Task<ServiceResult<CustomerDto>> UpdateAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return ServiceResult<CustomerDto>.Failure("Customer not found");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return ServiceResult<CustomerDto>.Failure("Customer name is required");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return ServiceResult<CustomerDto>.Failure("Customer email is required");

            var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(dto.Email);

            if (existingCustomer != null && existingCustomer.Id != id)
                return ServiceResult<CustomerDto>.Failure("Email already exists");

            _mapper.Map(dto, customer);

            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();

            var resultDto = _mapper.Map<CustomerDto>(customer);

            return ServiceResult<CustomerDto>.Success(
                resultDto,
                "Customer updated successfully"
            );
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return ServiceResult<bool>.Failure("Customer not found");

            _customerRepository.Delete(customer);
            await _customerRepository.SaveChangesAsync();

            return ServiceResult<bool>.Success(
                true,
                "Customer deleted successfully"
            );
        }
    }
}