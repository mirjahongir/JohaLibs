using JohaRepository.Interfaces;

using System;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace TelegramNotifier
{
    //public class MongoTelegramNotifier : IPushNotifier
    //{
    //    private ReceiverOptions receiverOptions;

    //    public MongoTelegramNotifier(string token)
    //    {
    //        var botClient = new TelegramBotClient(token);
    //        var cts = new CancellationTokenSource();
    //        botClient.StartReceiving(HandleUpdateAsync,
    //HandleErrorAsync,
    //receiverOptions,
    //cancellationToken: cts.Token);
    //        var me = botClient.GetMeAsync().Result;

    //        Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
    //    }

    //    private Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private async Task HandleUpdateAsync(ITelegramBotClient arg1, Telegram.Bot.Types.Update update, CancellationToken arg3)
    //    {
    //        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
    //        {

    //        }
    //        if (update.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
    //        {

    //            return;
    //        }
    //    }

    //    public string SendMessage(string message)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
