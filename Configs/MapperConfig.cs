using AutoMapper;
using UProastery.Data;
using UProastery.Data.DB;
using UProastery.Data.User;

namespace UProastery.Configs {
    
    //create the necessary mappings (also formally called Profile Assemblies)
    public class MapperConfig : Profile {
        
        public MapperConfig()
        {
            CreateMap<DateOnly, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            CreateMap<ApiUser, ApiUserDTO>().ReverseMap();
            CreateMap<ApiUser, LoginDTO>().ReverseMap();

            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO_OUT>().ReverseMap();
            CreateMap<Order, OrderDTO_OUT>().ReverseMap();
        }
    }

    //DateTime to DateOnly converter
    public class DateTimeTypeConverter : ITypeConverter<DateOnly, DateTime> {
        
        public DateTime Convert(DateOnly source, DateTime destination, ResolutionContext context) {
            return source.ToDateTime(TimeOnly.MinValue);
        }
    }
}
