using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UProastery.Contracts;
using UProastery.Data;
using UProastery.Data.DB;
using UProastery.Data.User;

namespace UProastery.Helpers {


    public class OrderManager : IOrderManager {

        private readonly UP_context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAcc;
        private readonly UserManager<ApiUser> _userManager;

        public OrderManager(UP_context context, IMapper mapper, IHttpContextAccessor http_context, UserManager<ApiUser> user_manager) {
            this._context = context;
            this._mapper = mapper;
            this._httpContextAcc = http_context;
            this._userManager = user_manager;
        }
        /*NOTE: stock updating functionality currently carried out by SQL triggers (inconvinient) */
        public async Task<int> AddOrderItems(List<OrderItemDTO> dto_in) {

            //check for invalid items
            for(int i = dto_in.Count - 1; i >= 0; i--) {
                Item? item = await _context.Set<Item>().FindAsync(dto_in[i].ItemId);
                if (item == null) {
                    dto_in.Remove(dto_in[i]);
                    continue;
                }
                dto_in[i].Item = item;
            }
            //create order
            if (dto_in.Count > 0) {
                List<OrderItem> orderItems = _mapper.Map<List<OrderItem>>(dto_in);
                var username = _httpContextAcc.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                
                Order order = new Order {
                    Date = DateTime.Now,
                    TotalPrice = orderItems.Sum(i => i.Item.Price * i.Quantity),
                    User = await _userManager.FindByNameAsync(username),
                    OrderItems = orderItems
                };
                await _context.Set<Order>().AddAsync(order);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<OrderDTO_OUT> GetOrder(int id) {

            Order? order = await _context.Set<Order>().Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) {
                return null;
            }
            var dto_out = _mapper.Map<OrderDTO_OUT>(order);
            return dto_out;
        }

        public async Task<int> DeleteOrder(int id) {
            Order? order = await _context.Set<Order>().Include(o => o.OrderItems).ThenInclude(oi => oi.Item).FirstOrDefaultAsync(o => o.Id == id);
            if(order == null) {
                return -1;
            }
            //update stock before removing order
            foreach(OrderItem entry in order.OrderItems) {
                entry.Item.Stock += entry.Quantity;
            }
            _context.Set<Order>().Remove(order);
            return await _context.SaveChangesAsync();
        }

        public Task<List<OrderDTO_OUT>> GetOrders() {
            throw new NotImplementedException();
        }
    }
}
