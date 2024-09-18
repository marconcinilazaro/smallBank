using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smallBank.Domain.entities;
using smallBank.DTO;

namespace smallBank.Service.Services.Interface
{
    public interface IBankAccountService
    {
        Task<BankAccount> GetByIdAsync(Guid id);
        Task UpdateAsync(BankAccount bankAccount);
        Task DeleteAsync(Guid id);
        Task<bool> AccountExistsAsync(Guid id);
        Task<BankAccount> CreateAsync(BankAccount bankAccount);
    }
}
