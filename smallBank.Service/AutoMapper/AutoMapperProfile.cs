using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using smallBank.Domain.entities;
using smallBank.DTO;

namespace smallBank.Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateBankAccountDto, BankAccount>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore());

            CreateMap<BankAccount, BankAccountDto>();
        }
    }
}
