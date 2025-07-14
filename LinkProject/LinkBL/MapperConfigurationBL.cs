using AutoMapper;
using LinkBL.ModelBL.TableWithUrlBL.Dto;
using LinkDL.Models;

namespace LinkBL
{
    public class MapperConfigurationBL : Profile
    {
        public MapperConfigurationBL()
        {
            CreateMap<TableWithUrl, OutTableWithUrlDtoBL>().ForMember(x => x.CreatedAt, y =>
                y.MapFrom(z => z.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")));
            CreateMap<TableWithUrl, OutMagnifyCountOfTransitionsDtoBL>();
        }
    }
}
