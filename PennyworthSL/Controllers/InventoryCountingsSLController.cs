using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PennyworthSL.Models;
using System.Net;
using System.Text;

namespace PennyworthSL.Controllers
{
    [ApiController]
    [Route("api/ICSL")]
    public class InventoryCountingsSLController : ControllerBase
    {
        [HttpPost]
        [ActionName("generar_reajuste")]
        [ProducesResponseType(typeof(InventoryCountingsSL), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InventoryCountingsSL), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ICSL([FromBody] InventoryCountingsSL body)
        {
            string IDss = string.Empty;


            try
            {
                LoginSL bodylogin = new LoginSL();
                //bodylogin.CompanyDB = "TESTREP";
                //bodylogin.UserName = "Desarrollo";
                //bodylogin.Password = "123456";

                string urllogin = "https://hanab1:50000/b1s/v1/Login";

                string result = String.Empty;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient httpClient = new HttpClient(clientHandler);
                var bodyjson = Newtonsoft.Json.JsonConvert.SerializeObject(bodylogin);
                HttpResponseMessage response = await httpClient.PostAsync(urllogin, new StringContent(bodyjson, Encoding.UTF8));

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                    var sesion = JsonConvert.DeserializeObject<dynamic>(result);
                    IDss = sesion["SessionId"];
                }
                else
                {
                    result = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.BadRequest) { return BadRequest(result); }
                    if (response.StatusCode == HttpStatusCode.NotFound) { return NotFound(Array.Empty<string>()); }
                    if (response.StatusCode == HttpStatusCode.Unauthorized) { return StatusCode(401, result); }
                    if (response.StatusCode == HttpStatusCode.InternalServerError) { return StatusCode(500, result); }
                }

                //return Ok(result);
            }
            catch (Exception ex)
            {
                //return BadRequest(new ResponseError { code = 500, message = ex.Message, error = Array.Empty<string>() });
                return BadRequest(ex);
            }

            try
            {
                string url = "https://hanab1:50000/b1s/v1/InventoryCountings";

                string result = String.Empty;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient httpClient = new HttpClient(clientHandler);
                httpClient.DefaultRequestHeaders.Add("Cookie", String.Format("B1SESSION={0}", IDss));
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
