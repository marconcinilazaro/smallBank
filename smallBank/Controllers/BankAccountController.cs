using Microsoft.AspNetCore.Mvc;
using smallBank.Domain.entities;
using smallBank.DTO;
using smallBank.Service.Services.Interface;
namespace smallBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        // POST: api/BankAccount
        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccount accountDto)
        {
            if (accountDto == null || string.IsNullOrEmpty(accountDto.Name))
            {
                return BadRequest("Favor informar o nome da conta.");
            }

            var newAccount = await _bankAccountService.CreateAsync(accountDto);
            return CreatedAtAction(nameof(GetBankAccount), new { id = newAccount.Id }, newAccount);
        }

        // GET: api/BankAccount/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankAccount(Guid id)
        {
            var account = await _bankAccountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound("Conta não encontrada.");
            }

            return Ok(account);
        }
    }
}
