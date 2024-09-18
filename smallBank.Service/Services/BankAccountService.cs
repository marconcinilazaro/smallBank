using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using smallBank.Domain.entities;
using smallBank.DTO;
using smallBank.Infra.Repositories;
using smallBank.Service.Services.Interface;
using static smallBank.Service.Services.Interface.IBankAccountService;

namespace smallBank.Service.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountService _repository;
        private readonly IMapper _mapper;

        public BankAccountService(IBankAccountService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<BankAccount> CreateAsync(BankAccount accountDto)
        {
    
            var bankAccounts = new BankAccount
            {
                Id = new Guid(),
                Name = accountDto.Name,
                Balance = accountDto.Balance,
                CreateDate = DateTime.UtcNow
            };

            var createdAccount = await _repository.CreateAsync(bankAccounts);
            
            return bankAccounts;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BankAccountDto> GetBankAccountAsync(Guid id)
        {
            var bankAccount = await _repository.GetByIdAsync(id);
            if (bankAccount == null) return null;

            return new BankAccountDto
            {
                Id = bankAccount.Id,
                Name = bankAccount.Name,
                Balance = bankAccount.Balance
            };
        }

   
        Task<bool> IBankAccountService.AccountExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        Task IBankAccountService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<BankAccount> IBankAccountService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IBankAccountService.UpdateAsync(BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }
    }
}
