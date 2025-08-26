using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeRate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly IExchangeRateAppService _exchangeRateAppService;
        private readonly ILogger<CurrencyExchangeController> _logger;
        private const string ExchangeRateDataIsNullMessage = "Exchange rate data is null.";

        public CurrencyExchangeController(ICurrencyAppService currencyAppService,
                                          IExchangeRateAppService exchangeRateAppService,
                                          ILogger<CurrencyExchangeController> logger)
        {
            _exchangeRateAppService = exchangeRateAppService;
            _logger = logger;
        }

        [HttpGet("rate/{baseCurrency}/{targetCurrency}")]
        public async Task<IActionResult> GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            try
            {
                _logger.LogInformation("Fetching exchange rate from {BaseCurrency} to {TargetCurrency}",
                                       baseCurrency,
                                       targetCurrency);

                var exchangeRate = await _exchangeRateAppService.GetExchangeRate(baseCurrency, targetCurrency);

                if (exchangeRate == null)
                {
                    _logger.LogWarning("Exchange rate from {BaseCurrency} to {TargetCurrency} not found",
                                       baseCurrency,
                                       targetCurrency);

                    return NotFound($"Exchange rate from {baseCurrency} to {targetCurrency} not found.");
                }

                return Ok(exchangeRate);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex,
                                 "An error occurred while fetching exchange rate from {BaseCurrency} to {TargetCurrency}",
                                 baseCurrency,
                                 targetCurrency);

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("rate")]
        public async Task<IActionResult> CreateExchangeRate([FromBody] ExchangeRateCreateDto exchangeRateCreateDto)
        {
            if (exchangeRateCreateDto == null)
            {
                return BadRequest(ExchangeRateDataIsNullMessage);
            }

            try
            {
                _logger.LogInformation("Creating exchange rate from {BaseCurrency} to {TargetCurrency}",
                                       exchangeRateCreateDto.BaseCurrencyCode,
                                       exchangeRateCreateDto.TargetCurrencyCode);

                var createdExchangeRate = await _exchangeRateAppService.CreateExchangeRate(exchangeRateCreateDto);

                if (createdExchangeRate == null)
                {
                    _logger.LogWarning("Attempted to create an existing exchange rate from {BaseCurrency} to {TargetCurrency}",
                                       exchangeRateCreateDto.BaseCurrencyCode,
                                       exchangeRateCreateDto.TargetCurrencyCode);

                    return Conflict("This exchange rate already exists.");
                }

                _logger.LogInformation("Exchange rate from {BaseCurrency} to {TargetCurrency} created successfully",
                                       createdExchangeRate.BaseCurrencyCode,
                                       createdExchangeRate.TargetCurrencyCode);

                return CreatedAtAction(nameof(GetExchangeRate),
                                       new
                                       {
                                           baseCurrency = createdExchangeRate.BaseCurrencyCode,
                                           targetCurrency = createdExchangeRate.TargetCurrencyCode
                                       }, createdExchangeRate);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex, "An error occurred while creating exchange rate from {BaseCurrency} to {TargetCurrency}",
                                 exchangeRateCreateDto.BaseCurrencyCode,
                                 exchangeRateCreateDto.TargetCurrencyCode);

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("rate/{baseCurrency}/{targetCurrency}")]
        public async Task<IActionResult> UpdateExchangeRate(string baseCurrency,
                                                            string targetCurrency,
                                                            [FromBody] ExchangeRateUpdateDto exchangeRateUpdateDto)
        {
            if (exchangeRateUpdateDto == null)
            {
                return BadRequest(ExchangeRateDataIsNullMessage);
            }

            try
            {
                _logger.LogInformation("Updating exchange rate from {BaseCurrency} to {TargetCurrency}",
                                       baseCurrency,
                                       targetCurrency);


                var updatedExchangeRate = await _exchangeRateAppService.UpdateExchangeRate(baseCurrency,
                                                                                           targetCurrency,
                                                                                           exchangeRateUpdateDto);

                if (updatedExchangeRate == null)
                {
                    _logger.LogWarning("Exchange rate from {BaseCurrency} to {TargetCurrency} not found",
                                       baseCurrency,
                                       targetCurrency);

                    return NotFound($"Exchange rate from {baseCurrency} to {targetCurrency} not found.");
                }

                _logger.LogInformation("Exchange rate from {BaseCurrency} to {TargetCurrency} updated successfully",
                                       updatedExchangeRate.BaseCurrencyCode,
                                       updatedExchangeRate.TargetCurrencyCode);

                return Ok(updatedExchangeRate);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex, "An error occurred while updating exchange rate from {BaseCurrency} to {TargetCurrency}",
                                 baseCurrency,
                                 targetCurrency);

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}