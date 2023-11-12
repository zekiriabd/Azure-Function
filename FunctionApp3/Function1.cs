
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Communication.Email;
using System.Threading;
using System.IO;
using FunctionApp3.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace FunctionApp3
{
   
    public static class Function1
    {
        [FunctionName("FunctionName")]
        public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        [Sql("[dbo].[CapteurIfo]", "SqlConnectionString")] out CapteurIfo capteurInfo, 
        ILogger log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            capteurInfo = new CapteurIfo();
            capteurInfo = JsonSerializer.Deserialize<CapteurIfo>(requestBody);
            //SendEmail().Wait();
            return new OkObjectResult("Data has been stored successfully.");
        }

        private static async Task SendEmail()
        {
            var connectionString = "endpoint=https://mycommunicationservices2023.france.communication.azure.com/;accesskey=1o5f4tac+2T8S+0JTYdx9aZRYLjKPcPQT8RPNq7EmS27tM97sCFcJbYFBDFWd9i1x2uxA2Tio492JT3JKERxQg==";
            var emailClient = new EmailClient(connectionString);
            var emailContent = new EmailContent("Email Subject Here");
            emailContent.PlainText = "The plain text content of the email.";
            emailContent.Html = "<html><body><h1>The HTML content of the email.</h1></body></html>";
            var emailMessage = new EmailMessage("DoNotReply@4a91af26-3e6b-448c-b536-20a72636f09d.azurecomm.net",
                "zekiriabd@yahoo.fr", emailContent);
            var wait = new Azure.WaitUntil();
            await emailClient.SendAsync(wait, emailMessage, cancellationToken: CancellationToken.None);
        }
    }
}


//[FunctionName("FunctionName")]
//public static IActionResult Run(
//[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
//[Sql("GetCapteurIfos", "SqlConnectionString", CommandType.StoredProcedure)] IEnumerable<CapteurIfo> capteurInfos, ILogger log)
//{
//    return new OkObjectResult(capteurInfos);
//}
//[FunctionName("FunctionName")]
//public static IActionResult Run(
//[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
//[Sql("SELECT * FROM CapteurIfo", "SqlConnectionString", CommandType.Text)] IEnumerable<CapteurIfo> capteurInfos, ILogger log)
//{
//    return new OkObjectResult(capteurInfos);
//}





