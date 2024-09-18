namespace smallBank.DTO
{
    public class CreateBankAccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}