using AutoMapper;

using GeekShopping.CupomApi.Data.ValueObject;
using GeekShopping.CupomApi.Model;

namespace GeekShopping.CupomApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisteMappers()
        {
            var mapperConfig = new MapperConfiguration(Config =>
            {
                //Config.CreateMap<ProductVO, Product>().ReverseMap();
                //Config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                //Config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                //Config.CreateMap<CartVO, Cart>().ReverseMap();



                Config.CreateMap<Coupon, CouponVO>().ReverseMap();

            });
            return mapperConfig;
        }
    }
}
