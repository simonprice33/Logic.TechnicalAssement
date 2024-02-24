using AutoMapper;
using Logic.TechnicalAssement.Core.Entities;
using Logic.TechnicalAssement.Core.Models;

namespace Logic.TechnicalAssement.Core.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<Leave, LeaveViewModel>();
        }
    }
}
