// WhatsAppService.cs
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WhatsAppBot
{
    public class WhatsAppService
    {
        private IWebDriver _driver;
        private HashSet<string> _seenMessages; // To keep track of seen messages

        public WhatsAppService()
        {
            _seenMessages = new HashSet<string>(); // Initialize the seen messages set
            StartWhatsApp();
        }

        private void StartWhatsApp()
        {
            // Initialize Chrome WebDriver
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://web.whatsapp.com");
            Console.WriteLine("Please scan the QR code to log in.");
        }

 public void ExtractMessages()
{
    // Wait for the user to log in and for the messages to load
    Thread.Sleep(15000); // Wait 15 seconds for manual login

    while (true)
    {
        try
        {
            // Locate message elements using the new class _ajx_ (or whatever class is responsible for messages)
            var messages = _driver.FindElements(By.ClassName("_amm9")); // Use the correct class for messages

            foreach (var message in messages)
            {
                try
                {
                    // Get sender, message text, and timestamp (Update based on the current structure)
                    var sender = message.FindElement(By.CssSelector("span[title]")).Text; // Sender's name
                    var messageText = message.FindElement(By.CssSelector("span.selectable-text")).Text; // Message text
                    var timestamp = DateTime.Now; // You can extract the actual time from the DOM if needed

                    // Print the message to console
                    Console.WriteLine($"[{timestamp}] {sender}: {messageText}");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Unable to extract message details.");
                }
            }
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("No new messages found.");
        }

        // Wait a few seconds before checking for new messages again
        Thread.Sleep(5000);
    }
}



        private void CheckForDeletedMessages(HashSet<string> currentMessages)
        {
            // Find messages that were seen but are no longer in the current message list
            var deletedMessages = _seenMessages.Except(currentMessages).ToList();

            foreach (var messageId in deletedMessages)
            {
                Console.WriteLine($"[DELETED] Message ID: {messageId} has been deleted.");
                _seenMessages.Remove(messageId); // Remove from seen messages
            }
        }
    }
}
