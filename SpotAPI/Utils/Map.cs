//using AutoMapper;

using Newtonsoft.Json;

namespace SpotAPI.Utils
{
    internal sealed class Map
    {
        //private static MapperConfiguration _config;
        //private static IMapper _mapper;

        //public static void Initialize()
        //{
            //_config = new MapperConfiguration(e => Configure(e));
            //_mapper = _config.CreateMapper();
        //}

        //private static void Configure(IMapperConfigurationExpression ex)
        //{
        //    //ex.CreateMap<SpotifyAlbumModel, ExpandoObject>().ReverseMap();

        //    //ex.AddMaps(Assembly.GetExecutingAssembly());
        //}

        public static T ConvertTo<T>(object data)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));
            //return _mapper.Map<T>(data);
        }

        //public T Convert<T>(object data)
        //{
        //    return ConvertTo<T>(data);
        //}
    }
}
