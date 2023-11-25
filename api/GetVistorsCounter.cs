using System.Net;
using System.Text.Json;
using Api.Function;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetVistorsCounter
    {
        private readonly ILogger _logger;

        public GetVistorsCounter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetVistorsCounter>();
        }

        [Function("GetVistorsCounter")]
        public async Task<UpdatedCounter> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        
        [CosmosDBInput("satish-resume","Counter", Connection = "CosmosDbConnectionString", Id = "1", PartitionKey = "1")] Counter counter)
        
        {

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        string jsonString = JsonSerializer.Serialize(counter);
        await response.WriteStringAsync(jsonString);
        counter.Count += 1;
        return new UpdatedCounter()
        {
            NewCounter = counter,
            HttpResponse = response
        };
    }
    }
}
