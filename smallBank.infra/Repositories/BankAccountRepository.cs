using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using smallBank.Domain.entities;
using smallBank.Infra.Context;
using smallBank.Service.Services.Interface;

namespace smallBank.Infra.Repositories
{
    public class BankAccountRepository : IBankAccountService
    {
        private readonly ApplicationDbContext _context;

        public BankAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> GetByIdAsync(Guid id)
        {
            return await _context.BankAccounts.FindAsync(id);
        }

        public async Task<BankAccount> CreateAsync(BankAccount bankAccount)
        {
            await _context.BankAccounts.AddAsync(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccount;
        }

        public async Task UpdateAsync(BankAccount bankAccount)
        {
            _context.BankAccounts.Update(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var account = await _context.BankAccounts.FindAsync(id);
            if (account != null)
            {
                _context.BankAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AccountExistsAsync(Guid id)
        {
            return await _context.BankAccounts.AnyAsync(a => a.Id == id);
        }
    }
}
