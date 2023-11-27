using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6812726099:AAGBY51eekLF6DNwQjdJG3SsgJ3SVBpZytQ");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;
           
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new[] { new KeyboardButton("whats on your mind?"), new KeyboardButton("show me something")  },
                new [] { new KeyboardButton("count for me"), new KeyboardButton("ur not funny") },
            })

            {
                ResizeKeyboard = true
            };
                   
            switch (messageText)
            {
                case "whats on your mind?":
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "I've always felt alone my whole life, for as long as I can remember. I don't know if I like it... or if I'm just used to it, but I do know this: being lonely does things to you, and feeling shit and bitter and angry all the time just... eats away at you.");                    
                    break;

                case "show me something":
                    await botClient.SendPhotoAsync(
                    message.Chat.Id,
                    photo: InputFile.FromUri("https://imgflip.com/s/meme/Cute-Cat.jpg"),
                    caption: "here you go",
                    parseMode: ParseMode.Html,
                    cancellationToken: default(CancellationToken));
                    break;

                case "count for me":
                    await botClient.SendVideoAsync(
                    chatId: chatId,
                    video: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4"),
                    supportsStreaming: true,
                    cancellationToken: default(CancellationToken));
                    break;

                case "ur not funny":
                    await botClient.SendStickerAsync(
                    message.Chat.Id,
                    sticker: InputFile.FromFileId("CAACAgIAAxkBAAECKihlZH6eOyErUn3_Jwag_ZjIjrbe8AACljIAAoyDEEoTU1lqnU0dPDME"),
                    cancellationToken: default(CancellationToken));
                    break;

                default:
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "what are you seeking for",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: default(CancellationToken));                       
                    break;
            }
        }


        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}