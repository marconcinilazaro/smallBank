using Microsoft.AspNetCore.Mvc;
using smallBank.DTO;
using smallBank.Service.Services.Interface;

namespace smallBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountTransactionController : ControllerBase
    {
        private readonly IAccountTransactionService _accountTransactionService;

        public AccountTransactionController(IAccountTransactionService accountTransactionService)
        {
            _accountTransactionService = accountTransactionService;
        }

        [HttpPost("credit")]
        public async Task<IActionResult> CreditAccount([FromBody] CreditDto creditDto)
        {
            if (creditDto == null || creditDto.Amount <= 0)
            {
                return BadRequest("Operação inválida.");
            }

            var result = await _accountTransactionService.CreditAccountAsync(creditDto);
            if (!result)
            {
                return NotFound("Conta não encontrada.");
            }

            return Ok("Operação concluída.");
        }

        [HttpPost("debit")]
        public async Task<IActionResult> DebitAccount([FromBody] DebitDto debitDto)
        {
            if (debitDto == null || debitDto.Amount <= 0)
            {
                return BadRequest("Debito invalido.");
            }

            var result = await _accountTransactionService.DebitAccountAsync(debitDto);
            if (!result)
            {
                return NotFound("Você não possui fundos suficientes.");
            }

            return Ok("Operação concluida.");
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transferDto)
        {
            if (transferDto == null || transferDto.Amount <= 0)
            {
                return BadRequest("Operação invalida.");
            }

            var result = await _accountTransactionService.TransferAsync(transferDto);
            if (!result)
            {
                return NotFound("Você não possui fundos suficientes.");
            }

            return Ok("Operação concluida.");
        }
    }
}
