using Lw.Data.Entity;
using Microsoft.OpenApi.Models;
using smallBank.Domain.entities;
using smallBank.DTO;
using smallBank.Service.Services.Interface;
using smallBank.Infra.Interfaces;

namespace smallBank.Service.Services
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly IApplicationDbContext _context;

        public AccountTransactionService(IDbContext context) => _context = (IApplicationDbContext?)context;

        public async Task<bool> CreditAccountAsync(CreditDto creditDto)
        {
            var account = await _context.BankAccounts.FindAsync(creditDto.AccountId);
            if (account == null) return false;

            account.Balance += creditDto.Amount;
            _context.BankLaunches.Add(new BankLaunch
            {
                OriginBankAccountId = creditDto.AccountId,
                Value = creditDto.Amount,
                OperationType = Domain.entities.OperationType.Credit,
                CreateDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DebitAccountAsync(DebitDto debitDto)
        {
            var account = await _context.BankAccounts.FindAsync(debitDto.AccountId);
            if (account == null || account.Balance < debitDto.Amount) return false;

            account.Balance -= debitDto.Amount;
            _context.BankLaunches.Add(new BankLaunch
            {
                OriginBankAccountId = debitDto.AccountId,
                Value = debitDto.Amount,
                OperationType = Domain.entities.OperationType.Debit,
                CreateDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransferAsync(TransferDto transferDto)
        {
            var originAccount = await _context.BankAccounts.FindAsync(transferDto.OriginAccountId);
            var destinationAccount = await _context.BankAccounts.FindAsync(transferDto.DestinationAccountId);

            if (originAccount == null || destinationAccount == null || originAccount.Balance < transferDto.Amount) return false;

            originAccount.Balance -= transferDto.Amount;
            destinationAccount.Balance += transferDto.Amount;

            _context.BankLaunches.Add(new BankLaunch
            {
                OriginBankAccountId = transferDto.OriginAccountId,
                DestinationBankAccountId = transferDto.DestinationAccountId,
                Value = transferDto.Amount,
                OperationType = Domain.entities.OperationType.Transfer,
                CreateDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
