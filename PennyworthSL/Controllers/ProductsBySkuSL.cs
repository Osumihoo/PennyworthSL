using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PennyworthSL.Models;
using System.Net;
using System.Text;

namespace PennyworthSL.Controllers
{
    [ApiController]
    [Route("api/PBSSL")]
    public class ProductsBySkuSL : ControllerBase
    {
        [HttpGet("{sku}")]
        [ActionName("obtener_codigo_articulo")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PBSSL(string sku)
        {
            string IDss = string.Empty;

            try
            {


                try
                {
                    LoginSL body = new LoginSL();
                    //body.CompanyDB = "TESTREP";
                    //body.UserName = "Desarrollo";
                    //body.Password = "123456";

                    string urllogin = "https://hanab1:50000/b1s/v1/Login";

                    string result = String.Empty;
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient httpClient = new HttpClient(clientHandler);
                    var bodyjson = Newtonsoft.Json.JsonConvert.SerializeObject(body);
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
                    string url = "https://hanab1:50000/b1s/v1/SQLQueries('PL_PBS')/List?sku='" + sku + "'";
                    string result = String.Empty;
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient httpClient = new HttpClient(clientHandler);
                    httpClient.DefaultRequestHeaders.Add("Cookie", String.Format("B1SESSION={0}", IDss));
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.BadRequest) { return BadRequest(result); }
                        if (response.StatusCode == HttpStatusCode.NotFound) { return NotFound(Array.Empty<string>()); }
                        if (response.StatusCode == HttpStatusCode.Unauthorized) { return StatusCode(401, result); }
                        if (response.StatusCode == HttpStatusCode.InternalServerError) { return StatusCode(500, result); }
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }
    }
}
