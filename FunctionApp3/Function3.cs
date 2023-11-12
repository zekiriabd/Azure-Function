using System;
using System.Collections.Generic;
using System.Text.Json;
using FunctionApp3.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp3
{
    public static class Function3
    {
        [FunctionName("Function3")]
        public static void Run([CosmosDBTrigger(
            databaseName: "ToDoList",
            containerName: "Items",
            Connection = "CosmosDbConnectionString",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)]IReadOnlyList<ToDoItem> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
               
                log.LogInformation("document : " + 
                    JsonSerializer.Serialize(input));
                  
            }
        }
    }

   
}
