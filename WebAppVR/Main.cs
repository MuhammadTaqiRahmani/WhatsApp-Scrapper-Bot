// Main.css
using WhatsAppBot;

class Program
{
    static void Main(string[] args)
    {
        var whatsappService = new WhatsAppService();
        whatsappService.ExtractMessages();
    }
}
