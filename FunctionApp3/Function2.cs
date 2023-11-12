
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FunctionApp3.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp3
{
    public class Function2
    {
        [FunctionName("Function2")]
        public void Run(
            [BlobTrigger("samples-workitems/{name}", Connection = "StorageConnectionString")] Stream myBlob,
            string name,
            [Sql("[dbo].[CapteurIfo]", "SqlConnectionString")] IAsyncCollector<CapteurIfo> capteurInfo,
            ILogger log)
        {
            log.LogInformation($"File name :{name} - Size: {myBlob.Length}");

            using (StreamReader reader = new(myBlob))
            {
                string jsonContent = reader.ReadToEnd();
                var capteurList = JsonSerializer.Deserialize<List<CapteurIfo>>(jsonContent);
                capteurList.ForEach(x => capteurInfo.AddAsync(x));
            }
        }
    }
}