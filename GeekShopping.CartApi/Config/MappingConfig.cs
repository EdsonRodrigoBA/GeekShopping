using AutoMapper;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Model;

namespace GeekShopping.CartApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisteMappers()
        {
            var mapperConfig = new MapperConfiguration(Config =>
            {
                Config.CreateMap<ProductVO, Product>().ReverseMap();
                Config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                Config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                Config.CreateMap<CartVO, Cart>().ReverseMap();



                //Config.CreateMap<Product, ProductVO>();

            });
            return mapperConfig;
        }
    }
}
