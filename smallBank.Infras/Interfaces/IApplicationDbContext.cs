using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using smallBank.Domain.entities;

namespace smallBank.Infra.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<BankAccount> BankAccounts { get; set; }
        DbSet<BankLaunch> BankLaunches { get; set; }
        Guid SaveChanges();
        Task SaveChangesAsync();
    }
}
