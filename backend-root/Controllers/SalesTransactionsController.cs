using backend_root.DTOs;
using backend_root.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend_root.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesTransactionsController : ControllerBase
    {
        private readonly ISalesTransactionService _salesTransactionService;
        public SalesTransactionsController(ISalesTransactionService salesTransactionService)
        {
            _salesTransactionService = salesTransactionService;
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesTransactions()
        {
            return Ok(await _salesTransactionService.GetAllAsync());
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesTransactionById(int id)
        {
            try
            {
                var salesTransaction = await _salesTransactionService.GetByIdAsync(id);
                return Ok(salesTransaction);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSalesTransaction([FromForm] SalesTransactionForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _salesTransactionService.AddAsync(form.TransactionDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesTransaction(int id, [FromBody] SalesTransactionDto salesTransactionDto)
        {
            if (id != salesTransactionDto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _salesTransactionService.UpdateAsync(id, salesTransactionDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesTransaction(int id)
        {
            try
            {
                await _salesTransactionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetSalesTransactionsByCustomerId(int userId)
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetByUserIdAsync(userId);
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByProductId/{productId}")]
        public async Task<IActionResult> GetSalesTransactionsByProductId(int productId)
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetByProductIdAsync(productId);
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByDateRange")]
        public async Task<IActionResult> GetSalesTransactionsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetByDateRangeAsync(startDate, endDate);
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByUserIdAndDateRange")]
        public async Task<IActionResult> GetSalesTransactionsByUserIdAndDateRange([FromQuery] int userId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByProductIdAndDateRange")]
        public async Task<IActionResult> GetSalesTransactionsByProductIdAndDateRange([FromQuery] int productId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetByProductIdAndDateRangeAsync(productId, startDate, endDate);
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByUserIdAndProductId")]
        public async Task<IActionResult> GetSalesTransactionsByUserIdAndProductId([FromQuery] int userId, [FromQuery] int productId)
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetByUserIdAndProductIdAsync(userId, productId);
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByUserIdAndProductIdAndDateRange")]
        public async Task<IActionResult> GetSalesTransactionsByUserIdAndProductIdAndDateRange([FromQuery] int userId, [FromQuery] int productId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetByUserIdAndProductIdAndDateRangeAsync(userId, productId, startDate, endDate);
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllGroupedByProduct")]
        public async Task<IActionResult> GetAllSalesTransactionsGroupedByProduct()
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetAllGroupedByProductAsync();
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllGroupedByRegion")]
        public async Task<IActionResult> GetAllSalesTransactionsGroupedByRegion()
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetAllGroupedByRegionAsync();
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllGroupedByRegionAndProduct")]
        public async Task<IActionResult> GetAllSalesTransactionsGroupedByRegionAndProduct()
        {
            try
            {
                var salesTransactions = await _salesTransactionService.GetAllGroupedByRegionAndProductAsync();
                return Ok(salesTransactions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
