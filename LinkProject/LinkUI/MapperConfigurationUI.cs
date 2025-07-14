using AutoMapper;
using LinkBL.ModelBL.TableWithUrlBL.Dto;
using LinkUI.Models.TableWithUrlUI.Dto;

namespace LinkUI
{
    public class MapperConfigurationUI : Profile
    {
        public MapperConfigurationUI() 
        {
            CreateMap<OutTableWithUrlDtoBL, OutTableWithUrlDtoUI>();
            CreateMap<OutMagnifyCountOfTransitionsDtoBL, OutMagnifyCountOfTransitionsDtoUI>();
        }
    }
}
