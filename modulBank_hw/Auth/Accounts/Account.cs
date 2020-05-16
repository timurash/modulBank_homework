namespace Accounts
{
    public class Account
    {
        public string AccountNumber { get; set; }

        public int OwnerId { get; set; }

        public decimal Balance { get; set; }

        public string AccountName { get; set; }

        public Account()
        {
        }
    }
}