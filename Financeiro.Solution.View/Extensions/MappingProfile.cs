using AutoMapper;
using Financeiro.Solution.Infra.Data.Migrations.Entities;
using Financeiro.Solution.View.DTO.Login;
using FinanceiroSolution.Domain.Entidades;

namespace Financeiro.Solution.View.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          

            CreateMap<UserForRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
