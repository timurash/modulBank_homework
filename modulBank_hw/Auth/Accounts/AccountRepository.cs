using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace Accounts
{
    public interface IAccountRepository
    {
        void CreateAccount(Account account);
        void PutMoney(Transaction transaction);
        List<Account> GetAccounts();
        void MoneyTransfer(Transaction transaction);
        List<Transaction> GetTransactions();
    }

    public class AccountRepository : IAccountRepository
    {
        string connectionString = "";

        /*
        public AccountRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }
        */

        internal IDbConnection Connection
        {
            get { return new NpgsqlConnection(connectionString); }
        }

        public AccountRepository(string conn)
        {
            connectionString = conn;
        }

        public void CreateAccount(Account account)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuerry = "INSERT INTO accounts(accountnumber, ownerid, balance, accountname) VALUES (nextval('accountnumberseq'), @OwnerId, @Balance, @AccountName)";
                db.Execute(sqlQuerry, account);
            }
        }

        public List<Account> GetAccounts()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Account>("SELECT * FROM accounts ORDER BY accountnumber").ToList();
            }
        }

        public void PutMoney(Transaction transaction)
        {
            using (IDbConnection db = Connection)
            {
                decimal currentBalance = db.Query<decimal>("SELECT balance FROM accounts WHERE accountnumber = @toAccount", new { transaction.ToAccount }).FirstOrDefault();
                decimal balanceAfterTransaction = currentBalance + transaction.Value;
                var sqlQuerry = "UPDATE accounts SET balance = @balanceAfterTransaction WHERE accountnumber = @toAccount";
                db.Execute(sqlQuerry, new
                {
                    balanceAfterTransaction,
                    transaction.ToAccount
                });

                //записываем транзакцию в историю 
                var transactionSqlQuerry = "INSERT INTO transactions(transactiontype, value, fromaccount, toaccount, userid) VALUES (@TransactionType, @Value, @FromAccount, @ToAccount, @UserId)";
                db.Execute(transactionSqlQuerry, transaction);
            }
        }

        public void MoneyTransfer(Transaction transaction)
        {
            using (IDbConnection db = Connection)
            {
                decimal currentBalanceFromAccount = db.Query<decimal>("SELECT balance FROM accounts WHERE accountnumber = @fromAccount", new { transaction.FromAccount }).FirstOrDefault();
                decimal currentBalanceToAccount = db.Query<decimal>("SELECT balance FROM accounts WHERE accountnumber = @toAccount", new { transaction.ToAccount }).FirstOrDefault();

                //вычитаем баланс исходного счета
                var sqlQuery1 = "UPDATE accounts SET balance = @balanceAfterTransaction WHERE accountnumber = @fromAccount";

                db.Execute(sqlQuery1, new
                {
                    balanceAfterTransaction = currentBalanceFromAccount - transaction.Value,
                    transaction.FromAccount
                });

                //добавляем на баланс необходимого счета
                var sqlQuery2 = "UPDATE accounts SET balance = @balanceAfterTransaction WHERE accountnumber = @toAccount";

                db.Execute(sqlQuery2, new
                {
                    balanceAfterTransaction = currentBalanceToAccount + transaction.Value,
                    transaction.ToAccount
                });

                //записываем транзакцию в историю
                var transactionSqlQuerry = "INSERT INTO transactions(transactiontype, value, fromaccount, toaccount, userid) VALUES (@TransactionType, @Value, @FromAccount, @ToAccount, @UserId)";
                db.Execute(transactionSqlQuerry, transaction);
            }
        }

        public List<Transaction> GetTransactions()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Transaction>("SELECT * FROM transactions ORDER BY id").ToList();
            }
        }
    }
}

