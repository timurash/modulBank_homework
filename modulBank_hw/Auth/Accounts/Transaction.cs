namespace Accounts
{
    public class Transaction
    {
        public int Id { get; set; }

        public int TransactionType { get; set; }

        public decimal Value { get; set; }

        public string FromAccount { get; set; }

        public string ToAccount { get; set; }

        public int UserId { get; set; }

        public Transaction()
        { }
    }
}