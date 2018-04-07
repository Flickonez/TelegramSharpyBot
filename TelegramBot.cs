using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class MyBot
    {
        #region Variables
        private static readonly TelegramBotClient botClient = new TelegramBotClient("token");
        private static readonly string korobaImageUrl = "http://biblio.dissernet.org/files/medium/86821_98.jpg";
        private static readonly string korobaRedImageUrl = "https://i.imgur.com/mymwmBG.jpg";
        private static readonly string mulExImageUrl = "https://i.imgur.com/pFyIi15.png";
        private static readonly string divExImageUrl = "https://i.imgur.com/XbYOSn3.png";
        private static readonly string plusMinusExImageUrl = "https://i.imgur.com/Eh5Tj09.png";
        private static readonly string unsignedTransferImageUrl = "https://i.imgur.com/knbKLql.png";
        private static readonly string signedTransferImageUrl = "https://i.imgur.com/Zb6Jn9j.png";
        private static readonly string unsignedCharL1ImageUrl = "https://i.imgur.com/2bynDQn.png";
        private static readonly string signedCharL1ImageUrl = "https://i.imgur.com/RkohpTy.png";
        private static readonly string unsignedIntL1ImageUrl = "https://i.imgur.com/HhRBzeX.png";
        private static readonly string signedIntL1ImageUrl = "https://i.imgur.com/DfAFVGn.png";
        private static readonly string unsignedIntL2ImageUrl = "https://i.imgur.com/NSfr7j7.png";
        private static readonly string signedIntL2ImageUrl = "https://i.imgur.com/yKotusG.png";

        private static readonly FileToSend korobaImage = new FileToSend(korobaImageUrl);
        private static readonly FileToSend korobaRedImage = new FileToSend(korobaRedImageUrl);
        private static readonly FileToSend mulExImage = new FileToSend(mulExImageUrl);
        private static readonly FileToSend divExImage = new FileToSend(divExImageUrl);
        private static readonly FileToSend plusMinusExImage = new FileToSend(plusMinusExImageUrl);
        private static readonly FileToSend unsignedTransferImage = new FileToSend(unsignedTransferImageUrl);
        private static readonly FileToSend signedTransferImage = new FileToSend(signedTransferImageUrl);
        private static readonly FileToSend unsignedCharL1Image = new FileToSend(unsignedCharL1ImageUrl);
        private static readonly FileToSend signedCharL1Image = new FileToSend(signedCharL1ImageUrl);
        private static readonly FileToSend unsignedIntL1Image = new FileToSend(unsignedIntL1ImageUrl);
        private static readonly FileToSend signedIntL1Image = new FileToSend(signedIntL1ImageUrl);
        private static readonly FileToSend unsignedIntL2Image = new FileToSend(unsignedIntL2ImageUrl);
        private static readonly FileToSend signedIntL2Image = new FileToSend(signedIntL2ImageUrl);
        private static readonly FileToSend questionList = new FileToSend("https://i.imgur.com/hADvY57.png");

        private static readonly List<List<FileToSend>> imgList = new List<List<FileToSend>>()
        {
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/dhPQ3CS.jpg"), // 1_1
               new FileToSend("https://i.imgur.com/VDdviLX.jpg"), // 1_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/sMIMCQo.png"), // 2_1
               new FileToSend("https://i.imgur.com/Tsds5r9.png") // 2_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/nTXEeCG.png"), // 3_1
               new FileToSend("https://i.imgur.com/1JUAoDt.png"), // 3_2
               new FileToSend("https://i.imgur.com/Iqf6oSt.png"), // 3_3
               new FileToSend("https://i.imgur.com/O68dGAw.png"), // 3_4
               new FileToSend("https://i.imgur.com/m7XRvkR.png") // 3_5
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/d1Svk59.png"), // 4_1
               new FileToSend("https://i.imgur.com/nGhKJnR.png") // 4_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/qJk2rbx.png"), // 5_1
               new FileToSend("https://i.imgur.com/XN1rKo1.png") // 5_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/u1MjJMP.png"), // 6_1
               new FileToSend("https://i.imgur.com/Ye943DB.png") // 6_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/IxJQ9mV.png") // 7
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/5Bmwgdu.png"), // 8_1
               new FileToSend("https://i.imgur.com/bgpzB1E.png") // 8_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/f6trWaq.png"), // 9_1
               new FileToSend("https://i.imgur.com/jg5B3WZ.png") // 9_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/gQv5TsX.png"), // 10_1
               new FileToSend("https://i.imgur.com/58OEnpX.png") // 10_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/liAgjcR.png"), // 11_1
               new FileToSend("https://i.imgur.com/bSNe780.png"), // 11_2
               new FileToSend("https://i.imgur.com/U8OxgDU.png") // 11_3
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/ReX2XCT.png"), // 12_1
               new FileToSend("https://i.imgur.com/CcS4Rah.png") // 12_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/vN0NPFO.png"), // 13_1
               new FileToSend("https://i.imgur.com/mrMD1dd.png"), // 13_2
               new FileToSend("https://i.imgur.com/eZdeUjR.png") // 13_3
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/QSaAfUj.png"), // 14_1
               new FileToSend("https://i.imgur.com/2yjgHYD.png") // 14_2
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/a2ApOau.png") // 15
           },
           new List<FileToSend>()
           {
               new FileToSend("https://i.imgur.com/jCO1Z01.png"), // 16_1
               new FileToSend("https://i.imgur.com/0ot3uQm.png"), // 16_2
               new FileToSend("https://i.imgur.com/NdDywo3.png") // 16_3
           }
        };

        private static Dictionary<long, String> userChoices = new Dictionary<long, string>();
        #endregion

        #region Keyboards
        static ReplyKeyboardMarkup mainKeyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
        {
            Keyboard = new[] {
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("\U0001F4D1 Программа"),
                                                    new Telegram.Bot.Types.KeyboardButton("\U00002753 Вопрос")
                                                },
                                            },
            ResizeKeyboard = true
        };

        static ReplyKeyboardMarkup progKeyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
        {
            Keyboard = new[] {
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("Типы данных"),
                                                    new Telegram.Bot.Types.KeyboardButton("Арифметические операции")
                                                },
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("Команды преобразования"),
                                                    new Telegram.Bot.Types.KeyboardButton("Команды передачи управления")
                                                },
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("\U0001F4D6 Примеры"),
                                                    new Telegram.Bot.Types.KeyboardButton("\U000023EA Назад")
                                                },
                                            },
            ResizeKeyboard = true
        };
        #endregion

        public static void Main(string[] args)
        {
            var me = botClient.GetMeAsync().Result;
            Console.Title = me.Username;

            botClient.OnMessage += BotOnMessageReceived;
            botClient.OnMessageEdited += BotOnMessageReceived;
            botClient.OnCallbackQuery += BotOnCallbackQueryReceived;

            botClient.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

               
             
            botClient.StopReceiving();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            if(userChoices.ContainsKey(message.From.Id))
            {

            }
            else
            {
                userChoices.Add(message.From.Id, "");
            }

            switch (message.Text.ToLower())
            {
                case "/start":
                    await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(500); 


                    await botClient.SendPhotoAsync(
                    message.Chat.Id,
                    korobaImage,
                    "HI THERE"
                    );
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "CHOOSE YOUR DESTINY",
                    Telegram.Bot.Types.Enums.ParseMode.Default,
                    false,
                    false,
                    0,
                    mainKeyboard
                    );
                    break;


                case "\U0001F4D1 программа":
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите тему",
                    Telegram.Bot.Types.Enums.ParseMode.Default,
                    false,
                    false,
                    0,
                    progKeyboard
                    );
                    break;


                case "типы данных":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*SIGNED BYTE*  [-128..127]" +
                                                                  "\n*UNSIGNED BYTE*  [0..255]" +
                                                                  "\n*SIGNED WORD*  [-32768..32767]" +
                                                                  "\n*UNSIGNED WORD* [0..65535]" +
                                                                  "\n*SIGNED DWORD*  [-2147483648..2147483647]" +
                                                                  "\n*UNSIGNED DWORD*  [0..4294967295]",
                                                                  parseMode: ParseMode.Markdown);

                    break;


                case "команды преобразования":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*CBW* - Преобразование байта в слово ( AL -> AX )"
                                                                 + "\n*CWD* - Преобразовать слово в двойное слово(AX->DX:AX)"
                                                                 + "\n*CWDE* - Преобразовать AX в EAX"
                                                                 + "\n*CDQ* - Преобразовать EAX в EDX: EAX",
                                                                 parseMode: ParseMode.Markdown);
                    break;
                case "команды передачи управления":
                    var typesCommandsTransferManaging = new InlineKeyboardMarkup(new[]
                        {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Команды для беззнаковых типов", "transEx_US"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Команды для знаковых типов", "transEx_S"),
                        },
                    });
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                    "*JMP* MARK - переходит на метку MARK\n" +
                    "*CMP* A, B - сравнивает A и B",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: typesCommandsTransferManaging);
                    break;
                case "арифметические операции":
                    userChoices[message.From.Id] = "арифметические операции";
                    var arithmKeyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                    {
                        Keyboard = new[] {
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("\U0000274C Умножение"),
                                                    new Telegram.Bot.Types.KeyboardButton("\U00002797 Деление")
                                                },
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("\U00002795 \U00002796 Сложение/Вычитание"),
                                                    new Telegram.Bot.Types.KeyboardButton("\U000023EA Назад")
                                                },
                                            },
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите операцию",
                    Telegram.Bot.Types.Enums.ParseMode.Default,
                    false,
                    false,
                    0,
                    arithmKeyboard);
                    break;
                case "\U0000274C умножение":
                    var arithmMulExamplesKeybord = new InlineKeyboardMarkup(new[]
                                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Пример", "mulEx"),
                        },
                });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*MUL/IMUL K*\n"
                                                                 + "Если K - байт, второй сомножитель в AL, результат в AX\n"
                                                                 + "Если K - слово, второй сомножитель в AX, результат в DX: AX\n"
                                                                 + "Если K - двойное слово, второй сомножитель в EAX, результат в EDX: EAX",
                                                                 parseMode: ParseMode.Markdown,
                                                                 replyMarkup: arithmMulExamplesKeybord);
                    break;
                case "\U00002797 деление":
                    var arithmDivExamplesKeybord = new InlineKeyboardMarkup(new[]
                        {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Пример", "divEx"),
                        },
                    });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*DIV/IDIV K*\n"
                                                                 + "Если K - байт, делимое в AX, результат: частное в AL, остаток в AH\n"
                                                                 + "Если K - слово, делимое в DX:AX, результат: частное в AX, остаток в DX\n"
                                                                 + "Если K - двойное слово, делимое в EDX:EAX, результат: частное в EAX, остаток в EDX",
                                                                 parseMode: ParseMode.Markdown,
                                                                 replyMarkup: arithmDivExamplesKeybord);
                    break;
                case "\U00002795 \U00002796 сложение/вычитание":
                    var arithmPlusMinusExamplesKeybord = new InlineKeyboardMarkup(new[]
                            {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Пример", "plusMinusEx"),
                        },
                    });
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                                                         "*ADD A,B* - прибавляет B к A\n" +
                                                         "*SUB A,B* - вычитает B из A",
                                                         parseMode: ParseMode.Markdown,
                                                         replyMarkup: arithmPlusMinusExamplesKeybord);
                    break;
                case "\U0001F4D6 примеры":
                    userChoices[message.From.Id] = "примеры";
                    var programExamplesKeyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                    {
                        Keyboard = new[] {
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("Лаба \U00000031"),
                                                    new Telegram.Bot.Types.KeyboardButton("Лаба \U00000032")
                                                },
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("\U000023EA Назад"),
                                                },
                                            },
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Какая лабораторная?",
                    replyMarkup: programExamplesKeyboard
                    );
                    break;
                case "\U000023EA назад":
                    if (userChoices[message.From.Id] == "арифметические операции")
                    {
                        await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "Выберите тему",
                        Telegram.Bot.Types.Enums.ParseMode.Default,
                        false,
                        false,
                        0,
                        progKeyboard
                        );
                        userChoices[message.From.Id] = "";
                    }
                    else if (userChoices[message.From.Id] == "примеры")
                    {
                        await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "Выберите тему",
                        Telegram.Bot.Types.Enums.ParseMode.Default,
                        false,
                        false,
                        0,
                        progKeyboard
                        );
                        userChoices[message.From.Id] = "";
                    }
                    else
                    {
                        await botClient.SendPhotoAsync(
                        message.Chat.Id,
                        korobaRedImage,
                        "CHOOSE YOUR DESTINY",
                        false,
                        0,
                        mainKeyboard);
                        userChoices[message.From.Id] = "";
                    }
                    break;
                case "лаба \U00000031":
                    var lab1ExamplesKeyboard = new InlineKeyboardMarkup(new[]
    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("UNSIGNED CHAR", "unsignedCharL1"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("SIGNED CHAR", "signedCharL1"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("UNSIGNED INT", "unsignedIntL1"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("SIGNED INT", "signedIntL1"),
                        },
                    });
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Тип данных?",
                    replyMarkup: lab1ExamplesKeyboard
                    );
                    break;
                case "лаба \U00000032":
                    var lab2ExamplesKeyboard = new InlineKeyboardMarkup(new[]
{
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("UNSIGNED INT", "unsignedIntL2"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("SIGNED INT", "signedIntL2"),
                        },
                    });
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Тип данных?",
                    replyMarkup: lab2ExamplesKeyboard
                    );
                    break;
                case "\U00002753 вопрос":
                    userChoices[message.From.Id] = "вопрос";
                    var questionsKeyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                    {
                        Keyboard = new[] {
                                                new[]
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("\U0001F4CB Список"),
                                                    new Telegram.Bot.Types.KeyboardButton("\U000023EA Назад")
                                                },
                                            },
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Введите номер интересующего вас билета",
                    replyMarkup: questionsKeyboard
                    );
                    break;
                case "\U0001F4CB список":
                    await botClient.SendPhotoAsync(
                       message.Chat.Id,
                       questionList,
                       "Список билетов",
                       false,
                       0);
                    break;
                default:
                    if(userChoices[message.From.Id] == "вопрос")
                    {
                        int ticket = 0;
                        try
                        {
                            ticket = Convert.ToInt32(message.Text.Trim());
                        }
                        catch(Exception e)
                        {
                            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                            await Task.Delay(500);
                            await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Неверно введен номер билета");
                            break;
                        };
                        if (ticket > 0 && ticket <= 16)
                        {
                            for(int i = 0; i < imgList[ticket-1].Count; i++)
                            {
                                await botClient.SendPhotoAsync(
                                   message.Chat.Id,
                                   imgList[ticket-1][i],
                                   $"Билет №{ticket}.{i+1}",
                                   false,
                                   0);
                            }
                        }
                    }
                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            switch(callbackQuery.Data)
            {
                case "mulEx":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mulExImage
                        );
                    break;
                case "divEx":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        divExImage
                        );
                    break;
                case "plusMinusEx":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        plusMinusExImage
                        );
                    break;
                case "transEx_US":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        unsignedTransferImage
                        );
                    break;
                case "transEx_S":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        signedTransferImage
                        );
                    break;
                case "unsignedCharL1":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        unsignedCharL1Image
                        );
                    break;
                case "signedCharL1":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        signedCharL1Image
                        );
                    break;
                case "unsignedIntL1":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         unsignedIntL1Image
                         );
                    break;
                case "signedIntL1":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         signedIntL1Image
                         );
                    break;
                case "unsignedIntL2":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         unsignedIntL2Image
                         );
                    break;
                case "signedIntL2":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         signedIntL2Image
                         );
                    break;
                default:
                    break;
            }
           
        }
    }
}
