using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace AzureFunctionsWithSendGridBindings
{
  public class SendEmailTimer
  {
    [FunctionName("SendEmailTimer")]
    [return: SendGrid(ApiKey = "SendGridApiKey")]
    public SendGridMessage Run([TimerTrigger("*/15 * * * * *")] TimerInfo myTimer, ILogger log)
    {
      log.LogInformation($"SendEmailTimer executed at: {DateTime.Now}");

      var msg = new SendGridMessage()
      {
        From = new EmailAddress("[REPLACE WITH YOUR EMAIL]", "[REPLACE WITH YOUR NAME]"),
        Subject = "Sending emails with Twilio SendGrid is Fun",
        PlainTextContent = "and easy to do anywhere, especially with C#",
        HtmlContent = "and easy to do anywhere, <strong>especially with C#</strong>"
      };
      msg.AddTo(new EmailAddress("[REPLACE WITH DESIRED TO EMAIL]", "[REPLACE WITH DESIRED TO NAME]"));

      return msg;
    }
  }
}