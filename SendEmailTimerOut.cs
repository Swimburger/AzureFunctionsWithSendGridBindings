using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace AzureFunctionsWithSendGridBindings
{
  public class SendEmailTimerOut
  {
    [FunctionName("SendEmailTimerOut")]
    public void Run(
      [TimerTrigger("*/15 * * * * *")] TimerInfo myTimer,
      ILogger log,
      [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message1,
      [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message2
    )
    {
      log.LogInformation($"SendEmailTimerOut executed at: {DateTime.Now}");

      message1 = new SendGridMessage()
      {
        From = new EmailAddress("[REPLACE WITH YOUR EMAIL]", "[REPLACE WITH YOUR NAME]"),
        Subject = "Sending emails with Twilio SendGrid is Fun",
        PlainTextContent = "and easy to do anywhere, especially with C#",
        HtmlContent = "and easy to do anywhere, <strong>especially with C#</strong>"
      };
      message1.AddTo(new EmailAddress("[REPLACE WITH DESIRED TO EMAIL]", "[REPLACE WITH DESIRED TO NAME]"));

      message2 = new SendGridMessage()
      {
        From = new EmailAddress("[REPLACE WITH YOUR EMAIL]", "[REPLACE WITH YOUR NAME]"),
        Subject = "Sending emails with Twilio SendGrid is Fun",
        PlainTextContent = "Azure Functions and SendGrid makes sending emails easy!",
        HtmlContent = "<strong>Azure Functions and SendGrid</strong> makes sending emails easy!"
      };
      message2.AddTo(new EmailAddress("[REPLACE WITH DESIRED TO EMAIL]", "[REPLACE WITH DESIRED TO NAME]"));
    }
  }
}