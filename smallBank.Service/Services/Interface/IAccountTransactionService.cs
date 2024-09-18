using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smallBank.DTO;

namespace smallBank.Service.Services.Interface
{
    public interface IAccountTransactionService
    {
        Task<bool> CreditAccountAsync(CreditDto creditDto);
        Task<bool> DebitAccountAsync(DebitDto debitDto);
        Task<bool> TransferAsync(TransferDto transferDto);
    }
}
