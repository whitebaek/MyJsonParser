using MyMi9Code.lib;
using MyMi9Code.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyMi9Code.Controllers
{
    public class RequestDataController : ApiController
    {
        [Route("")]
        public async Task<HttpResponseMessage> PostData(HttpRequestMessage request)
        {
            try
            {
                var jsonString = await request.Content.ReadAsStringAsync();

                //Validate JSON with JsonValidatingReader 
                JsonTextReader reader = new JsonTextReader(new StringReader(jsonString));
                JsonValidatingReader validatingReader = new JsonValidatingReader(reader);
                validatingReader.Schema = JsonSchema.Parse(Helper.Instance().jsonRequest);

                IList<string> messages = new List<string>();
                validatingReader.ValidationEventHandler += (o, a) => messages.Add(a.Message);
                JsonSerializer serializer = new JsonSerializer();

                //DeserializeObject
                RequestData requestData = serializer.Deserialize<RequestData>(validatingReader);

                //Compute responseData
                var responseDatas = Helper.Instance().GetResponseData(requestData);


                //
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                string jsonResponse = JsonConvert.SerializeObject(responseDatas);
                response.Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json");

                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                return response;
            }
            catch
            {
                var myError = new
                {
                    //error = ex.Message
                    error = "Could not decode request: JSON parsing failed"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, myError);

            }
        }
    }
}
