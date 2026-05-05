using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            var orders = await _orderRepository.GetOrdersWithCustomerAsync();
            var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return ServiceResult<IEnumerable<OrderDto>>.Success(dtos);
        }

        public async Task<ServiceResult<OrderDto>> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderWithDetailsAsync(id);

            if (order == null)
                return ServiceResult<OrderDto>.Failure("Order not found");

            var dto = _mapper.Map<OrderDto>(order);
            return ServiceResult<OrderDto>.Success(dto);
        }

        public async Task<ServiceResult<OrderDto>> CreateAsync(CreateOrderDto dto)
        {
            // cek customer
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
            if (customer == null)
                return ServiceResult<OrderDto>.Failure("Customer not found");

            decimal total = 0;
            var orderDetails = new List<OrderDetail>();

            // loop tiap item
            foreach (var item in dto.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    return ServiceResult<OrderDto>.Failure($"Product {item.ProductId} not found");

                //  cek stock
                if (product.Stock < item.Quantity)
                    return ServiceResult<OrderDto>.Failure($"Stock not enough for {product.Name}");

                //  hitung subtotal
                var subtotal = product.Price * item.Quantity;
                total += subtotal;

                // 5buat order detail
                var detail = new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                orderDetails.Add(detail);

                //  kurangi stock
                product.Stock -= item.Quantity;
                _productRepository.Update(product);
            }

            //  buat order
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = total,
                OrderDetails = orderDetails
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            // 8. ambil ulang dengan include
            var createdOrder = await _orderRepository.GetOrderWithDetailsAsync(order.Id);

            var resultDto = _mapper.Map<OrderDto>(createdOrder);

            return ServiceResult<OrderDto>.Success(
                resultDto,
                "Order created successfully"
            );
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return ServiceResult<bool>.Failure("Order not found");

            _orderRepository.Delete(order);
            await _orderRepository.SaveChangesAsync();

            return ServiceResult<bool>.Success(true, "Order deleted");
        }
    }
}