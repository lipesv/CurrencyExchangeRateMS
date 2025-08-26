using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeRate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyAppService _currencyService;
        private readonly ILogger<CurrencyController> _logger;

        private const string InternalServerErrorMessage = "Internal server error: ";

        public CurrencyController(ICurrencyAppService currencyService,
                                  ILogger<CurrencyController> logger)
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            try
            {
                _logger.LogInformation("Fetching all currencies");

                var currencies = await _currencyService.GetAllCurrencies();

                return Ok(currencies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all currencies");
                return StatusCode(500, InternalServerErrorMessage + ex.Message);
            }
        }


        [HttpGet("{currencyCode}")]
        public async Task<IActionResult> Get(string currencyCode)
        {
            try
            {
                _logger.LogInformation("Fetching currency with code: {CurrencyCode}",
                                       currencyCode);

                var currency = await _currencyService.GetCurrencyByCode(currencyCode);

                if (currency == null)
                {
                    _logger.LogWarning("Currency with code {CurrencyCode} not found", currencyCode);
                    return NotFound($"Currency with code {currencyCode} not found.");
                }

                return Ok(currency);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex,
                                 "An error occurred while fetching currency with code: {CurrencyCode}",
                                 currencyCode);

                return StatusCode(500, InternalServerErrorMessage + ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CurrencyCreateDto currencyCreateDto)
        {
            if (currencyCreateDto == null)
            {
                return BadRequest("Currency data is null.");
            }

            try
            {
                _logger.LogInformation("Creating currency with code: {CurrencyCode}",
                                       currencyCreateDto.CurrencyCode);

                var createdCurrency = await _currencyService.CreateCurrency(currencyCreateDto);

                if (createdCurrency == null)
                {
                    _logger.LogWarning("Attempted to create an existing currency with code: {CurrencyCode}",
                                       currencyCreateDto.CurrencyCode);

                    // Retorna 409 Conflict, indicando que o recurso já existe.
                    return Conflict($"Currency with code '{currencyCreateDto.CurrencyCode}' already exists.");
                }

                _logger.LogInformation("Currency with code {CurrencyCode} created successfully",
                                       createdCurrency.CurrencyCode);

                return CreatedAtAction(nameof(Get), new { currencyCode = createdCurrency.CurrencyCode }, createdCurrency);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex, "An error occurred while creating currency with code: {CurrencyCode}",
                                 currencyCreateDto.CurrencyCode);

                return StatusCode(500, InternalServerErrorMessage + ex.Message);
            }
        }

        // PUT: api/Currency/BRL
        [HttpPut("{currencyCode}")]
        public async Task<IActionResult> Update(string currencyCode, [FromBody] CurrencyUpdateDto currencyUpdateDto)
        {
            if (currencyUpdateDto == null)
            {
                return BadRequest("Currency data is null.");
            }

            try
            {
                var updatedCurrency = await _currencyService.UpdateCurrency(currencyCode, currencyUpdateDto);

                if (updatedCurrency == null)
                {
                    _logger.LogWarning("Currency with code {CurrencyCode} not found for update", currencyCode);
                    return NotFound($"Currency with code {currencyCode} not found.");
                }

                return Ok(updatedCurrency);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating currency with code: {CurrencyCode}", currencyCode);
                return StatusCode(500, InternalServerErrorMessage + ex.Message);
            }
        }

        [HttpPost("CreateMultiple")]
        public async Task<IActionResult> CreateCurrencies([FromBody] List<CurrencyCreateDto> currencyCreateDtos)
        {
            if (currencyCreateDtos == null || !currencyCreateDtos.Any())
            {
                return BadRequest("Currency data list is null or empty.");
            }

            try
            {
                _logger.LogInformation("Creating multiple currencies");

                var createdCurrencies = await _currencyService.CreateCurrencies(currencyCreateDtos);

                if (createdCurrencies == null || !createdCurrencies.Any())
                {
                    _logger.LogError("Failed to create currencies.");
                    return BadRequest("Failed to create currencies.");
                }

                return Ok(createdCurrencies);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex, "An error occurred while creating currencies");

                return StatusCode(500, InternalServerErrorMessage + ex.Message);
            }
        }
    }
}