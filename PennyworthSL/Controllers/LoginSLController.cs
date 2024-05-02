using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PennyworthSL.Models;
using System.Net;
using System.Text;

namespace PennyworthSL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginSLController : ControllerBase
    {
        [HttpPost]
        [ActionName("login")]
        [ProducesResponseType(typeof(LoginSL), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoginSL), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginSL([FromBody] LoginSL body)
        {
            try
            {
                string url = "https://hanab1:50000/b1s/v1/Login";

                string result = String.Empty;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient httpClient = new HttpClient(clientHandler);
                var bodyjson = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(bodyjson, Encoding.UTF8));

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    result = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.BadRequest) { return BadRequest(result); }
                    if (response.StatusCode == HttpStatusCode.NotFound) { return NotFound(Array.Empty<string>()); }
                    if (response.StatusCode == HttpStatusCode.Unauthorized) { return StatusCode(401, result); }
                    if (response.StatusCode == HttpStatusCode.InternalServerError) { return StatusCode(500, result); }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                //return BadRequest(new ResponseError { code = 500, message = ex.Message, error = Array.Empty<string>() });
                return BadRequest(ex);
            }
        }
    }
}
