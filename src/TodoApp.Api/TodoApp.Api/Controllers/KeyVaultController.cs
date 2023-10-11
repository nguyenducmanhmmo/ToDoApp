using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Core.Entities.Business;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private readonly ILogger<KeyVaultController> _logger;
        public KeyVaultController(ILogger<KeyVaultController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// For testing purpose, get connection string from key vault
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "For testing purpose, get connection string from key vault")]
        [HttpGet("sqlconnectionstring")]
        public async Task<ActionResult> GetKeyVaultSQLConnectionString()
        {
            try
            {
                var keyVaultEndpoint = "https://orient-test.vault.azure.net/";
                var secretClient = new SecretClient(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
                var secret = await secretClient.GetSecretAsync("sqlconnectionstring9b4yw1");
                _logger.LogError(secret.ToString());
                return secret is null ? NotFound() : Ok(secret);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving toDos");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
