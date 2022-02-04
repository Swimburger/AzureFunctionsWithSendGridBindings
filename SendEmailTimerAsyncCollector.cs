using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace AzureFunctionsWithSendGridBindings
{
  public class SendEmailTimerAsyncCollector
  {
    [FunctionName("SendEmailTimerAsyncCollector")]
    public async Task Run(
      [TimerTrigger("*/15 * * * * *")] TimerInfo myTimer,
      ILogger log,
      [SendGrid(ApiKey = "SendGridApiKey")]
      IAsyncCollector<SendGridMessage> messageCollector
    )
    {
      log.LogInformation($"SendEmailTimerAsyncCollector executed at: {DateTime.Now}");

      for (int i = 1; i <= 2; i++)
      {
        var message = new SendGridMessage()
        {
          From = new EmailAddress("[REPLACE WITH YOUR EMAIL]", "[REPLACE WITH YOUR NAME]"),
          Subject = $"Email {i}: Sending emails with Twilio SendGrid is Fun ",
          PlainTextContent = "and easy to do anywhere, especially with C#",
          HtmlContent = "and easy to do anywhere, <strong>especially with C#</strong>"
        };
        message.AddTo(new EmailAddress("[REPLACE WITH DESIRED TO EMAIL]", "[REPLACE WITH DESIRED TO NAME]"));

        await messageCollector.AddAsync(message);
      }

      await messageCollector.FlushAsync();
    }
  }
}