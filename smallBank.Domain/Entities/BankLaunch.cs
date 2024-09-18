using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smallBank.Domain.entities
{
    public class BankLaunch
    {
        public Guid Id { get; set; }
        public Guid OriginBankAccountId { get; set; }
        public decimal Value { get; set; }
        public OperationType OperationType { get; set; }
        public Guid? DestinationBankAccountId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

  
        public BankAccount OriginBankAccount { get; set; }
        public BankAccount DestinationBankAccount { get; set; }
    }

    public enum OperationType
    {
        Credit,
        Debit,
        Transfer
    }
}
