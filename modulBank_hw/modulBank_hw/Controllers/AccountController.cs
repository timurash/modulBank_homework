using System;
using Auth;
using Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace modulBank_hw.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repo;

        public AccountController(IAccountRepository r)
        {
            repo = r;
        }

        [Authorize]
        [HttpPost("api/createAccount")]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            repo.CreateAccount(account);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/putMoney")]
        public IActionResult PutMoney([FromBody] Transaction transaction)
        {
            repo.PutMoney(transaction);
            return Ok();
        }

        [Authorize]
        [HttpGet("api/getAccounts")]
        public IActionResult GetAccounts()
        {
            return Ok(new { List = repo.GetAccounts() });
        }

        [Authorize]
        [HttpPost("api/moneyTransfer")]
        public IActionResult MoneyTransfer([FromBody] Transaction transaction)
        {
            repo.MoneyTransfer(transaction);
            return Ok();
        }

        [Authorize]
        [HttpGet("api/getTransactions")]
        public IActionResult GetTransactions()
        {
            return Ok(new { List = repo.GetTransactions() });
        }
    } 
}
