using UProastery.Data;

namespace UProastery.Contracts {

    public interface IOrderManager {
        public Task<OrderDTO_OUT> GetOrder(int id);
        public Task<List<OrderDTO_OUT>> GetOrders();
        public Task<int> AddOrderItems(List<OrderItemDTO> dto_in);
        public Task<int> DeleteOrder(int id);
    }
}
